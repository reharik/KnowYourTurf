using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
        private readonly ISelectListItemService _selectListItemService;
        private readonly IUploadedFileHandlerService _uploadedFileHandlerService;
        private readonly ISessionContext _sessionContext;

        public DocumentController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISelectListItemService selectListItemService,
            IUploadedFileHandlerService uploadedFileHandlerService,
            ISessionContext sessionContext)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _selectListItemService = selectListItemService;
            _uploadedFileHandlerService = uploadedFileHandlerService;
            _sessionContext = sessionContext;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var document = input.EntityId > 0 ? _repository.Find<Document>(input.EntityId) : new Document();
            var categoryItems = _selectListItemService.CreateList<DocumentCategory>(x=>x.Name,x=>x.EntityId,true);
            var model = new DocumentViewModel
            {
                Document = document,
                DocumentCategoryList = categoryItems,
                Title = WebLocalizationKeys.DOCUMENT_INFORMATION.ToString(),
                Popup = input.Popup
            };
            return View(model);
        }
      
        public ActionResult Display(DocumentViewModel input)
        {
            var document = _repository.Find<Document>(input.EntityId);
            var model = new DocumentViewModel
                            {
                                Document = document,
                                AddUpdateUrl = UrlContext.GetUrlForAction<DocumentController>(x => x.AddUpdate(null)) + "/" + document.EntityId,
                                Title = WebLocalizationKeys.DOCUMENT_INFORMATION.ToString(),
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
                _uploadedFileHandlerService.DeleteFile(item.FileUrl);
                _repository.HardDelete(item);
            });
            _repository.Commit();
            return Json(new Notification { Success = true }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Save(DocumentViewModel input)
        {
            var coId = _sessionContext.GetCompanyId();
            var document = input.Document.EntityId > 0 ? _repository.Find<Document>(input.Document.EntityId) : new Document();
            var newDoc = mapToDomain(input, document);
            var serverDirectory = "/CustomerDocuments/" + coId;
            document.FileUrl = _uploadedFileHandlerService.GetUploadedFileUrl(serverDirectory, document.Name);
            var crudManager = _saveEntityService.ProcessSave(newDoc);
            if (input.From == "Field")
            {
                var field = _repository.Find<Field>(input.ParentId);
                field.AddDocument(document);
                crudManager = _saveEntityService.ProcessSave(field, crudManager);
            }
            crudManager = _uploadedFileHandlerService.SaveUploadedFile(serverDirectory, newDoc.Name, crudManager);
            
            var notification = crudManager.Finish();
            return Json(notification, "text/plain");
        }

        private Document mapToDomain(DocumentViewModel input, Document document)
        {
            var documentModel = input.Document;
            document.FileType = documentModel.FileType;
            
            document.Name = documentModel.Name;
            document.Description = documentModel.Description;
            if (document.DocumentCategory == null || document.DocumentCategory.EntityId != input.Document.DocumentCategory.EntityId)
            {
                document.DocumentCategory = _repository.Find<DocumentCategory>(input.Document.DocumentCategory.EntityId);
            }
            return document;
        }
    }

    public class DocumentViewModel:ViewModel
    {
        public Document Document { get; set; }
        public IEnumerable<SelectListItem> DocumentCategoryList { get; set; }
        public long DocumentCategory { get; set; }
    }
}