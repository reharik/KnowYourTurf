using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class TaskListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IRepository _repository;

        public TaskListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IRepository repository)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _repository = repository;
        }

        public ActionResult ItemList(ViewModel input)
        {
            var _pendingTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("PendingTasks");
            var url = UrlContext.GetUrlForAction<TaskListController>(x => x.Tasks(null)) + "?RootId=" + input.RootId;
            ListViewModel model = new ListViewModel()
            {
                //AddUpdateUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddUpdate(null)),
                deleteMultipleUrl = UrlContext.GetUrlForAction<TaskController>(x => x.DeleteMultiple(null)),
                gridDef = _pendingTaskGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.TASKS.ToString()
            };
            model.headerButtons.Add("new");
            model.headerButtons.Add("delete");
            return new CustomJsonResult(model);
        }

        public JsonResult Tasks(GridItemsRequestModel input)
        {
            var _pendingTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("PendingTasks");
            var tasks = _repository.Query<Task>(x => x.Field.Site.EntityId == input.RootId && !x.Complete);
            var items = _dynamicExpressionQuery.PerformQuery(tasks, input.filters);
            var gridItemsViewModel = _pendingTaskGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }

        public ActionResult CompletedTasksGrid(ViewModel input)
        {
            var _completedTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("CompletedTasks");
            var url = UrlContext.GetUrlForAction<TaskListController>(x => x.CompletedTasks(null)) + "?RootId=" + input.RootId;
            ListViewModel model = new ListViewModel()
            {
                gridDef = _completedTaskGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId,
                _Title = WebLocalizationKeys.COMPLETED_TASKS.ToString(),

            };
            return new CustomJsonResult(model);
        }
        public JsonResult CompletedTasks(GridItemsRequestModel input)
        {
            var _completedTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("CompletedTasks");
            var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters,x => x.Field.Site.EntityId == input.RootId && x.Complete);
            var gridItemsViewModel = _completedTaskGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }
    }
}
