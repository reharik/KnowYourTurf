using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

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
                gridDef = _calculatorListGrid.GetGridDefinition(url,input.User),
                _Title = WebLocalizationKeys.CALCULATORS.ToString()
            };
            return new CustomJsonResult(model);
        }

        public JsonResult Calculators(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Calculator>(input.filters);
            var gridItemsViewModel = _calculatorListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }
    }

    public class CalculatorListViewModel : ListViewModel
    {
        public string CreateATaskUrl { get; set; }
    }
}