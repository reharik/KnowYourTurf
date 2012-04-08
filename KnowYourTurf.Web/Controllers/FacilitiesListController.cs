using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
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
            _gridHandlerService = ObjectFactory.Container.GetInstance<IEntityListGrid<User>>("Facilities");
        }

        public ActionResult FacilitiesList()
        {
            var url = UrlContext.GetUrlForAction<FacilitiesListController>(x => x.Facilitiess(null));
            ListViewModel model = new ListViewModel()
            {
                AddUpdateUrl = UrlContext.GetUrlForAction<FacilitiesController>(x => x.Facilities(null)),
                GridDefinition = _gridHandlerService.GetGridDefinition(url),
                Title = WebLocalizationKeys.FACILITIES.ToString()
            };
            return View(model);
        }

        public JsonResult Facilitiess(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<User>(input.filters, x=>x.UserRoles.Any(r=>r.Name==UserType.Facilities.ToString()));
            var gridItemsViewModel = _gridHandlerService.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}