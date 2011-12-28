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
        private readonly ISaveEntityService _saveEntityService;

        public PurchaseOrderLineItemListController(IRepository repository,
            IEntityListGrid<PurchaseOrderLineItem> purchaseOrderLineItemGrid,
            ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _purchaseOrderLineItemGrid = purchaseOrderLineItemGrid;
            _saveEntityService = saveEntityService;
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
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

    }   
}