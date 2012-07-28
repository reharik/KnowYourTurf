using System;
using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
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

        public PurchaseOrderLineItemController(IRepository repository,
            ISaveEntityService saveEntityService,
            IInventoryService inventoryService,
            IPurchaseOrderLineItemService purchaseOrderLineItemService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _inventoryService = inventoryService;
            _purchaseOrderLineItemService = purchaseOrderLineItemService;
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

            var model = new PurchaseOrderLineItemViewModel
            {
                Item = purchaseOrderLineItem,
                ParentId = input.ParentId
            };
            return PartialView("PurchaseOrderLineItemAddUpdate", model);
        }

        public ActionResult Display(ViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.ParentId);
            var purchaseOrderLineItem = purchaseOrder.LineItems.FirstOrDefault(x => x.EntityId == input.EntityId);
            var model = new PurchaseOrderLineItemViewModel
            {
                Item = purchaseOrderLineItem,
                AddUpdateUrl = UrlContext.GetUrlForAction<PurchaseOrderLineItemController>(x => x.AddUpdate(null)) + "/" + purchaseOrderLineItem.EntityId
            };
            return PartialView("PurchaseOrderLineItemView", model);
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
            var purchaseOrderLineItem = input.Item.EntityId > 0
                                            ? purchaseOrder.LineItems.FirstOrDefault(x=>x.EntityId == input.Item.EntityId)
                                            : new PurchaseOrderLineItem();
            purchaseOrderLineItem.Price = input.Item.Price;
            purchaseOrderLineItem.QuantityOrdered = input.Item.QuantityOrdered;
            purchaseOrderLineItem.SizeOfUnit = input.Item.SizeOfUnit;
            purchaseOrderLineItem.UnitType = input.Item.UnitType;
            purchaseOrderLineItem.Taxable= input.Item.Taxable;
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder,purchaseOrderLineItem);
            var crudManager = _saveEntityService.ProcessSave(purchaseOrder);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReceivePurchaseOrderLineItem(ViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.ParentId);
            var purchaseOrderLineItem = purchaseOrder.LineItems.FirstOrDefault(x => x.EntityId == input.EntityId);
            var model = new PurchaseOrderLineItemViewModel
            {
                Item = purchaseOrderLineItem,
                _Title = WebLocalizationKeys.RECEIVE_PURCHASE_ORDER_ITEM.ToString()
            };
            return PartialView("ReceivePurchaseOrderLineItem", model);
        }

        public ActionResult SaveReceived(PurchaseOrderLineItemViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.ParentId);
            var origionalPurchaseOrderLineItem = purchaseOrder.LineItems.FirstOrDefault(x => x.EntityId == input.EntityId);
            origionalPurchaseOrderLineItem.QuantityOrdered = input.Item.QuantityOrdered;
            origionalPurchaseOrderLineItem.Price = input.Item.Price;
            origionalPurchaseOrderLineItem.TotalReceived = input.Item.TotalReceived;
            origionalPurchaseOrderLineItem.Completed = input.Item.Completed;
            origionalPurchaseOrderLineItem.UnitType = input.Item.UnitType;
            origionalPurchaseOrderLineItem.SizeOfUnit = input.Item.SizeOfUnit;
            var crudManager = _saveEntityService.ProcessSave(purchaseOrder);
            crudManager = _inventoryService.ReceivePurchaseOrderLineItem(origionalPurchaseOrderLineItem,crudManager);
            var notification = crudManager.Finish();
            return Json(notification);
        }
    }
}