using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Enumerations;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using StructureMap;
using NHibernate.Linq;
using Status = KnowYourTurf.Core.Enums.Status;

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

        public ActionResult ViewEmployee_Template(ViewModel input)
        {
            return View("EmployeeDashboard", new UserViewModel());
        }

        public ActionResult ViewEmployee(ViewModel input)
        {
            var entityId = input.EntityId > 0 ? input.EntityId : _sessionContext.GetUserId();
            var employee = _repository.Query<User>(x=>x.EntityId == entityId).Fetch(x=>x.UserLoginInfo).FirstOrDefault();
            var availableUserRoles = _repository.FindAll<UserRole>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var selectedUserRoles = employee.UserRoles != null
                                                    ? employee.UserRoles.Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name })
                                                    : null;

           

            var model = Mapper.Map<User, UserViewModel>(employee);
            model.FileUrl = model.FileUrl.IsNotEmpty() ? model.AddImageSizeToName("thumb") : "";
            model._StateList = _selectListItemService.CreateList<State>();
            model._UserLoginInfoStatusList = _selectListItemService.CreateList<Status>();
            model._Title = WebLocalizationKeys.EMPLOYEE_INFORMATION.ToString();
            model._returnToList = input.EntityId > 0;
            model._pendingGridUrl = UrlContext.GetUrlForAction<EmployeeDashboardController>(x => x.PendingTasksGrid(null)) + "?ParentId=" + entityId;
            model._completedGridUrl = UrlContext.GetUrlForAction<EmployeeDashboardController>(x => x.CompletedTasksGrid(null)) + "?ParentId=" + entityId;
            model._saveUrl = UrlContext.GetUrlForAction<EmployeeController>(x => x.Save(null));
            model.UserRoles = new TokenInputViewModel { _availableItems = availableUserRoles, selectedItems = selectedUserRoles };
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public ActionResult PendingTasksGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EmployeeDashboardController>(x => x.PendingTasks(null)) + "?ParentId=" + input.ParentId;
            ListViewModel model = new ListViewModel()
            {
//                addUpdate = UrlContext.GetUrlForAction<TaskController>(x => x.AddUpdate(null)) + "?ParentId=" + input.ParentId + "&From=Employee",
                gridDef = _pendingTaskGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId
            };
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        public ActionResult CompletedTasksGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EmployeeDashboardController>(x => x.CompletedTasks(null)) + "?ParentId=" + input.ParentId;
            ListViewModel model = new ListViewModel()
            {
                gridDef = _completedTaskGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CompletedTasks(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters, x => x.Complete);
            var employeeItems = items.ToList().Where(x => x.Employees.Any(y => y.EntityId == input.ParentId)).AsQueryable();
            var gridItemsViewModel = _completedTaskGrid.GetGridItemsViewModel(input.PageSortFilter, employeeItems, input.User);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PendingTasks(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters, x => !x.Complete);
            var employeeItems = items.ToList().Where(x => x.Employees.Any(y => y.EntityId == input.ParentId)).AsQueryable();
            var gridItemsViewModel = _pendingTaskGrid.GetGridItemsViewModel(input.PageSortFilter, employeeItems, input.User);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }

    
}