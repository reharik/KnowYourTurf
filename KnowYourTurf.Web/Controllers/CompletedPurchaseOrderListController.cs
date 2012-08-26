using System;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class CompletedPurchaseOrderListController : AdminControllerBase
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
       private readonly IEntityListGrid<PurchaseOrder> _purchaseOrderListGrid;

        public CompletedPurchaseOrderListController(IDynamicExpressionQuery dynamicExpressionQuery)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _purchaseOrderListGrid = ObjectFactory.Container.GetInstance< IEntityListGrid<PurchaseOrder>>("Completed");
        }

        public ActionResult ItemList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<CompletedPurchaseOrderListController>(x => x.PurchaseOrdersCompleted(null));
            ListViewModel model = new ListViewModel()
            {
                gridDef = _purchaseOrderListGrid.GetGridDefinition(url),
                _Title = WebLocalizationKeys.COMPLETED_PURCHASE_ORDERS.ToString()
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PurchaseOrdersCompleted(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<PurchaseOrder>(input.filters, x => x.Completed);
            var gridItemsViewModel = _purchaseOrderListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}