using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class DocumentController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly ISelectListItemService _selectListItemService;
        private readonly ISessionContext _sessionContext;

        public DocumentController(IRepository repository,
            ISaveEntityService saveEntityService,
            IFileHandlerService fileHandlerService,
            ISelectListItemService selectListItemService,
            ISessionContext sessionContext)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _fileHandlerService = fileHandlerService;
            _selectListItemService = selectListItemService;
            _sessionContext = sessionContext;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("AddUpdate",new DocumentViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var document = input.EntityId > 0 ? _repository.Find<Document>(input.EntityId) : new Document();
            var categoryItems = _selectListItemService.CreateList<DocumentCategory>(x=>x.Name,x=>x.EntityId,true);
            var model = Mapper.Map<Document, DocumentViewModel>(document);
            model._DocumentCategoryEntityIdList = categoryItems;
            model._Title = WebLocalizationKeys.DOCUMENT_INFORMATION.ToString();
            model.Popup = input.Popup;
            model._saveUrl = UrlContext.GetUrlForAction<DocumentController>(x => x.Save(null));
            model.Var = input.Var;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(ViewModel input)
        {
            var document = _repository.Find<Document>(input.EntityId);
            _repository.HardDelete(document);
            _repository.UnitOfWork.Commit();
            return null;
        }
        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            input.EntityIds.ForEachItem(x =>
            {
                var item = _repository.Find<Document>(x);
                _fileHandlerService.DeleteFile(item.FileUrl);
                _repository.HardDelete(item);
            });
            _repository.Commit();
            return Json(new Notification { Success = true }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Save(DocumentViewModel input)
        {
            // change to switch when we add docs to other things.
            var field = _repository.Find<Field>(input.ParentId);
            var document = field.Documents.FirstOrDefault(x => x.EntityId == input.EntityId) ?? new Document();
            document = mapToDomain(input, document);
            document.FileUrl = _fileHandlerService.SaveAndReturnUrlForFile("CustomerDocuments");

            field.AddDocument(document);
            var crudManager = _saveEntityService.ProcessSave(field);

            var notification = crudManager.Finish();
            return Json(notification, "text/plain");
        }

        private Document mapToDomain(DocumentViewModel input, Document document)
        {
            document.Name = input.Name;
            document.Description = input.Description;
            if (document.DocumentCategory == null || document.DocumentCategory.EntityId != input.DocumentCategoryEntityId)
            {
                document.DocumentCategory = _repository.Find<DocumentCategory>(input.DocumentCategoryEntityId);
            }
            return document;
        }

        public ActionResult docs(ViewModel input)
        {
            var docs = _repository.FindAll<Document>();
            var model = docs.Select(x => new DocumentDto {file = x});
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }

    public class DocumentViewModel:ViewModel
    {
        public IEnumerable<SelectListItem> _DocumentCategoryEntityIdList { get; set; }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int DocumentCategoryEntityId { get; set; }
        public virtual string FileUrl { get; set; }

        public string _saveUrl { get; set; }
    }

    public class DocViewModel : ViewModel
    {
        public IEnumerable<DocumentDto> files { get; set; } 
    }

    public class DocumentDto
    {
        public Document file { get; set; }
    }
}