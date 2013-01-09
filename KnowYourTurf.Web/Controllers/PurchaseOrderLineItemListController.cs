using System.Linq;
using System.Web.Mvc;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using FluentNHibernate.Utils;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class PurchaseOrderLineItemListController : AdminControllerBase
    {
        private readonly IRepository _repository;
        private readonly IEntityListGrid<PurchaseOrderLineItem> _purchaseOrderLineItemGrid;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;

        public PurchaseOrderLineItemListController(IRepository repository,
            IEntityListGrid<PurchaseOrderLineItem> purchaseOrderLineItemGrid,
            ISaveEntityService saveEntityService, 
            IDynamicExpressionQuery dynamicExpressionQuery)
        {
            _repository = repository;
            _purchaseOrderLineItemGrid = purchaseOrderLineItemGrid;
            _saveEntityService = saveEntityService;
            _dynamicExpressionQuery = dynamicExpressionQuery;
        }

        public ActionResult ItemList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<PurchaseOrderLineItemListController>(x => x.Items(null)) + "/" + input.EntityId; 
            var model = new ListViewModel()
            {
                gridDef = _purchaseOrderLineItemGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.PURCHASE_ORDER_LINE_ITEMS.ToString(),
                deleteMultipleUrl = UrlContext.GetUrlForAction<PurchaseOrderLineItemListController>(x => x.DeleteMultiple(null)) + "/" + input.EntityId
            };
            return new CustomJsonResult(model);
        }

        public JsonResult Items (GridItemsRequestModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.EntityId);
            if (purchaseOrder == null) return null;
            var items = _dynamicExpressionQuery.PerformQuery(purchaseOrder.LineItems, input.filters);

            if (input.PageSortFilter.SortColumn.IsEmpty()) items = items.OrderBy(x => x.Product.Name);
            var model = _purchaseOrderLineItemGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return new CustomJsonResult(model);
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.EntityId);
            input.EntityIds.Each(x =>
                                     {
                                         var item = purchaseOrder.LineItems.FirstOrDefault(i => i.EntityId == x);
                                         purchaseOrder.RemoveLineItem(item);
                                     });
            var crudManager = _saveEntityService.ProcessSave(purchaseOrder);
            var notification = crudManager.Finish();
            return new CustomJsonResult(notification);
        }

    }   
}