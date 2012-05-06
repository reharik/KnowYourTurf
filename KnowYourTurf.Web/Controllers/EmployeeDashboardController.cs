using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
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
            _completedTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("CompletedTasks");
            _sessionContext = sessionContext;
        }

        public ActionResult ViewEmployee(ViewModel input)
        {
            long entityId;
            entityId = input.EntityId > 0 ? input.EntityId : _sessionContext.GetUserId();
            var employee = _repository.Find<User>(entityId);
            
            var availableUserRoles = _repository.FindAll<UserRole>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name});
            IEnumerable<TokenInputDto> selectedUserRoles;
            if (employee.UserRoles != null)
            {
                selectedUserRoles = employee.UserRoles.Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            }
            else
            {
                selectedUserRoles = null;
            }

            var url = UrlContext.GetUrlForAction<EmployeeDashboardController>(x => x.PendingTasks(null)) + "?ParentId=" + entityId;
            var completeUrl = UrlContext.GetUrlForAction<EmployeeDashboardController>(x => x.CompletedTasks(null)) + "?ParentId=" + entityId;
            var model = new EmployeeDashboardViewModel
            {
                //TODO put modficaztions here "Employee"
                Item = employee,
                AvailableItems = availableUserRoles,
                SelectedItems = selectedUserRoles,
                AddUpdateUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddUpdate(null)) + "?ParentId=" + entityId+"&From=Employee",
                GridDefinition = _pendingTaskGrid.GetGridDefinition(url),
                CompletedListDefinition = _completedTaskGrid.GetGridDefinition(completeUrl),
                EmployeeListUrl = UrlContext.GetUrlForAction<EmployeeListController>(x=>x.ItemList(null)),
                DeleteMultipleUrl = UrlContext.GetUrlForAction<TaskController>(x=>x.DeleteMultiple(null)),
                Title = WebLocalizationKeys.EMPLOYEE_INFORMATION.ToString(),
                ReturnToList = input.EntityId>0
            };
            return View("EmployeeDashboard", model);
        }

        public JsonResult CompletedTasks(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters, x => x.Complete);
            var employeeItems = items.ToList().Where(x => x.Employees.Any(y => y.EntityId == input.ParentId)).AsQueryable();
            var gridItemsViewModel = _completedTaskGrid.GetGridItemsViewModel(input.PageSortFilter, employeeItems, "completeTaskGrid");
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PendingTasks(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters, x => !x.Complete);
            var employeeItems = items.ToList().Where(x => x.Employees.Any(y => y.EntityId == input.ParentId)).AsQueryable();
            var gridItemsViewModel = _pendingTaskGrid.GetGridItemsViewModel(input.PageSortFilter, employeeItems, "pendingTaskGrid");
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}