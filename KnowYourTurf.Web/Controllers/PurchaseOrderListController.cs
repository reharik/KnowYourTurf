using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class PurchaseOrderListController : AdminControllerBase
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IPurchaseOrderListGrid _purchaseOrderListGrid;

        public PurchaseOrderListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IPurchaseOrderListGrid purchaseOrderListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _purchaseOrderListGrid = purchaseOrderListGrid;
        }


        public ActionResult PurchaseOrderList()
        {
            var url = UrlContext.GetUrlForAction<PurchaseOrderListController>(x => x.PurchaseOrders(null));
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<PurchaseOrderController>(x => x.AddEdit(null)),
                ListDefinition = _purchaseOrderListGrid.GetGridDefinition(url, WebLocalizationKeys.PURCHASE_ORDERS),
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
                    col.AddActionUrlParameters(new Dictionary<string, string> { { "ParentId", v.EntityId.ToString() } });
                }
            };
            _purchaseOrderListGrid.AddColumnModifications(mod);

            var gridItemsViewModel = _purchaseOrderListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}