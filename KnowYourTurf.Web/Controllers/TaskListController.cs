using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class TaskListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
       private readonly IEntityListGrid<Task> _taskListGrid;

        public TaskListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Task> taskListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _taskListGrid = taskListGrid;
        }

        public ActionResult TaskList()
        {
            var url = UrlContext.GetUrlForAction<TaskListController>(x => x.Tasks(null));
            ListViewModel model = new ListViewModel()
            {
                AddUpdateUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddEdit(null)),
                GridDefinition = _taskListGrid.GetGridDefinition(url)
            };
            return View(model);
        }

        public JsonResult Tasks(GridItemsRequestModel input)
        {
               var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters);
            var gridItemsViewModel = _taskListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}