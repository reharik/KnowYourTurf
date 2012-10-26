using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
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
                gridDef = _chemicalListGrid.GetGridDefinition(url,input.User),
                _Title = WebLocalizationKeys.CHEMICALS.ToString()
            };
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Chemicals(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Chemical>(input.filters);
            var gridItemsViewModel = _chemicalListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}