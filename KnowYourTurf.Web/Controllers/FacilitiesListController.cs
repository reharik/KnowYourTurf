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
    public class FacilitiesListController : AdminControllerBase
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
       private readonly IEntityListGrid<User> _gridHandlerService;

        public FacilitiesListController(IDynamicExpressionQuery dynamicExpressionQuery)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _gridHandlerService = ObjectFactory.Container.GetInstance<IEntityListGrid<User>>("AddUpdate");
        }

        public ActionResult ItemList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<FacilitiesListController>(x => x.Facilitiess(null));
            ListViewModel model = new ListViewModel()
            {
                gridDef = _gridHandlerService.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.FACILITIES.ToString()
            };
            model.headerButtons.Add("new");
            model.headerButtons.Add("delete");
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Facilitiess(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<User>(input.filters, x=>x.UserRoles.Any(r=>r.Name==UserType.Facilities.ToString()));
            var gridItemsViewModel = _gridHandlerService.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}