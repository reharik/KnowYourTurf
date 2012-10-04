using System.Linq;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class AdminListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<User> _grid;

        public AdminListController(IDynamicExpressionQuery dynamicExpressionQuery)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _grid = ObjectFactory.Container.GetInstance<IEntityListGrid<User>>("Admins");
        }

        public ActionResult AdminList()
        {
            var url = UrlContext.GetUrlForAction<AdminListController>(x => x.Admins(null));
            ListViewModel model = new ListViewModel()
            {
                gridDef = _grid.GetGridDefinition(url),
                _Title = WebLocalizationKeys.ADMINS.ToString()
            };
            return View(model);
        
        }

        public JsonResult Admins(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<User>(input.filters, x=>x.UserRoles.Any(r=>r.Name==UserType.Administrator.ToString()));
            var gridItemsViewModel = _grid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}