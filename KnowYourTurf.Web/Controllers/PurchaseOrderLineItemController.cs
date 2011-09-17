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

        public ActionResult AddEdit(ViewModel input)
        {
            var purchaseOrderLineItem = input.EntityId > 0 ? _repository.Find<PurchaseOrderLineItem>(input.EntityId) : new PurchaseOrderLineItem();
            var model = new PurchaseOrderLineItemViewModel
            {
                PurchaseOrderLineItem = purchaseOrderLineItem,
                ParentId = input.ParentId
            };
            return PartialView("PurchaseOrderLineItemAddUpdate", model);
        }

        public ActionResult Display(ViewModel input)
        {
            var purchaseOrderLineItem = _repository.Find<PurchaseOrderLineItem>(input.EntityId);
            var model = new PurchaseOrderLineItemViewModel
            {
                PurchaseOrderLineItem = purchaseOrderLineItem,
                AddEditUrl = UrlContext.GetUrlForAction<PurchaseOrderLineItemController>(x => x.AddEdit(null)) + "/" + purchaseOrderLineItem.EntityId
            };
            return PartialView("PurchaseOrderLineItemView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var purchaseOrderLineItem = _repository.Find<PurchaseOrderLineItem>(input.EntityId);
            _repository.HardDelete(purchaseOrderLineItem);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(PurchaseOrderLineItemViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.ParentId);
            var purchaseOrderLineItem = input.PurchaseOrderLineItem.EntityId > 0
                                            ? purchaseOrder.GetLineItems().FirstOrDefault(x=>x.EntityId == input.PurchaseOrderLineItem.EntityId)
                                            : new PurchaseOrderLineItem();
            purchaseOrderLineItem.Price = input.PurchaseOrderLineItem.Price;
            purchaseOrderLineItem.QuantityOrdered = input.PurchaseOrderLineItem.QuantityOrdered;
            purchaseOrderLineItem.SizeOfUnit = input.PurchaseOrderLineItem.SizeOfUnit;
            purchaseOrderLineItem.UnitType = input.PurchaseOrderLineItem.UnitType;
            purchaseOrderLineItem.Taxable= input.PurchaseOrderLineItem.Taxable;
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder,purchaseOrderLineItem);
            var crudManager = _saveEntityService.ProcessSave(purchaseOrder);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReceivePurchaseOrderLineItem(ViewModel input)
        {
            var purchaseOrderLineItem = _repository.Find<PurchaseOrderLineItem>(input.EntityId);
            var model = new PurchaseOrderLineItemViewModel
            {
                PurchaseOrderLineItem = purchaseOrderLineItem
            };
            return PartialView("ReceivePurchaseOrderLineItem", model);
        }

        public ActionResult SaveReceived(PurchaseOrderLineItemViewModel input)
        {
            var origionalPurchaseOrderLineItem = _repository.Find<PurchaseOrderLineItem>(input.PurchaseOrderLineItem.EntityId);
            origionalPurchaseOrderLineItem.QuantityOrdered = input.PurchaseOrderLineItem.QuantityOrdered;
            origionalPurchaseOrderLineItem.Price = input.PurchaseOrderLineItem.Price;
            origionalPurchaseOrderLineItem.TotalReceived = input.PurchaseOrderLineItem.TotalReceived;
            origionalPurchaseOrderLineItem.Completed = input.PurchaseOrderLineItem.Completed;
            origionalPurchaseOrderLineItem.UnitType = input.PurchaseOrderLineItem.UnitType;
            origionalPurchaseOrderLineItem.SizeOfUnit = input.PurchaseOrderLineItem.SizeOfUnit;
            var crudManager = _saveEntityService.ProcessSave(origionalPurchaseOrderLineItem);
            crudManager = _inventoryService.ReceivePurchaseOrderLineItem(origionalPurchaseOrderLineItem,crudManager);
            var notification = crudManager.Finish();
            return Json(notification);

        }
    }
}