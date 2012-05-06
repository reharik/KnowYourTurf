using System.Collections.Generic;
using System.Web.Mvc;
using FluentNHibernate.Utils;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using System.Linq;

namespace KnowYourTurf.Web.Controllers
{
    public class PurchaseOrderController : AdminControllerBase
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISelectListItemService _selectListItemService;
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<BaseProduct> _purchaseOrderSelectorGrid;
        private readonly IEntityListGrid<PurchaseOrderLineItem> _purchaseOrderLineItemGrid;
        private readonly IPurchaseOrderLineItemService _purchaseOrderLineItemService;

        public PurchaseOrderController(IRepository repository, ISaveEntityService saveEntityService, ISelectListItemService selectListItemService, IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<BaseProduct> purchaseOrderSelectorGrid,
            IEntityListGrid<PurchaseOrderLineItem> purchaseOrderLineItemGrid,
            IPurchaseOrderLineItemService purchaseOrderLineItemService
            )
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _selectListItemService = selectListItemService;
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _purchaseOrderSelectorGrid = purchaseOrderSelectorGrid;
            _purchaseOrderLineItemGrid = purchaseOrderLineItemGrid;
            _purchaseOrderLineItemService = purchaseOrderLineItemService;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var purchaseOrder = input.EntityId > 0 ? _repository.Find<PurchaseOrder>(input.EntityId) : new PurchaseOrder();
            var vendors = _selectListItemService.CreateList<Vendor>(x=>x.Company,x=>x.EntityId,true);
            var url = UrlContext.GetUrlForAction<PurchaseOrderController>(x => x.Products(null))+"?Vendor=0";
            var PoliUrl = UrlContext.GetUrlForAction<PurchaseOrderLineItemListController>(x => x.PurchaseOrderLineItems(null)) + "?EntityId=" + purchaseOrder.EntityId;
            var deleteMany = UrlContext.GetUrlForAction<PurchaseOrderLineItemListController>(x => x.DeleteMultiple(null)) + "?EntityId=" + purchaseOrder.EntityId;
            POListViewModel model = new POListViewModel()
            {
                Item = purchaseOrder,
                VendorList = vendors,
                VendorId = purchaseOrder.EntityId > 0 ? purchaseOrder.Vendor.EntityId : 0,
                ReturnUrl = UrlContext.GetUrlForAction<PurchaseOrderListController>(x => x.ItemList(null)),
                CommitUrl = UrlContext.GetUrlForAction<PurchaseOrderCommitController>(x => x.PurchaseOrderCommit(null)),
                DeleteMultipleUrl = deleteMany,
                GridDefinition = _purchaseOrderSelectorGrid.GetGridDefinition(url),
                PoliListDefinition = _purchaseOrderLineItemGrid.GetGridDefinition(PoliUrl),
                Title = WebLocalizationKeys.PURCHASE_ORDER_INFORMATION.ToString()

            };
            return PartialView("PurchaseOrderBuilder", model);
        }

        
        public JsonResult Products(PoSelectorGridItemsRequestModel input)
        {
            var vendor = _repository.Find<Vendor>(input.Vendor);
            var items = _dynamicExpressionQuery.PerformQuery(vendor.Products, input.filters);

            var model = _purchaseOrderSelectorGrid.GetGridItemsViewModel(input.PageSortFilter, items, "productGrid");
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Display(ViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.EntityId);
            var model = new POListViewModel()
            {
                Item = purchaseOrder,
                AddUpdateUrl = UrlContext.GetUrlForAction<PurchaseOrderController>(x => x.AddUpdate(null)) + "/" + purchaseOrder.EntityId
            };
            return PartialView("PurchaseOrderView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.EntityId);
            _repository.HardDelete(purchaseOrder);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<PurchaseOrder>(x);
                _repository.HardDelete(item);
            });
            _repository.Commit();
            return Json(new Notification { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(POSaveViewModel input)
        {
            PurchaseOrder purchaseOrder = input.PurchaseOrderId>0 ? _repository.Find<PurchaseOrder>(input.PurchaseOrderId) : new PurchaseOrder();
            if(input.PurchaseOrderId<=0)
            {
                var vendor = _repository.Find<Vendor>(input.VendorId);
                purchaseOrder.Vendor = vendor;
            }
            var baseProduct = _repository.Find<BaseProduct>(input.EntityId);
            purchaseOrder.AddLineItem(new PurchaseOrderLineItem { Product = baseProduct });

            var crudManager = _saveEntityService.ProcessSave(purchaseOrder);
            var notification = crudManager.Finish();
            notification.EntityId = purchaseOrder.EntityId;
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddItem(ViewModel input)
        {
            var vendor = _repository.Find<Vendor>(input.RootId);
            
            var purchaseOrder = input.ParentId > 0
                                    ? vendor.GetPurchaseOrderInProcess().FirstOrDefault(x => x.EntityId == input.ParentId)
                                    : new PurchaseOrder{Vendor = vendor};
            var baseProduct = _repository.Find<BaseProduct>(input.EntityId);
            var purchaseOrderLineItem = new PurchaseOrderLineItem
                                            {
                                                Product = baseProduct,
                                                PurchaseOrder = purchaseOrder
                                            };

            var model = new PurchaseOrderLineItemViewModel
                                                     {
                                                         Item = purchaseOrderLineItem
                                                     };
            return View(model);
        }

        public ActionResult SaveItem(PurchaseOrderLineItemViewModel input)
        {
            var vendor = _repository.Find<Vendor>(input.RootId);
            var purchaseOrder = input.ParentId > 0
                                    ? vendor.GetPurchaseOrderInProcess().FirstOrDefault(x => x.EntityId == input.ParentId)
                                    : new PurchaseOrder{Vendor = vendor};
            var baseProduct = _repository.Find<BaseProduct>(input.Item.Product.EntityId);
            var newPo = purchaseOrder.EntityId == 0;
            var purchaseOrderLineItem = new PurchaseOrderLineItem
            {
                Product = baseProduct,
                PurchaseOrder = purchaseOrder
            };
            mapItem(purchaseOrderLineItem, input.Item);
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder, purchaseOrderLineItem);
            vendor.AddPurchaseOrder(purchaseOrder);
            
            var crudManager = _saveEntityService.ProcessSave(vendor);
            var notification = crudManager.Finish();
            notification.Data = new {poId = purchaseOrder.EntityId};
            if(newPo)
            {
                notification.Redirect = true;
                notification.RedirectUrl=UrlContext.GetUrlForAction<PurchaseOrderController>(x => x.AddUpdate(null)) + "/" +
                        vendor.EntityId+"?ParentId="+ purchaseOrder.EntityId;
            }
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private void mapItem(PurchaseOrderLineItem item, PurchaseOrderLineItem input)
        {
            item.QuantityOrdered = input.QuantityOrdered;
            item.SizeOfUnit = input.SizeOfUnit;
            item.UnitType = input.UnitType;
            item.Price = input.Price;
            item.Taxable = input.Taxable;
        }
    }

    public class PoSelectorGridItemsRequestModel : GridItemsRequestModel
    {
        public long Vendor { get; set; }
    }

    public class POSaveViewModel:ViewModel
    {
        public IEnumerable<long> ProductIds { get; set; }
        public long VendorId { get; set; }
        public long PurchaseOrderId { get; set; }
    }
}