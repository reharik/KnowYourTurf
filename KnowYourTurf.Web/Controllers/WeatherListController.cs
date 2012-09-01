using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

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

        public ActionResult ItemList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<WeatherListController>(x => x.Weathers(null));
            ListViewModel model = new ListViewModel()
            {
                gridDef = _weatherListGrid.GetGridDefinition(url),
                _Title = WebLocalizationKeys.WEATHER.ToString()
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Weathers(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Weather>();
            var gridItemsViewModel = _weatherListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}