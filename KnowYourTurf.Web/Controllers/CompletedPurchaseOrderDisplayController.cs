using System;
using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using StructureMap;
using NHibernate.Linq;

namespace KnowYourTurf.Web.Controllers
{
    public class CompletedPurchaseOrderDisplayController : AdminControllerBase
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IRepository _repository;
        private readonly IEntityListGrid<PurchaseOrderLineItem> _purchaseOrderListGrid;

        public CompletedPurchaseOrderDisplayController(IDynamicExpressionQuery dynamicExpressionQuery, IRepository repository)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _repository = repository;
            _purchaseOrderListGrid = ObjectFactory.Container.GetInstance< IEntityListGrid<PurchaseOrderLineItem>>("Completed");
        }

        public ActionResult ItemList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<CompletedPurchaseOrderDisplayController>(x => x.Items(null)) + "/"+input.EntityId;
            ListViewModel model = new ListViewModel()
            {
                gridDef = _purchaseOrderListGrid.GetGridDefinition(url),
                _Title = WebLocalizationKeys.COMPLETED_PURCHASE_ORDERS.ToString()
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Items(GridItemsRequestModel input)
        {
            var po = _repository.Query<PurchaseOrder>(x => x.EntityId == input.EntityId).Fetch(x => x.LineItems).FirstOrDefault();
            var items = _dynamicExpressionQuery.PerformQuery(po.LineItems, input.filters);
            var gridItemsViewModel = _purchaseOrderListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}