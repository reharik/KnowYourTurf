using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class CalculatorListController:KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Calculator> _calculatorListGrid;

        public CalculatorListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Calculator> calculatorListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _calculatorListGrid = calculatorListGrid;
        }

        public ActionResult ItemList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<CalculatorListController>(x => x.Calculators(null));
            CalculatorListViewModel model = new CalculatorListViewModel()
            {
                CreateATaskUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddUpdate(null))+"?From=Calculator",
                GridDefinition = _calculatorListGrid.GetGridDefinition(url),
            Title = WebLocalizationKeys.CALCULATORS.ToString()
            };
            return View(model);
        }

        public JsonResult Calculators(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Calculator>(input.filters);
            var gridItemsViewModel = _calculatorListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }

    public class CalculatorListViewModel : ListViewModel
    {
        public string CreateATaskUrl { get; set; }
    }
}