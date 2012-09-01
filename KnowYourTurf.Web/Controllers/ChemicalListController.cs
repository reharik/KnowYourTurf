using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class ChemicalListController:KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Chemical> _chemicalListGrid;

        public ChemicalListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Chemical> chemicalListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _chemicalListGrid = chemicalListGrid;
        }

        public ActionResult ItemList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<ChemicalListController>(x => x.Chemicals(null));
            ListViewModel model = new ListViewModel()
            {
                deleteMultipleUrl = UrlContext.GetUrlForAction<ChemicalController>(x => x.DeleteMultiple(null)),
                gridDef = _chemicalListGrid.GetGridDefinition(url),
                _Title = WebLocalizationKeys.CHEMICALS.ToString()
            };
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Chemicals(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Chemical>(input.filters);
            var gridItemsViewModel = _chemicalListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}