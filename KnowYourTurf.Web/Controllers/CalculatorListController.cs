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
    public class CalculatorListController:KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly ICalculatorListGrid _calculatorListGrid;

        public CalculatorListController(IDynamicExpressionQuery dynamicExpressionQuery,
            ICalculatorListGrid calculatorListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _calculatorListGrid = calculatorListGrid;
        }

        public ActionResult CalculatorList()
        {
            var url = UrlContext.GetUrlForAction<CalculatorListController>(x => x.Calculators(null));
            CalculatorListViewModel model = new CalculatorListViewModel()
            {
                CreateATaskUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddEdit(null))+"?From=Calculator",
                ListDefinition = _calculatorListGrid.GetGridDefinition(url, WebLocalizationKeys.CALCULATORS),
            Title = WebLocalizationKeys.CALCULATOR.ToString()
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