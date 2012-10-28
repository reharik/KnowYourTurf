using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
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

        public ActionResult ItemList(ViewModel input)
        {
            var url =UrlContext.GetUrlForAction<MaterialListController>(x => x.Materials(null));
            var model = new ListViewModel()
            {
                deleteMultipleUrl = UrlContext.GetUrlForAction<MaterialController>(x => x.DeleteMultiple(null)),
                gridDef = _materialListGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.MATERIALS.ToString()
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Materials(GridItemsRequestModel input)
        {
              var items = _dynamicExpressionQuery.PerformQuery<Material>(input.filters);
              var gridItemsViewModel = _materialListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}