using System.Web.Mvc;
using AutoMapper;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using FluentNHibernate.Utils;
using KnowYourTurf.Core.Domain;
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
        private readonly IPurchaseOrderLineItemService _purchaseOrderLineItemService;

        public PurchaseOrderController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISelectListItemService selectListItemService,
            IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<BaseProduct> purchaseOrderSelectorGrid,
            IPurchaseOrderLineItemService purchaseOrderLineItemService
            )
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _selectListItemService = selectListItemService;
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _purchaseOrderSelectorGrid = purchaseOrderSelectorGrid;
            _purchaseOrderLineItemService = purchaseOrderLineItemService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("PurchaseOrderBuilder", new POListViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var purchaseOrder = input.EntityId > 0 ? _repository.Find<PurchaseOrder>(input.EntityId) : new PurchaseOrder();
            var vendors = _selectListItemService.CreateList<FieldVendor>(x=>x.Company,x=>x.EntityId,true);

            POListViewModel model = Mapper.Map<PurchaseOrder, POListViewModel>(purchaseOrder);
            model._VendorEntityIdList = vendors;
            model._vendorProductsUrl = UrlContext.GetUrlForAction<PurchaseOrderController>(x => x.VendorProductList(null));
            model._POLIUrl = UrlContext.GetUrlForAction<PurchaseOrderLineItemListController>(x => x.ItemList(null)); 
            model._commitPOUrl = UrlContext.GetUrlForAction<PurchaseOrderCommitController>(x => x.PurchaseOrderCommit(null));
            model._addToOrderUrl = UrlContext.GetUrlForAction<PurchaseOrderController>(x => x.AddItemToPO(null));
            model._removePOLItemUrl = UrlContext.GetUrlForAction<PurchaseOrderLineItemController>(x => x.Delete(null));
            model._editPOLItemUrl = UrlContext.GetUrlForAction<PurchaseOrderLineItemController>(x => x.AddUpdate(null));
            model._editPOLItemTitle = WebLocalizationKeys.PURCHASE_ORDER_LINE_ITEM.ToString();
            model._Title = WebLocalizationKeys.PURCHASE_ORDER_INFORMATION.ToString();
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public ActionResult VendorProductList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<PurchaseOrderController>(x => x.Products(null))+"/"+input.EntityId;
            var model = new ListViewModel()
            {
                gridDef = _purchaseOrderSelectorGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.PRODUCTS.ToString(),
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult Products(PoSelectorGridItemsRequestModel input)
        {
            var vendor = _repository.Find<FieldVendor>(input.EntityId);
            var items = _dynamicExpressionQuery.PerformQuery(vendor.Products, input.filters);

            var model = _purchaseOrderSelectorGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
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

        public JsonResult AddItemToPO(PurchaseOrderLineItemViewModel input)
        {
            var vendor = _repository.Find<FieldVendor>(input.RootId);
            PurchaseOrder purchaseOrder;
            if (input.ParentId > 0)
            {
                purchaseOrder = vendor.GetPurchaseOrderInProcess().FirstOrDefault(x => x.EntityId == input.ParentId);
            }
            else
            {
                purchaseOrder = new PurchaseOrder {Vendor = vendor};
                vendor.AddPurchaseOrder(purchaseOrder);
            }
            var baseProduct = _repository.Find<BaseProduct>(input.EntityId);
            var purchaseOrderLineItem = new PurchaseOrderLineItem {Product = baseProduct};
            mapItem(purchaseOrderLineItem, input);
            purchaseOrder.AddLineItem(purchaseOrderLineItem);

            var crudManager = _saveEntityService.ProcessSave(vendor);
            var notification = crudManager.Finish();
            notification.EntityId = purchaseOrder.EntityId;
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        /// ?????? ///

//        public ActionResult AddItem(ViewModel input)
//        {
//            var FieldVendor = _repository.Find<FieldVendor>(input.RootId);
//            
//            PurchaseOrder purchaseOrder;
//            purchaseOrder = input.ParentId > 0 ? FieldVendor.GetPurchaseOrderInProcess().FirstOrDefault(x => x.EntityId == input.ParentId) : new PurchaseOrder { FieldVendor = FieldVendor };
//            var baseProduct = _repository.Find<BaseProduct>(input.EntityId);
//            var purchaseOrderLineItem = new PurchaseOrderLineItem
//                                            {
//                                                PurchaseOrder = purchaseOrder
//                                            };
//            purchaseOrderLineItem.Product = baseProduct;
//
//            var model = new PurchaseOrderLineItemViewModel
//                                                     {
//                                                         Item = purchaseOrderLineItem
//                                                     };
//            return View(model);
//        }

//        public ActionResult SaveItem(PurchaseOrderLineItemViewModel input)
//        {
//
//            var baseProduct = _repository.Find<BaseProduct>(input.Item.Product.EntityId);
//            var newPo = purchaseOrder.EntityId == 0;
//            var purchaseOrderLineItem = new PurchaseOrderLineItem
//            {
//                PurchaseOrder = purchaseOrder
//            };
//            purchaseOrderLineItem.Product = baseProduct;
//            mapItem(purchaseOrderLineItem, input.Item);
//            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder, purchaseOrderLineItem);
//            FieldVendor.AddPurchaseOrder(purchaseOrder);
//            
//            var crudManager = _saveEntityService.ProcessSave(FieldVendor);
//            var notification = crudManager.Finish();
//            notification.Data = new {poId = purchaseOrder.EntityId};
//            if(newPo)
//            {
//                notification.Redirect = true;
//                notification.RedirectUrl=UrlContext.GetUrlForAction<PurchaseOrderController>(x => x.AddUpdate(null)) + "/" +
//                        FieldVendor.EntityId+"?ParentId="+ purchaseOrder.EntityId;
//            }
//            return Json(notification, JsonRequestBehavior.AllowGet);
//        }

        private void mapItem(PurchaseOrderLineItem item, PurchaseOrderLineItemViewModel input)
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
        public int Vendor { get; set; }
    }
}
