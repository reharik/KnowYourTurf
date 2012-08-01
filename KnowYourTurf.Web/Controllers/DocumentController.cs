using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FubuMVC.Core;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
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

        public ActionResult AddUpdate(ViewModel input)
        {
            var document = input.EntityId > 0 ? _repository.Find<Document>(input.EntityId) : new Document();
            var categoryItems = _selectListItemService.CreateList<DocumentCategory>(x=>x.Name,x=>x.EntityId,true);
            var model = new DocumentViewModel
            {
                Item = document,
                DocumentCategoryList = categoryItems,
                _Title = WebLocalizationKeys.DOCUMENT_INFORMATION.ToString(),
                Popup = input.Popup
            };
            return View(model);
        }
      
        public ActionResult Display(DocumentViewModel input)
        {
            var document = _repository.Find<Document>(input.EntityId);
            var model = new DocumentViewModel
                            {
                                Item = document,
                                AddUpdateUrl = UrlContext.GetUrlForAction<DocumentController>(x => x.AddUpdate(null)) + "/" + document.EntityId,
                                _Title = WebLocalizationKeys.DOCUMENT_INFORMATION.ToString(),
                                Popup = input.Popup
                            };
            return View(model);
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
            input.EntityIds.Each(x =>
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
            var coId = _sessionContext.GetCompanyId();
            var document = input.Item.EntityId > 0 ? _repository.Find<Document>(input.Item.EntityId) : new Document();
            var newDoc = mapToDomain(input, document);
            document.FileUrl = _fileHandlerService.SaveAndReturnUrlForFile("CustomerDocuments");
            var crudManager = _saveEntityService.ProcessSave(newDoc);
            if (input.Var == "Field")
            {
                var field = _repository.Find<Field>(input.ParentId);
                field.AddDocument(document);
                crudManager = _saveEntityService.ProcessSave(field, crudManager);
            }
            
            var notification = crudManager.Finish();
            return Json(notification, "text/plain");
        }

        private Document mapToDomain(DocumentViewModel input, Document document)
        {
            var documentModel = input.Item;
            document.FileType = documentModel.FileType;
            
            document.Name = documentModel.Name;
            document.Description = documentModel.Description;
            if (document.DocumentCategory == null || document.DocumentCategory.EntityId != input.Item.DocumentCategory.EntityId)
            {
                document.DocumentCategory = _repository.Find<DocumentCategory>(input.Item.DocumentCategory.EntityId);
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
        public Document Item { get; set; }
        public IEnumerable<SelectListItem> DocumentCategoryList { get; set; }
        public long DocumentCategory { get; set; }
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