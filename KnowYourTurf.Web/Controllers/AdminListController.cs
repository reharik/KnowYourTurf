using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class AdminListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Administrator> _grid;

        public AdminListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Administrator> grid )
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _grid = grid;
        }

        public ActionResult AdminList()
        {
            var url = UrlContext.GetUrlForAction<AdminListController>(x => x.Admins(null));
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<AdminController>(x => x.Admin(null)),
                ListDefinition = _grid.GetGridDefinition(url),
            };
            return View(model);
        
        }

        public JsonResult Admins(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Administrator>(input.filters);
            var gridItemsViewModel = _grid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}