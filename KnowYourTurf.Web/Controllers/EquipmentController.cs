using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IFileHandlerService _fileHandlerService;
        private readonly ISessionContext _sessionContext;
        private readonly ISelectListItemService _selectListItemService;

        public EquipmentController(IRepository repository,
            ISaveEntityService saveEntityService,
            IFileHandlerService fileHandlerService,
            ISessionContext sessionContext, ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _fileHandlerService = fileHandlerService;
            _sessionContext = sessionContext;
            _selectListItemService = selectListItemService;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var equipment = input.EntityId > 0 ? _repository.Find<Equipment>(input.EntityId) : new Equipment();
            var vendors = _selectListItemService.CreateList<Vendor>(x => x.Company, x => x.EntityId, true);
            var model = new EquipmentViewModel
            {
                Item = equipment,
                VendorList = vendors,
                Title = WebLocalizationKeys.EQUIPMENT_INFORMATION.ToString()
            };
            return PartialView("EquipmentAddUpdate", model);
        }

        public ActionResult Display(ViewModel input)
        {
            var equipment = _repository.Find<Equipment>(input.EntityId);
            var model = new EquipmentViewModel
            {
                Item = equipment,
                AddUpdateUrl = UrlContext.GetUrlForAction<EquipmentController>(x => x.AddUpdate(null)) + "/" + equipment.EntityId,
                Title = WebLocalizationKeys.EQUIPMENT_INFORMATION.ToString()
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
        
        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = new Notification { Success = true };
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<Equipment>(x);
                if (checkDependencies(item, notification))
                {
                    _repository.HardDelete(item);
                }
            });
            _repository.Commit();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private bool checkDependencies(Equipment item, Notification notification)
        {
            var tasks = _repository.Query<Task>(x=>x.Equipment.Any(i=>i==item));
            if(tasks.Any())
            {
                if(notification.Message.IsEmpty())
                {
                    notification.Success = false;
                    notification.Message = WebLocalizationKeys.COULD_NOT_DELETE_FOR_TASK.ToString();
                }
                return false;
            }
            return true;
        }

        public ActionResult Save(EquipmentViewModel input)
        {
            var equipment = input.Item.EntityId > 0 ? _repository.Find<Equipment>(input.Item.EntityId) : new Equipment();
            equipment.Name = input.Item.Name;
            equipment.TotalHours = input.Item.TotalHours;
            equipment.Description = input.Item.Description;
            if (input.DeleteImage)
            {
                _fileHandlerService.DeleteFile(equipment.ImageUrl);
                equipment.ImageUrl = string.Empty;
            }
            if (equipment.Vendor == null || equipment.Vendor.EntityId != input.Item.Vendor.EntityId)
            {
                equipment.Vendor = _repository.Find<Vendor>(input.Item.Vendor.EntityId);
            }
            equipment.ImageUrl = _fileHandlerService.SaveAndReturnUrlForFile("CustomerPhotos/Equipment");
            var crudManager = _saveEntityService.ProcessSave(equipment);
            var notification = crudManager.Finish();
            return Json(notification, "text/plain");
        }
    }
}