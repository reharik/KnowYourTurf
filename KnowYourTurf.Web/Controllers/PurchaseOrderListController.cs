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


        public ActionResult ItemList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<PurchaseOrderListController>(x => x.PurchaseOrders(null));
            ListViewModel model = new ListViewModel()
            {
                deleteMultipleUrl = UrlContext.GetUrlForAction<PurchaseOrderController>(x => x.DeleteMultiple(null)),
                gridDef = _purchaseOrderListGrid.GetGridDefinition(url),
                _Title = WebLocalizationKeys.PURCHASE_ORDERS.ToString()
            };
            model.headerButtons.Add("new");
            return Json(model, JsonRequestBehavior.AllowGet);
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