using CC.Security.Interfaces;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Security;

namespace DBFluentMigration.Iteration_0
{
    public class CreateInitialPermissions
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IPermissions _permissions;

        public CreateInitialPermissions(IAuthorizationRepository authorizationRepository, 
            IPermissions permissions)
        {
            _authorizationRepository = authorizationRepository;
            _permissions = permissions;
        }

        public void Update()
        {
            GrantAdminPermissions();
            GrantFacilitiesPermissions();
            GrantEmployeePermissions();
        }

        public void GrantAdminPermissions()
        {
            var operations = ((CustomAuthorizationRepository)_authorizationRepository).GetAllOperations();
            foreach (var operation in operations)
            {
                _permissions.CreatePermission(UserType.Administrator, operation.Name, 10);
            }
        }

        public void GrantFacilitiesPermissions()
        {
            _permissions.CreateControllerPermission("EventController", UserType.Facilities);
            _permissions.CreateControllerPermission("EventCalendarController", UserType.Facilities);
            _permissions.CreateControllerPermission("KnowYourTurfController", UserType.Facilities);
            _permissions.CreateControllerPermission("OrthogonalController", UserType.Facilities);
            
            _permissions.CreateMenuPermission(UserType.Employee, "Events");
        }

        public void GrantEmployeePermissions()
        {
            _permissions.CreateControllerPermission("KnowYourTurfController", UserType.Employee);
            _permissions.CreateControllerPermission("OrthogonalController", UserType.Employee);
            _permissions.CreateControllerPermission("CalculatorController", UserType.Employee);
            _permissions.CreateControllerPermission("CalculatorListController", UserType.Employee);
            _permissions.CreateControllerPermission("EmployeeDashboardController", UserType.Employee);
            _permissions.CreateControllerPermission("EmployeeController", UserType.Employee);
            _permissions.CreateControllerPermission("EquipmentController", UserType.Employee);
            _permissions.CreateControllerPermission("EquipmentListController", UserType.Employee);
            _permissions.CreateControllerPermission("EventController", UserType.Employee);
            _permissions.CreateControllerPermission("EventCalendarController", UserType.Employee);
            _permissions.CreateControllerPermission("FieldController", UserType.Employee);
            _permissions.CreateControllerPermission("FieldDashboardController", UserType.Employee);
            _permissions.CreateControllerPermission("FieldListController", UserType.Employee);
            _permissions.CreateControllerPermission("ForumController", UserType.Employee);
            _permissions.CreateControllerPermission("InventoryListController", UserType.Employee);
            _permissions.CreateControllerPermission("PhotoController", UserType.Employee);
            _permissions.CreateControllerPermission("DocumentController", UserType.Employee);
            _permissions.CreateControllerPermission("PurchaseOrderCommitController", UserType.Employee);
            _permissions.CreateControllerPermission("PurchaseOrderController", UserType.Employee);
            _permissions.CreateControllerPermission("PurchaseOrderLineItemController", UserType.Employee);
            _permissions.CreateControllerPermission("PurchaseOrderListController", UserType.Employee);
            _permissions.CreateControllerPermission("TaskController", UserType.Employee);
            _permissions.CreateControllerPermission("TaskCalendarController", UserType.Employee);
            _permissions.CreateControllerPermission("TaskListController", UserType.Employee);
            _permissions.CreateControllerPermission("VendorContactController", UserType.Employee);
            _permissions.CreateControllerPermission("VendorContactListController", UserType.Employee);
            _permissions.CreateControllerPermission("VendorController", UserType.Employee);
            _permissions.CreateControllerPermission("VendorListController", UserType.Employee);
            _permissions.CreateControllerPermission("WeatherController", UserType.Employee);
            _permissions.CreateControllerPermission("WeatherListController", UserType.Employee);
            _permissions.CreateControllerPermission("EquipmentDashboardController", UserType.Employee);
            _permissions.CreateControllerPermission("EquipmentTaskCalendarController", UserType.Employee);
            _permissions.CreateControllerPermission("EquipmentTaskController", UserType.Employee);
            _permissions.CreateControllerPermission("EquipmentTaskListController", UserType.Employee);

            _permissions.CreateMenuPermission(UserType.Employee, "EquipmentTasks");
            _permissions.CreateMenuPermission(UserType.Employee, "EquipmentTasksLists");
            _permissions.CreateMenuPermission(UserType.Employee, "EquipmentTasks");
            _permissions.CreateMenuPermission(UserType.Employee, "CompletedEquipmentTasks");
            _permissions.CreateMenuPermission(UserType.Employee, "EquipmentTaskCalendar");
            _permissions.CreateMenuPermission(UserType.Employee, "Home");
            _permissions.CreateMenuPermission(UserType.Employee, "Fields");
            _permissions.CreateMenuPermission(UserType.Employee, "Equipment");
            _permissions.CreateMenuPermission(UserType.Employee, "Tasks");
            _permissions.CreateMenuPermission(UserType.Employee, "Events");
            _permissions.CreateMenuPermission(UserType.Employee, "Calculators");
            _permissions.CreateMenuPermission(UserType.Employee, "Weather");
            _permissions.CreateMenuPermission(UserType.Employee, "Forum");
        }

        
    }
}