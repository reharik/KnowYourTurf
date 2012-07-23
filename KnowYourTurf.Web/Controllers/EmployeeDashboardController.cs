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
        private readonly ISelectListItemService _selectListItemService;
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Task> _pendingTaskGrid;
        private readonly IEntityListGrid<Task> _completedTaskGrid;
        private readonly ISessionContext _sessionContext;

        public EmployeeDashboardController(IRepository repository, ISelectListItemService selectListItemService, IDynamicExpressionQuery dynamicExpressionQuery,
            ISessionContext sessionContext)
        {
            _repository = repository;
            _selectListItemService = selectListItemService;
            _dynamicExpressionQuery = dynamicExpressionQuery;
            //completed used for pending so that you can't edit on employee page
            _pendingTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("PendingTasks");
            _completedTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("CompletedTasks");
            _sessionContext = sessionContext;
        }

        public ActionResult ViewEmployee(ViewModel input)
        {
//            long entityId;
//            entityId = input.EntityId > 0 ? input.EntityId : _sessionContext.GetUserId();
//            var employee = _repository.Find<User>(entityId);
//            var availableUserRoles = _repository.FindAll<UserRole>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name});
//            var selectedUserRoles = employee.UserRoles != null
//                                                    ? employee.UserRoles.Select(x =>new TokenInputDto{id = x.EntityId.ToString(), name = x.Name})
//                                                    : null;

            var model = new EmployeeDashboardViewModel
            {
                //TODO put modficaztions here "Employee"
//                EmployeeId = employee.EmployeeId,
//                FirstName = employee.FirstName,
//                LastName = employee.LastName,
//                EmergencyContact = employee.EmergencyContact,
//                EmergencyContactPhone= employee.EmergencyContactPhone,
//                UserLoginInfo = new UserLoginInfoViewModel
//                                    {
//                                        Password = employee.UserLoginInfo.Password,
//                                        Status = employee.UserLoginInfo.Status
//                                    },
//                Email = employee.Email,
//                PhoneMobile = employee.PhoneMobile,
//                ImageUrl = employee.ImageUrl,
//                Address1 = employee.Address1,
//                Address2 = employee.Address2,
//                City = employee.City,
//                State = employee.State,
//                ZipCode = employee.ZipCode,
//                Notes = employee.Notes,
            };
            return View("EmployeeDashboard", model);
        }

        public ActionResult ViewEmployee_json(ViewModel input)
        {
            var entityId = input.EntityId > 0 ? input.EntityId : _sessionContext.GetUserId();
            var employee = _repository.Find<User>(entityId);
//            var availableUserRoles = _repository.FindAll<UserRole>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
//            var selectedUserRoles = employee.UserRoles != null
//                                                    ? employee.UserRoles.Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name })
//                                                    : null;

            var model = new EmployeeDashboardViewModel
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                EmergencyContact = employee.EmergencyContact,
                EmergencyContactPhone = employee.EmergencyContactPhone,
                UserLoginInfo = new UserLoginInfoViewModel
                {
                    Password = employee.UserLoginInfo.Password,
                    Status = employee.UserLoginInfo.Status,
                    StatusList = _selectListItemService.CreateList<Status>()
                },
                Email = employee.Email,
                PhoneMobile = employee.PhoneMobile,
                ImageUrl = employee.ImageUrl,
                Address1 = employee.Address1,
                Address2 = employee.Address2,
                City = employee.City,
                State = employee.State,
                StateList = _selectListItemService.CreateList<State>(),
                ZipCode = employee.ZipCode,
                Notes = employee.Notes,
                Title = WebLocalizationKeys.EMPLOYEE_INFORMATION.ToString(),
                ReturnToList = input.EntityId > 0,
                PendingGridUrl = UrlContext.GetUrlForAction<EmployeeDashboardController>(x => x.PendingTasksGrid(null)) + "?ParentId=" + entityId,
                CompletedGridUrl = UrlContext.GetUrlForAction<EmployeeDashboardController>(x => x.CompletedTasksGrid(null)) + "?ParentId=" + entityId,
                SaveUrl = UrlContext.GetUrlForAction<EmployeeController>(x=>x.Save(null))
            };
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public ActionResult PendingTasksGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EmployeeDashboardController>(x => x.PendingTasks(null)) + "?ParentId=" + input.ParentId;
            ListViewModel model = new ListViewModel()
            {
//                addUpdate = UrlContext.GetUrlForAction<TaskController>(x => x.AddUpdate(null)) + "?ParentId=" + input.ParentId + "&From=Employee",
                gridDef = _pendingTaskGrid.GetGridDefinition(url),
                ParentId = input.ParentId
            };
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        public ActionResult CompletedTasksGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EmployeeDashboardController>(x => x.CompletedTasks(null)) + "?ParentId=" + input.ParentId;
            ListViewModel model = new ListViewModel()
            {
                gridDef = _completedTaskGrid.GetGridDefinition(url),
                ParentId = input.ParentId
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CompletedTasks(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters, x => x.Complete);
            var employeeItems = items.ToList().Where(x => x.Employees.Any(y => y.EntityId == input.ParentId)).AsQueryable();
            var gridItemsViewModel = _completedTaskGrid.GetGridItemsViewModel(input.PageSortFilter, employeeItems);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PendingTasks(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters, x => !x.Complete);
            var employeeItems = items.ToList().Where(x => x.Employees.Any(y => y.EntityId == input.ParentId)).AsQueryable();
            var gridItemsViewModel = _pendingTaskGrid.GetGridItemsViewModel(input.PageSortFilter, employeeItems);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}