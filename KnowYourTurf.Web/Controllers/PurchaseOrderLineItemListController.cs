using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class PurchaseOrderLineItemListController : AdminControllerBase
    {
        private readonly IRepository _repository;
        private readonly IEntityListGrid<PurchaseOrderLineItem> _purchaseOrderLineItemGrid;

        public PurchaseOrderLineItemListController(IRepository repository, IEntityListGrid<PurchaseOrderLineItem> purchaseOrderLineItemGrid)
        {
            _repository = repository;
            _purchaseOrderLineItemGrid = purchaseOrderLineItemGrid;
        }

        public JsonResult PurchaseOrderLineItems(GridItemsRequestModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.EntityId);
            if (purchaseOrder == null) return null;
            IQueryable<PurchaseOrderLineItem> items = purchaseOrder.LineItems.AsQueryable();
            if (input.PageSortFilter.SortColumn.IsEmpty()) items = items.OrderBy(x => x.Product.Name);
            var model = _purchaseOrderLineItemGrid.GetGridItemsViewModel(input.PageSortFilter, items, "poliGrid");
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}