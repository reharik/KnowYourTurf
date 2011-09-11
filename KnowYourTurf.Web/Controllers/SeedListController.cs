using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class SeedListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly ISeedListGrid _seedListGrid;

        public SeedListController(IDynamicExpressionQuery dynamicExpressionQuery,
            ISeedListGrid seedListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _seedListGrid = seedListGrid;
        }

        public ActionResult SeedList()
        {
             var url = UrlContext.GetUrlForAction<SeedListController>(x => x.Seeds(null));
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl =  UrlContext.GetUrlForAction<SeedController>(x => x.AddEdit(null)),
                ListDefinition = _seedListGrid.GetGridDefinition(url, WebLocalizationKeys.SEEDS),
                CrudTitle = WebLocalizationKeys.SEED_INFORMATION.ToString()
            };
            return View(model);

        }

        public JsonResult Seeds(GridItemsRequestModel input)
        {
             var items = _dynamicExpressionQuery.PerformQuery<Seed>(input.filters);
            var gridItemsViewModel = _seedListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}