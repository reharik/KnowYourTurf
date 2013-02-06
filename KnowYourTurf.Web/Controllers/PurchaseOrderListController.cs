using System;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html;
using CC.Core.Html.Grid;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

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
                gridDef = _purchaseOrderListGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.PURCHASE_ORDERS.ToString()
            };
            model.headerButtons.Add("new");
            return new CustomJsonResult(model);
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

            var gridItemsViewModel = _purchaseOrderListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }
    }

}