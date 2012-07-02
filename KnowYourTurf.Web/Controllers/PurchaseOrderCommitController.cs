using System.Linq;
using System.Web.Mvc;
using FubuMVC.Core;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class PurchaseOrderCommitController:KYTController
    {
        private readonly IGrid<PurchaseOrderLineItem> _grid;
        private Pager<PurchaseOrderLineItem> _pager;
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

        public ActionResult PurchaseOrderCommit(ViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.EntityId);
            var url = UrlContext.GetUrlForAction<PurchaseOrderCommitController>(x => x.PurchaseOrderLineItems(null))+"?EntityId="+input.EntityId;
            POCommitViewModel model = new POCommitViewModel()
            {   
                ClosePOUrl = UrlContext.GetUrlForAction<PurchaseOrderCommitController>(x=>x.ClosePurchaseOrder(null)),
                PurchaseOrder = purchaseOrder,
                deleteMultipleUrl = UrlContext.GetUrlForAction<PurchaseOrderLineItemListController>(x => x.DeleteMultiple(null)) + "?EntityId=" + purchaseOrder.EntityId,
                gridDef = _receivePurchaseOrderLineItemGrid.GetGridDefinition(url),
                Title = WebLocalizationKeys.COMMIT_PURCHASE_ORDER.ToString()
            };
            return View("PurchaseOrderCommit",model);
        }

        public ActionResult ClosePurchaseOrder(ViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.EntityId);
            purchaseOrder.Completed = true;
            purchaseOrder.LineItems.Where(x => !x.Completed).Each(x => x.Completed = true);
            var crudManager = _saveEntityService.ProcessSave(purchaseOrder);
            var notification = crudManager.Finish();
            notification.RedirectUrl = UrlContext.GetUrlForAction<PurchaseOrderListController>(x => x.ItemList(null));
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PurchaseOrderLineItems(GridItemsRequestModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.EntityId);
            var items = _dynamicExpressionQuery.PerformQuery(purchaseOrder.LineItems,input.filters, x => x.PurchaseOrder.EntityId == input.EntityId && !x.Completed);
            var model = _receivePurchaseOrderLineItemGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}