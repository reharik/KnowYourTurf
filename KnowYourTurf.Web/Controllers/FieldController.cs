using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class FieldController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IUploadedFileHandlerService _uploadedFileHandlerService;
        private readonly IHttpContextAbstractor _httpContextAbstractor;

        public FieldController(IRepository repository,
            ISaveEntityService saveEntityService,
            IUploadedFileHandlerService uploadedFileHandlerService,
            IHttpContextAbstractor httpContextAbstractor)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _uploadedFileHandlerService = uploadedFileHandlerService;
            _httpContextAbstractor = httpContextAbstractor;
        }

        public ActionResult AddEdit(ViewModel input)
        {
            var field = input.EntityId > 0 ? _repository.Find<Field>(input.EntityId) : new Field();
            var model = new FieldViewModel
            {
                Field = field,
                AddEditUrl = UrlContext.GetUrlForAction<FieldController>(x => x.AddEdit(null)) + "/" + field.EntityId,
            };
            return PartialView("FieldAddUpdate", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var field = _repository.Find<Field>(input.EntityId);
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeleteFieldRules");
            var rulesResult = rulesEngineBase.ExecuteRules(field);
            if (!rulesResult.Success)
            {
                Notification notification = new Notification(rulesResult);
                return Json(notification);
            } 
            _repository.SoftDelete(field);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(FieldViewModel input)
        {
            var field = input.Field.EntityId>0? _repository.Find<Field>(input.Field.EntityId):new Field();
            field.Description = input.Field.Description;
            field.Name = input.Field.Name;
            field.Abbreviation= input.Field.Abbreviation;
            field.Size = input.Field.Size;
            field.Status = input.Field.Status;

            if(input.DeleteImage)
            {
                _uploadedFileHandlerService.DeleteFile(field.ImageUrl);
                field.ImageUrl = string.Empty;
            }

            var serverDirectory = "/CustomerPhotos/" + _httpContextAbstractor.GetCompanyIdFromIdentity() + "/Fields";
            field.ImageUrl = _uploadedFileHandlerService.GetUploadedFileUrl(serverDirectory, field.Name);
            var crudManager = _saveEntityService.ProcessSave(field);

            crudManager = _uploadedFileHandlerService.SaveUploadedFile(serverDirectory, field.Name, crudManager);
            var notification = crudManager.Finish();
            return Json(notification,"text/plain");
        }
    }
}