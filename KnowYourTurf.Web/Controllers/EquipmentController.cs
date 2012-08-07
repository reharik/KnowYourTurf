using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Castle.Components.Validator;
using FluentNHibernate.Utils;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
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

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("EquipmentAddUpdate", new EquipmentViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var equipment = input.EntityId > 0 ? _repository.Find<Equipment>(input.EntityId) : new Equipment();
            var vendors = _selectListItemService.CreateList<Vendor>(x => x.Company, x => x.EntityId, true);
            var model = Mapper.Map<Equipment, EquipmentViewModel>(equipment);
            
            model._VendorEntityIdList = vendors;
            model._Title = WebLocalizationKeys.EQUIPMENT_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<EquipmentController>(x => x.Save(null));
            return Json(model, JsonRequestBehavior.AllowGet);
        }

//        public ActionResult Display(ViewModel input)
//        {
//            var equipment = _repository.Find<Equipment>(input.EntityId);
//            var model = new EquipmentViewModel
//            {
//                Item = equipment,
//                AddUpdateUrl = UrlContext.GetUrlForAction<EquipmentController>(x => x.AddUpdate(null)) + "/" + equipment.EntityId,
//                _Title = WebLocalizationKeys.EQUIPMENT_INFORMATION.ToString()
//            };
//            return PartialView("EquipmentView", model);
//        }

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
            var equipment = input.EntityId > 0 ? _repository.Find<Equipment>(input.EntityId) : new Equipment();
            equipment.Name = input.Name;
            equipment.TotalHours = input.TotalHours;
            equipment.Description = input.Description;
            if (input.DeleteImage)
            {
                _fileHandlerService.DeleteFile(equipment.ImageUrl);
                equipment.ImageUrl = string.Empty;
            }
            if (input.VendorEntityId >0 && (equipment.Vendor == null || equipment.Vendor.EntityId != input.VendorEntityId))
            {
                var vendor = _repository.Find<Vendor>(input.VendorEntityId);
                equipment.SetVendor(vendor);
            }
            if (_fileHandlerService.RequsetHasFile())
            {
                equipment.ImageUrl = _fileHandlerService.SaveAndReturnUrlForFile("CustomerPhotos/Equipment");
            }
            var crudManager = _saveEntityService.ProcessSave(equipment);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }
    }

    public class EquipmentViewModel:ViewModel
    {
        public string _saveUrl { get; set; }
        public IEnumerable<SelectListItem> _VendorEntityIdList { get; set; }

        [ValidateNonEmpty]
        public string Name { get; set; }
        public string Description { get; set; }
        [ValueOfIEnumerable]
        public int VendorEntityId { get; set; }
        [ValidateNonEmpty]
        [ValidateDecimal]
        public int TotalHours { get; set; }
        public string ImageUrl { get; set; }
        public bool DeleteImage { get; set; }
    }

}
