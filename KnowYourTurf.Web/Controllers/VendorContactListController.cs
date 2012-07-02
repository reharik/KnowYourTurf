using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class VendorContactListController:KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<VendorContact>_vendorContactListGrid;
        private readonly IRepository _repository;

        public VendorContactListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<VendorContact> vendorContactListGrid,
            IRepository repository)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _vendorContactListGrid = vendorContactListGrid;
            _repository = repository;
        }

        public ActionResult VendorContactList(ListViewModel input)
        {
            var vendor = _repository.Find<Vendor>(input.EntityId);
            var url = UrlContext.GetUrlForAction<VendorContactListController>(x => x.VendorContacts(null)) + "?ParentId=" + input.EntityId;
            ListViewModel model = new ListViewModel()
            {
                AddUpdateUrl = UrlContext.GetUrlForAction<VendorContactController>(x => x.AddUpdate(null)) + "?ParentId=" + input.EntityId,
                gridDef = _vendorContactListGrid.GetGridDefinition(url),
                deleteMultipleUrl = UrlContext.GetUrlForAction<VendorContactController>(x => x.DeleteMultiple(null)) + "?ParentId=" + input.EntityId,
                Title = "("+vendor.Company+") "+ WebLocalizationKeys.VENDOR_CONTACTS
            };
            return View(model);
        }

        public JsonResult VendorContacts(GridItemsRequestModel input)
        {
            var vendor = _repository.Find<Vendor>(input.ParentId);
            var items = _dynamicExpressionQuery.PerformQuery(vendor.Contacts,input.filters);
            var gridItemsViewModel = _vendorContactListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}