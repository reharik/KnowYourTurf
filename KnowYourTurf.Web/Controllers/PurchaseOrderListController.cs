using System;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class PurchaseOrderListController : AdminControllerBase
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
       private readonly IEntityListGrid<PurchaseOrder> _purchaseOrderListGrid;

        public PurchaseOrderListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<PurchaseOrder> purchaseOrderListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _purchaseOrderListGrid = purchaseOrderListGrid;
        }


        public ActionResult PurchaseOrderList()
        {
            var url = UrlContext.GetUrlForAction<PurchaseOrderListController>(x => x.PurchaseOrders(null));
            ListViewModel model = new ListViewModel()
            {
                AddUpdateUrl = UrlContext.GetUrlForAction<PurchaseOrderController>(x => x.AddEdit(null)),
                GridDefinition = _purchaseOrderListGrid.GetGridDefinition(url),
            };
            return View(model);
        }

        public JsonResult PurchaseOrders(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<PurchaseOrder>(input.filters,x=>x.Completed == false);
            Action<IGridColumn, PurchaseOrder> mod = (c, v) =>
            {
                if (c.GetType() == typeof(ImageButtonColumn<PurchaseOrder>) && c.ColumnIndex == 2
                    || c.GetType() == typeof(ImageButtonColumn<PurchaseOrder>) && c.ColumnIndex == 10)
                {
                    var col = (ImageButtonColumn<PurchaseOrder>)c;
                    col.AddDataToEvent("{ 'ParentId' : "+ v.EntityId+ "}" );
                }
            };
            _purchaseOrderListGrid.AddColumnModifications(mod);

            var gridItemsViewModel = _purchaseOrderListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}