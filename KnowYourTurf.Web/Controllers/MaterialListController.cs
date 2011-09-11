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
    public class MaterialListController:KYTController
    {
         private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IMaterialListGrid _materialListGrid;

        public MaterialListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IMaterialListGrid materialListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _materialListGrid = materialListGrid;
        }

        public ActionResult MaterialList()
        {
            var url =UrlContext.GetUrlForAction<MaterialListController>(x => x.Materials(null));
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<MaterialController>(x => x.AddEdit(null)),
                ListDefinition = _materialListGrid.GetGridDefinition(url, WebLocalizationKeys.MATERIALS),
                CrudTitle = WebLocalizationKeys.MATERIAL_INFORMATION.ToString()
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