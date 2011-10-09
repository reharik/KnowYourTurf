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
    public class ChemicalListController:KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IChemicalListGrid _chemicalListGrid;

        public ChemicalListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IChemicalListGrid chemicalListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _chemicalListGrid = chemicalListGrid;
        }

        public ActionResult ChemicalList()
        {
            var url = UrlContext.GetUrlForAction<ChemicalListController>(x => x.Chemicals(null));
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<ChemicalController>(x => x.AddEdit(null)),
                ListDefinition = _chemicalListGrid.GetGridDefinition(url, WebLocalizationKeys.CHEMICALS)
            };
            return View(model);
        }

        public JsonResult Chemicals(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Chemical>(input.filters);
            var gridItemsViewModel = _chemicalListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}