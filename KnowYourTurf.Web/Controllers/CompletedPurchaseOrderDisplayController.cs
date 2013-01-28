using System.Linq;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using StructureMap;
using NHibernate.Linq;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

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
                gridDef = _purchaseOrderListGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.COMPLETED_PURCHASE_ORDERS.ToString()
            };
            return new CustomJsonResult(model);
        }

        public JsonResult Items(GridItemsRequestModel input)
        {
            var po = _repository.Query<PurchaseOrder>(x => x.EntityId == input.EntityId).Fetch(x => x.LineItems).FirstOrDefault();
            var items = _dynamicExpressionQuery.PerformQuery(po.LineItems, input.filters);
            var gridItemsViewModel = _purchaseOrderListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }
    }
}