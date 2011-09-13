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
    public class FacilitiesListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IFacilitiesListGrid _gridHandlerService;

        public FacilitiesListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IFacilitiesListGrid gridHandlerService)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _gridHandlerService = gridHandlerService;
        }

        public ActionResult FacilitiesList()
        {
            var url = UrlContext.GetUrlForAction<FacilitiesListController>(x => x.Facilitiess(null));
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<FacilitiesController>(x => x.Facilities(null)),
                ListDefinition = _gridHandlerService.GetGridDefinition(url, WebLocalizationKeys.EMPLOYEES),
            };
            return View(model);
        }

        public JsonResult Facilitiess(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Facilities>(input.filters);
            var gridItemsViewModel = _gridHandlerService.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}