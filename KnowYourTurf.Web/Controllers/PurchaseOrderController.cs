using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
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

        public PurchaseOrderController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISelectListItemService selectListItemService,
            IDynamicExpressionQuery dynamicExpressionQuery,
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

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("PurchaseOrderBuilder", new POListViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var purchaseOrder = input.EntityId > 0 ? _repository.Find<PurchaseOrder>(input.EntityId) : new PurchaseOrder();
            var vendors = _selectListItemService.CreateList<Vendor>(x=>x.Company,x=>x.EntityId,true);

            POListViewModel model = Mapper.Map<PurchaseOrder, POListViewModel>(purchaseOrder);
            model._VendorEntityIdList = vendors;
            model._VendorProductsUrl = UrlContext.GetUrlForAction<PurchaseOrderController>(x => x.Products(null));
            model._POLIUrl = UrlContext.GetUrlForAction<PurchaseOrderLineItemListController>(x => x.ItemList(null)); 
            model._ReturnUrl = UrlContext.GetUrlForAction<PurchaseOrderListController>(x => x.ItemList(null));
            model._CommitUrl = UrlContext.GetUrlForAction<PurchaseOrderCommitController>(x => x.PurchaseOrderCommit(null));
            model._Title = WebLocalizationKeys.PURCHASE_ORDER_INFORMATION.ToString();
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public ActionResult VendorProductList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<PurchaseOrderController>(x => x.Products(null))+"/"+input.EntityId;
            var model = new ListViewModel()
            {
                gridDef = _purchaseOrderSelectorGrid.GetGridDefinition(url),
                _Title = WebLocalizationKeys.PRODUCTS.ToString(),
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult Products(PoSelectorGridItemsRequestModel input)
        {
            var vendor = _repository.Find<Vendor>(input.EntityId);
            var items = _dynamicExpressionQuery.PerformQuery(vendor.Products, input.filters);

            var model = _purchaseOrderSelectorGrid.GetGridItemsViewModel(input.PageSortFilter, items, "productGrid");
            return Json(model, JsonRequestBehavior.AllowGet);
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
            var purchaseOrderLineItem = new PurchaseOrderLineItem();
            purchaseOrderLineItem.Product = baseProduct;
            purchaseOrder.AddLineItem(purchaseOrderLineItem);

            var crudManager = _saveEntityService.ProcessSave(purchaseOrder);
            var notification = crudManager.Finish();
            notification.EntityId = purchaseOrder.EntityId;
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddItem(ViewModel input)
        {
            var vendor = _repository.Find<Vendor>(input.RootId);
            
            PurchaseOrder purchaseOrder;
            if (input.ParentId > 0)
            {purchaseOrder = vendor.GetPurchaseOrderInProcess().FirstOrDefault(x => x.EntityId == input.ParentId);}
            else
            {
                purchaseOrder = new PurchaseOrder();
                purchaseOrder.Vendor = vendor;
            }
            var baseProduct = _repository.Find<BaseProduct>(input.EntityId);
            var purchaseOrderLineItem = new PurchaseOrderLineItem
                                            {
                                                PurchaseOrder = purchaseOrder
                                            };
            purchaseOrderLineItem.Product = baseProduct;

            var model = new PurchaseOrderLineItemViewModel
                                                     {
                                                         Item = purchaseOrderLineItem
                                                     };
            return View(model);
        }

        public ActionResult SaveItem(PurchaseOrderLineItemViewModel input)
        {
            var vendor = _repository.Find<Vendor>(input.RootId);
            PurchaseOrder purchaseOrder;
            if (input.ParentId > 0)
            {
                purchaseOrder = vendor.GetPurchaseOrderInProcess().FirstOrDefault(x => x.EntityId == input.ParentId);
            }
            else
            {
                purchaseOrder = new PurchaseOrder();
                purchaseOrder.Vendor = vendor;
            }
            var baseProduct = _repository.Find<BaseProduct>(input.Item.Product.EntityId);
            var newPo = purchaseOrder.EntityId == 0;
            var purchaseOrderLineItem = new PurchaseOrderLineItem
            {
                PurchaseOrder = purchaseOrder
            };
            purchaseOrderLineItem.Product = baseProduct;
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
