using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FluentNHibernate.Utils;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Models.Vendor;

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
            var vendor = input.EntityId > 0 ? _repository.Find<Vendor>(input.EntityId) : new Vendor();
            var availableChemicals = _repository.FindAll<Chemical>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var selectedChemicals = vendor.GetAllProductsOfType("Chemical").Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var availableFertilizers = _repository.FindAll<Fertilizer>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var selectedFertilizers = vendor.GetAllProductsOfType("Fertilizer").Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var availableMaterials = _repository.FindAll<Material>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var selectedMaterials = vendor.GetAllProductsOfType("Material").Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var states = _selectListItemService.CreateList<State>(true);
            var status = _selectListItemService.CreateList<Status>(true);
            var model = Mapper.Map<Vendor,VendorViewModel>(vendor);
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
            var vendor = _repository.Find<Vendor>(input.EntityId);
            _repository.HardDelete(vendor);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = new Notification { Success = true };
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<Vendor>(x);
                if (checkDependencies(item, notification))
                {
                    _repository.HardDelete(item);
                }
            });
            _repository.Commit();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }
        private bool checkDependencies(Vendor item, Notification notification)
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
            var vendor = input.EntityId > 0 ? _repository.Find<Vendor>(input.EntityId) : new Vendor();
            var newTask = mapToDomain(input, vendor);

            var crudManager = _saveEntityService.ProcessSave(newTask);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private Vendor mapToDomain(VendorViewModel input, Vendor vendor)
        {
            vendor.Company = input.Company;
            vendor.Fax = input.Fax;
            vendor.Phone = input.Phone;
            vendor.Address1 = input.Address1;
            vendor.Address2 = input.Address2;
            vendor.City = input.City;
            vendor.State = input.State;
            vendor.ZipCode = input.ZipCode;
            vendor.Website = input.Website;
            vendor.Status = input.Status;
            vendor.Notes = input.Notes;
            vendor.ClearProducts();
            _updateCollectionService.Update(vendor.Products, input.Chemicals, vendor.AddProduct, vendor.RemoveProduct, (x, i) => x.InstantiatingType==i.InstantiatingType);
            _updateCollectionService.Update(vendor.Products, input.Fertilizers, vendor.AddProduct, vendor.RemoveProduct, (x, i) => x.InstantiatingType == i.InstantiatingType);
            _updateCollectionService.Update(vendor.Products, input.Materials, vendor.AddProduct, vendor.RemoveProduct, (x, i) =>x.InstantiatingType == i.InstantiatingType);
        
            return vendor;
        }
    }
}