using System.Linq;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using StructureMap;
using CC.Core;

namespace KnowYourTurf.Web.Controllers
{
    public class PurchaseOrderCommitController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<PurchaseOrderLineItem> _receivePurchaseOrderLineItemGrid;

        public PurchaseOrderCommitController(IRepository repository,
            ISaveEntityService saveEntityService,
            IDynamicExpressionQuery dynamicExpressionQuery)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _receivePurchaseOrderLineItemGrid = ObjectFactory.Container.GetInstance < IEntityListGrid<PurchaseOrderLineItem>>("Recieve");
        }

        public ActionResult PurchaseOrderCommit_Template(ViewModel input)
        {
            return View("PurchaseOrderCommit", new POCommitViewModel());
        }

        public ActionResult PurchaseOrderCommit(ViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.EntityId);
            var model = new POCommitViewModel()
            {   
                EntityId = input.EntityId,
                _POLIUrl = UrlContext.GetUrlForAction<PurchaseOrderCommitController>(x=>x.PurchaseOrderLineItemList(null))+"/"+input.EntityId,
                VendorCompany = purchaseOrder.FieldVendor.Company,
                _ClosePOUrl = UrlContext.GetUrlForAction<PurchaseOrderCommitController>(x => x.ClosePurchaseOrder(null)) + "/" + input.EntityId,
                _Title = WebLocalizationKeys.COMMIT_PURCHASE_ORDER.ToString()
            };
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClosePurchaseOrder(ViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.EntityId);
            purchaseOrder.Completed = true;
            purchaseOrder.LineItems.Where(x => !x.Completed).ForEachItem(x => x.Completed = true);
            var crudManager = _saveEntityService.ProcessSave(purchaseOrder);
            var notification = crudManager.Finish();
            notification.RedirectUrl = UrlContext.GetUrlForAction<PurchaseOrderListController>(x => x.ItemList(null));
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PurchaseOrderLineItemList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<PurchaseOrderCommitController>(x => x.PurchaseOrderLineItems(null)) + "/" + input.EntityId;
            var model = new ListViewModel()
            {
                gridDef = _receivePurchaseOrderLineItemGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.PURCHASE_ORDER_LINE_ITEMS.ToString(),
                deleteMultipleUrl = UrlContext.GetUrlForAction<PurchaseOrderLineItemListController>(x => x.DeleteMultiple(null)) + "?EntityId=" + input.EntityId,
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PurchaseOrderLineItems(GridItemsRequestModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.EntityId);
            var items = _dynamicExpressionQuery.PerformQuery(purchaseOrder.LineItems,input.filters, x=> !x.Completed);
            var model = _receivePurchaseOrderLineItemGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}