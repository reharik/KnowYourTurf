using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class PurchaseOrderLineItemController : AdminControllerBase
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IInventoryService _inventoryService;
        private readonly IPurchaseOrderLineItemService _purchaseOrderLineItemService;
        private readonly ISelectListItemService _selectListItemService;

        public PurchaseOrderLineItemController(IRepository repository,
            ISaveEntityService saveEntityService,
            IInventoryService inventoryService,
            IPurchaseOrderLineItemService purchaseOrderLineItemService,
            ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _inventoryService = inventoryService;
            _purchaseOrderLineItemService = purchaseOrderLineItemService;
            _selectListItemService = selectListItemService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("PurchaseOrderLineItemAddUpdate", new PurchaseOrderLineItemViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            PurchaseOrderLineItem purchaseOrderLineItem;
            if (input.EntityId > 0)
            {
                var purchaseORder = _repository.Find<PurchaseOrder>(input.ParentId);
                purchaseOrderLineItem = purchaseORder.LineItems.FirstOrDefault(x => x.EntityId == input.EntityId);
            }
            else { purchaseOrderLineItem = new PurchaseOrderLineItem(); }

            var model = Mapper.Map<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel>(purchaseOrderLineItem);
            model.EntityId = input.EntityId;
            model.ParentId = input.ParentId;
            model._UnitTypeList = _selectListItemService.CreateList<UnitType>(true);
            model._saveUrl = UrlContext.GetUrlForAction<PurchaseOrderLineItemController>(x => x.Save(null));
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Display_Template(ViewModel input)
        {
            return PartialView("PurchaseOrderLineItemView", new PurchaseOrderLineItemViewModel());
        }

        public ActionResult Display(ViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.ParentId);
            var purchaseOrderLineItem = purchaseOrder.LineItems.FirstOrDefault(x => x.EntityId == input.EntityId);
            var model = Mapper.Map<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel>(purchaseOrderLineItem);
            model.EntityId = input.EntityId;
            model.ParentId = input.ParentId;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(ViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.ParentId);
            var purchaseOrderLineItem = purchaseOrder.LineItems.FirstOrDefault(x => x.EntityId == input.EntityId);
            purchaseOrder.RemoveLineItem(purchaseOrderLineItem);
            var crudManager = _saveEntityService.ProcessSave(purchaseOrder);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(PurchaseOrderLineItemViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.ParentId);
            var purchaseOrderLineItem = input.EntityId > 0
                                            ? purchaseOrder.LineItems.FirstOrDefault(x=>x.EntityId == input.EntityId)
                                            : new PurchaseOrderLineItem();
            purchaseOrderLineItem.Price = input.Price;
            purchaseOrderLineItem.QuantityOrdered = input.QuantityOrdered;
            purchaseOrderLineItem.SizeOfUnit = input.SizeOfUnit;
            purchaseOrderLineItem.UnitType = input.UnitType;
            purchaseOrderLineItem.Taxable= input.Taxable;
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder,purchaseOrderLineItem);
            var crudManager = _saveEntityService.ProcessSave(purchaseOrder);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReceivePurchaseOrderLineItem_Template(ViewModel input)
        {
            return View("ReceivePurchaseOrderLineItem", new PurchaseOrderLineItemViewModel());
        }

        public ActionResult ReceivePurchaseOrderLineItem(ViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.ParentId);
            var purchaseOrderLineItem = purchaseOrder.LineItems.FirstOrDefault(x => x.EntityId == input.EntityId);
            var model = Mapper.Map<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel>(purchaseOrderLineItem);
            model.EntityId = input.EntityId;
            model.ParentId = input.ParentId;
            model._Title = WebLocalizationKeys.RECEIVE_PURCHASE_ORDER_ITEM.ToString();
            model._UnitTypeList = _selectListItemService.CreateList<UnitType>(true);
            model._saveUrl = UrlContext.GetUrlForAction<PurchaseOrderLineItemController>(x => x.SaveReceived(null));
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveReceived(PurchaseOrderLineItemViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.ParentId);
            var origionalPurchaseOrderLineItem = purchaseOrder.LineItems.FirstOrDefault(x => x.EntityId == input.EntityId);
            origionalPurchaseOrderLineItem.QuantityOrdered = input.QuantityOrdered;
            origionalPurchaseOrderLineItem.Price = input.Price;
            origionalPurchaseOrderLineItem.TotalReceived = input.TotalReceived;
            origionalPurchaseOrderLineItem.Completed = input.Completed;
            origionalPurchaseOrderLineItem.UnitType = input.UnitType;
            origionalPurchaseOrderLineItem.SizeOfUnit = input.SizeOfUnit;
            var crudManager = _saveEntityService.ProcessSave(purchaseOrder);
            crudManager = _inventoryService.ReceivePurchaseOrderLineItem(origionalPurchaseOrderLineItem,crudManager);
            var notification = crudManager.Finish();
            return Json(notification);
        }
    }
}