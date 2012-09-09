﻿using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using NHibernate.Linq;
using System.Linq;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
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
                gridDef = _pendingTaskGrid.GetGridDefinition(url),
                _Title = WebLocalizationKeys.TASKS.ToString(),
                searchField = "TaskType.Name"
            };
            model.headerButtons.Add("new");
            model.headerButtons.Add("delete");
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Tasks(GridItemsRequestModel input)
        {
            var _pendingTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("PendingTasks");
            var tasks = _repository.Query<Task>(x => x.Field.Category.EntityId == input.RootId && !x.Complete);
            var items = _dynamicExpressionQuery.PerformQuery(tasks, input.filters);
            var gridItemsViewModel = _pendingTaskGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CompletedTasksGrid(ViewModel input)
        {
            var _completedTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("CompletedTasks");
            var url = UrlContext.GetUrlForAction<TaskListController>(x => x.CompletedTasks(null)) + "?RootId=" + input.RootId;
            ListViewModel model = new ListViewModel()
            {
                gridDef = _completedTaskGrid.GetGridDefinition(url),
                ParentId = input.ParentId,
                _Title = WebLocalizationKeys.COMPLETED_TASKS.ToString(),

            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CompletedTasks(GridItemsRequestModel input)
        {
            var _completedTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("CompletedTasks");
            var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters,x => x.Field.Category.EntityId == input.RootId && x.Complete);
            var gridItemsViewModel = _completedTaskGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}
