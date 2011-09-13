using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class AdminListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IAdminListGrid _gridHandlerService;

        public AdminListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IAdminListGrid gridHandlerService)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _gridHandlerService = gridHandlerService;
        }

        public ActionResult AdminList()
        {
            var url = UrlContext.GetUrlForAction<AdminListController>(x => x.Admins(null));
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<AdminController>(x => x.Admin(null)),
                ListDefinition = _gridHandlerService.GetGridDefinition(url, WebLocalizationKeys.ADMINS),
            };
            return View(model);
        }

        public JsonResult Admins(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Administrator>(input.filters);
            var gridItemsViewModel = _gridHandlerService.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}