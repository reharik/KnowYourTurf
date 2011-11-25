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
    public class WeatherListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
       private readonly IEntityListGrid<Weather> _weatherListGrid;

        public WeatherListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Weather> weatherListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _weatherListGrid = weatherListGrid;
        }

        public ActionResult WeatherList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<WeatherListController>(x => x.Weathers(null));
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<WeatherController>(x => x.AddEdit(null)),
                ListDefinition = _weatherListGrid.GetGridDefinition(url, WebLocalizationKeys.WEATHER)
            };
            return View(model);
        }

        public JsonResult Weathers(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Weather>();
            var gridItemsViewModel = _weatherListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}