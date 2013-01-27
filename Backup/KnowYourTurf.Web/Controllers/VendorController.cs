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
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Models.Vendor;
using Status = KnowYourTurf.Core.Enums.Status;

namespace KnowYourTurf.Web.Controllers
{
    public class VendorController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISelectListItemService _selectListItemService;
        private readonly IUpdateCollectionService _updateCollectionService;

        public VendorController(IRepository repository,
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
            return View("VendorAddUpdate", new VendorViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var vendor = input.EntityId > 0 ? _repository.Find<FieldVendor>(input.EntityId) : new FieldVendor();
            var availableChemicals = _repository.FindAll<Chemical>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var selectedChemicals = vendor.GetAllProductsOfType("Chemical").Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var availableFertilizers = _repository.FindAll<Fertilizer>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var selectedFertilizers = vendor.GetAllProductsOfType("Fertilizer").Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var availableMaterials = _repository.FindAll<Material>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var selectedMaterials = vendor.GetAllProductsOfType("Material").Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var states = _selectListItemService.CreateList<State>(true);
            var status = _selectListItemService.CreateList<Status>(true);
            var model = Mapper.Map<FieldVendor,VendorViewModel>(vendor);
            model.Chemicals = new TokenInputViewModel { _availableItems = availableChemicals, selectedItems = selectedChemicals };
            model.Fertilizers = new TokenInputViewModel { _availableItems = availableFertilizers, selectedItems = selectedFertilizers };
            model.Materials = new TokenInputViewModel { _availableItems = availableMaterials, selectedItems = selectedMaterials };
            model._Title = WebLocalizationKeys.VENDOR_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<VendorController>(x => x.Save(null));
            model._StateList = states;
            model._StatusList = status;
            return Json(model,JsonRequestBehavior.AllowGet);
        }
     
        public ActionResult Delete(ViewModel input)
        {
            var vendor = _repository.Find<FieldVendor>(input.EntityId);
            _repository.HardDelete(vendor);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = new Notification { Success = true };
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<FieldVendor>(x);
                if (checkDependencies(item, notification))
                {
                    _repository.HardDelete(item);
                }
            });
            _repository.Commit();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }
        private bool checkDependencies(FieldVendor item, Notification notification)
        {
            var dependantItems = _repository.Query<PurchaseOrder>(x => x.Vendor == item);
            if (dependantItems.Any())
            {
                if (notification.Message.IsEmpty())
                {
                    notification.Success = false;
                    notification.Message = WebLocalizationKeys.COULD_NOT_DELETE_FOR_PURCHASEORDER.ToString();
                }
                return false;
            }
            return true;
        }
        public ActionResult Save(VendorViewModel input)
        {
            var vendor = input.EntityId > 0 ? _repository.Find<FieldVendor>(input.EntityId) : new FieldVendor();
            var newTask = mapToDomain(input, vendor);

            var crudManager = _saveEntityService.ProcessSave(newTask);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private FieldVendor mapToDomain(VendorViewModel input, FieldVendor fieldVendor)
        {
            fieldVendor.Company = input.Company;
            fieldVendor.Fax = input.Fax;
            fieldVendor.Phone = input.Phone;
            fieldVendor.Address1 = input.Address1;
            fieldVendor.Address2 = input.Address2;
            fieldVendor.City = input.City;
            fieldVendor.State = input.State;
            fieldVendor.ZipCode = input.ZipCode;
            fieldVendor.Website = input.Website;
            fieldVendor.Status = input.Status;
            fieldVendor.Notes = input.Notes;
            // concatenate all the ids since they are all of the same base class
            var selected = (input.Chemicals ?? new TokenInputViewModel { selectedItems = new TokenInputDto[] { } })
                .selectedItems.Concat((input.Fertilizers ?? new TokenInputViewModel { selectedItems = new TokenInputDto[] { } }).selectedItems)
                .Concat((input.Materials ?? new TokenInputViewModel { selectedItems = new TokenInputDto[] { } }).selectedItems);

            var ids = new TokenInputViewModel { selectedItems = selected };
            _updateCollectionService.Update(fieldVendor.Products, ids, fieldVendor.AddProduct, fieldVendor.RemoveProduct);
            return fieldVendor;
        }
    }
}