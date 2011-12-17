using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModelAndDTOs;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enumerations;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Areas.Schedule.Grids;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Areas.Schedule.Controllers
{
    public class TrainerListController:KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<User> _trainerListGrid;

        public TrainerListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<User> trainerListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _trainerListGrid = trainerListGrid;
        }

        public ActionResult ItemList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<TrainerListController>(x => x.Trainers(null), AreaName.Schedule);
            var model = new ListViewModel()
            {
                AddUpdateUrl = UrlContext.GetUrlForAction<TrainerController>(x => x.AddUpdate(null), AreaName.Schedule),
                GridDefinition = _trainerListGrid.GetGridDefinition(url)
            };
            return View(model);
        }

        public JsonResult Trainers(GridItemsRequestModel input)
        {
            //TODO find way to deal with string here
            var items = _dynamicExpressionQuery.PerformQuery<User>(input.filters, 
                x=>x.UserRoles.Any(r=>r.Name == "Trainer" ));
            var gridItemsViewModel = _trainerListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}