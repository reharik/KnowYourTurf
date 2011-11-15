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
        private readonly IPendingTaskGrid _pendingTaskGrid;
        private readonly ICompletedTaskGrid _completedTaskGrid;

        public EmployeeDashboardController(IRepository repository, IDynamicExpressionQuery dynamicExpressionQuery,
            IPendingTaskGrid pendingTaskGrid,
            ICompletedTaskGrid completedTaskGrid)
        {
            _repository = repository;
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _pendingTaskGrid = pendingTaskGrid;
            _completedTaskGrid = completedTaskGrid;
        }

        public ActionResult ViewEmployee(ViewModel input)
        {
            var employee = _repository.Find<Employee>(input.EntityId);
            var url = UrlContext.GetUrlForAction<EmployeeDashboardController>(x => x.PendingTasks(null)) + "?ParentId=" + input.EntityId;
            var completeUrl = UrlContext.GetUrlForAction<EmployeeDashboardController>(x => x.CompletedTasks(null)) + "?ParentId=" + input.EntityId;
            var model = new EmployeeDashboardViewModel
            {
                //TODO put modficaztions here "Employee"
                Employee = employee,
                AddEditUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddEdit(null)) + "?ParentId=" + input.EntityId+"&From=Employee",
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