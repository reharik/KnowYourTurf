using System;
using System.Collections.Generic;
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
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class EmployeeDashboardController:KYTController
    {
        private readonly IRepository _repository;
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Task> _pendingTaskGrid;
        private readonly IEntityListGrid<Task> _completedTaskGrid;
        private readonly ISessionContext _sessionContext;

        public EmployeeDashboardController(IRepository repository, IDynamicExpressionQuery dynamicExpressionQuery,
            ISessionContext sessionContext)
        {
            _repository = repository;
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _pendingTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("PendingTasks");
            _completedTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("CompletedTasks"); ;
            _sessionContext = sessionContext;
        }

        public ActionResult ViewEmployee(ViewModel input)
        {
            var entityId = input.EntityId>0?input.EntityId:_sessionContext.GetUserId();

            var employee = _repository.Find<Employee>(entityId);
            var url = UrlContext.GetUrlForAction<EmployeeDashboardController>(x => x.PendingTasks(null)) + "?ParentId=" + entityId;
            var completeUrl = UrlContext.GetUrlForAction<EmployeeDashboardController>(x => x.CompletedTasks(null)) + "?ParentId=" + entityId;
            var model = new EmployeeDashboardViewModel
            {
                //TODO put modficaztions here "Employee"
                Employee = employee,
                AddEditUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddEdit(null)) + "?ParentId=" + entityId+"&From=Employee",
                ListDefinition = _pendingTaskGrid.GetGridDefinition(url, WebLocalizationKeys.PENDING_TASKS),
                CompletedListDefinition = _completedTaskGrid.GetGridDefinition(completeUrl, WebLocalizationKeys.COMPLETED_TASKS),
               
            };
            return View("EmployeeDashboard", model);
        }

        public JsonResult CompletedTasks(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters, x => x.Complete);
            var employeeItems = items.ToList().Where(x => x.GetEmployees().Any(y => y.EntityId == input.ParentId)).AsQueryable();
            var gridItemsViewModel = _completedTaskGrid.GetGridItemsViewModel(input.PageSortFilter, employeeItems, "completeTaskGrid");
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PendingTasks(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters, x => !x.Complete);
            var employeeItems = items.ToList().Where(x => x.GetEmployees().Any(y => y.EntityId == input.ParentId)).AsQueryable();
            var gridItemsViewModel = _pendingTaskGrid.GetGridItemsViewModel(input.PageSortFilter, employeeItems, "pendingTaskGrid");
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}