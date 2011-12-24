using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class MaterialListController:KYTController
    {
         private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
         private readonly IEntityListGrid<Material> _materialListGrid;

        public MaterialListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Material> materialListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _materialListGrid = materialListGrid;
        }

        public ActionResult MaterialList()
        {
            var url =UrlContext.GetUrlForAction<MaterialListController>(x => x.Materials(null));
            ListViewModel model = new ListViewModel()
            {
                AddUpdateUrl = UrlContext.GetUrlForAction<MaterialController>(x => x.AddUpdate(null)),
                GridDefinition = _materialListGrid.GetGridDefinition(url),
                Title = WebLocalizationKeys.MATERIALS.ToString()
            };
            return View(model);
        }

        public JsonResult Materials(GridItemsRequestModel input)
        {
              var items = _dynamicExpressionQuery.PerformQuery<Material>(input.filters);
            var gridItemsViewModel = _materialListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}