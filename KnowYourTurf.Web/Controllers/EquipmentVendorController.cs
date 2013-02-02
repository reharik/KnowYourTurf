using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Enumerations;
using CC.Core.Html;
using CC.Core.Services;
using FluentNHibernate.Utils;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Domain.Persistence;
using KnowYourTurf.Web.Models.EquipmentVendor;
using KnowYourTurf.Web.Models.Vendor;
using Status = KnowYourTurf.Core.Enums.Status;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class EquipmentVendorController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISelectListItemService _selectListItemService;
        private readonly IUpdateCollectionService _updateCollectionService;

        public EquipmentVendorController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISelectListItemService selectListItemService,
            IUpdateCollectionService updateCollectionService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _selectListItemService = selectListItemService;
            _updateCollectionService = updateCollectionService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("EquipmentVendorAddUpdate", new EquipmentVendorViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var equipmentVendor = input.EntityId > 0 ? _repository.Find<EquipmentVendor>(input.EntityId) : new EquipmentVendor();
            var status = _selectListItemService.CreateList<Status>(true);
            var states = _selectListItemService.CreateList<State>(true);
            var model = Mapper.Map<EquipmentVendor, EquipmentVendorViewModel>(equipmentVendor);
            model._Title = WebLocalizationKeys.VENDOR_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<EquipmentVendorController>(x => x.Save(null));
            model._StateList = states;
            model._StatusList = status;
            return new CustomJsonResult(model);
        }
     
        public ActionResult Delete(ViewModel input)
        {
            var equipmentVendor = _repository.Find<EquipmentVendor>(input.EntityId);
            _repository.HardDelete(equipmentVendor);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = new Notification { Success = true };
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<EquipmentVendor>(x);
                if (checkDependencies(item, notification))
                {
                    _repository.HardDelete(item);
                }
            });
            _repository.Commit();
            return new CustomJsonResult(notification);
        }
        private bool checkDependencies(EquipmentVendor item, Notification notification)
        {
            var dependantItems = _repository.Query<Equipment>(x => x.EquipmentVendor == item);
            if (dependantItems.Any())
            {
                if (notification.Message.IsEmpty())
                {
                    notification.Success = false;
                    notification.Message = WebLocalizationKeys.COULD_NOT_DELETE_FOR_EQUIPMENT.ToString();
                }
                return false;
            }
            return true;
        }
        public ActionResult Save(EquipmentVendorViewModel input)
        {
            var equipmentVendor = input.EntityId > 0 ? _repository.Find<EquipmentVendor>(input.EntityId) : new EquipmentVendor();
            var newTask = mapToDomain(input, equipmentVendor);

            var crudManager = _saveEntityService.ProcessSave(newTask);
            var notification = crudManager.Finish();
            return new CustomJsonResult(notification);
        }

        private EquipmentVendor mapToDomain(EquipmentVendorViewModel input, EquipmentVendor equipmentVendor)
        {
            equipmentVendor.Client = input.Client;
            equipmentVendor.Fax = input.Fax;
            equipmentVendor.Phone = input.Phone;
            equipmentVendor.Address1 = input.Address1;
            equipmentVendor.Address2 = input.Address2;
            equipmentVendor.City = input.City;
            equipmentVendor.State = input.State;
            equipmentVendor.ZipCode = input.ZipCode;
            equipmentVendor.Website = input.Website;
            equipmentVendor.Status = input.Status;
            equipmentVendor.Notes = input.Notes;
        
            return equipmentVendor;
        }
    }
}