using System;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models.Equipment;
using KnowYourTurf.Web.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class EquipmentController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IUploadedFileHandlerService _uploadedFileHandlerService;
        private readonly ISessionContext _sessionContext;

        public EquipmentController(IRepository repository,
            ISaveEntityService saveEntityService,
            IUploadedFileHandlerService uploadedFileHandlerService,
            ISessionContext sessionContext)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _uploadedFileHandlerService = uploadedFileHandlerService;
            _sessionContext = sessionContext;
        }

        public ActionResult AddEdit(ViewModel input)
        {
            var equipment = input.EntityId > 0 ? _repository.Find<Equipment>(input.EntityId) : new Equipment();
            var model = new EquipmentViewModel
            {
                Equipment = equipment
            };
            return PartialView("EquipmentAddUpdate", model);
        }

        public ActionResult Display(ViewModel input)
        {
            var equipment = _repository.Find<Equipment>(input.EntityId);
            var model = new EquipmentViewModel
            {
                Equipment = equipment,
                AddEditUrl = UrlContext.GetUrlForAction<EquipmentController>(x => x.AddEdit(null)) + "/" + equipment.EntityId
            };
            return PartialView("EquipmentView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var equipment = _repository.Find<Equipment>(input.EntityId);
            _repository.HardDelete(equipment);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(EquipmentViewModel input)
        {
            var equipment = input.Equipment.EntityId > 0 ? _repository.Find<Equipment>(input.Equipment.EntityId) : new Equipment();
            equipment.Name = input.Equipment.Name;
            equipment.TotalHours = input.Equipment.TotalHours;
            if (input.DeleteImage)
            {
                _uploadedFileHandlerService.DeleteFile(equipment.ImageUrl);
                equipment.ImageUrl = string.Empty;
            }

            var serverDirectory = "/CustomerPhotos/" + _sessionContext.GetCompanyId() + "/Equipment";
            equipment.ImageUrl = _uploadedFileHandlerService.GetUploadedFileUrl(serverDirectory, equipment.Name);
            var crudManager = _saveEntityService.ProcessSave(equipment);
            crudManager = _uploadedFileHandlerService.SaveUploadedFile(serverDirectory, equipment.Name, crudManager);
            var notification = crudManager.Finish();
            return Json(notification, "text/plain");
        }
    }
}