using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class FertilizerListController:KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Fertilizer> _fertilizerListGrid;

        public FertilizerListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Fertilizer> fertilizerListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _fertilizerListGrid = fertilizerListGrid;
        }

        public ActionResult ItemList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<FertilizerListController>(x => x.Fertilizers(null));
            ListViewModel model = new ListViewModel()
            {
                deleteMultipleUrl= UrlContext.GetUrlForAction<FertilizerController>(x => x.DeleteMultiple(null)),
                gridDef = _fertilizerListGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.FERTILIZERS.ToString()
            };
            model.headerButtons.Add("new");
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Fertilizers(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Fertilizer>(input.filters);
            var gridItemsViewModel = _fertilizerListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}