using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using NHibernate.Linq;
using System.Linq;

namespace KnowYourTurf.Web.Controllers
{
    public class TaskListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
       private readonly IEntityListGrid<Task> _taskListGrid;
        private readonly IRepository _repository;

        public TaskListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Task> taskListGrid,
            IRepository repository)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _taskListGrid = taskListGrid;
            _repository = repository;
        }

        public ActionResult ItemList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<TaskListController>(x => x.Tasks(null)) + "?RootId=" + input.RootId;
            ListViewModel model = new ListViewModel()
            {
                //AddUpdateUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddUpdate(null)),
                deleteMultipleUrl = UrlContext.GetUrlForAction<TaskController>(x => x.DeleteMultiple(null)),
                gridDef = _taskListGrid.GetGridDefinition(url),
                _Title = WebLocalizationKeys.TASKS.ToString(),
                searchField = "TaskType.Name"
            };
            model.headerButtons.Add("new");
            model.headerButtons.Add("delete");
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Tasks(GridItemsRequestModel input)
        {
            var tasks = _repository.Query<Task>(x => x.ReadOnlyField.ReadOnlyCategory.EntityId == input.ParentId);
            var items = _dynamicExpressionQuery.PerformQuery(tasks, input.filters);
            var gridItemsViewModel = _taskListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}
