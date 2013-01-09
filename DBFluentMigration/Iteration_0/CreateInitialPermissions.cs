using CC.Security.Interfaces;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Security;

namespace DBFluentMigration.Iteration_0
{
    public class CreateInitialPermissions
    {
        private readonly IPermissions _permissions;

        public CreateInitialPermissions(IPermissions permissions)
        {
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
            _permissions.CreateControllerPermission("AdminController", UserType.Administrator);
            _permissions.CreateControllerPermission("AdminDashboardController", UserType.Administrator);
            _permissions.CreateControllerPermission("AdminListController", UserType.Administrator);
            _permissions.CreateControllerPermission("CalculatorController", UserType.Administrator);
            _permissions.CreateControllerPermission("CalculatorListController", UserType.Administrator);
            _permissions.CreateControllerPermission("ChemicalController", UserType.Administrator);
            _permissions.CreateControllerPermission("ChemicalListController", UserType.Administrator);
            _permissions.CreateControllerPermission("CompletedPurchaseOrderDisplayController", UserType.Administrator);
            _permissions.CreateControllerPermission("CompletedPurchaseOrderListController", UserType.Administrator);
            _permissions.CreateControllerPermission("DashboardController", UserType.Administrator);
            _permissions.CreateControllerPermission("DocumentController", UserType.Administrator);
            _permissions.CreateControllerPermission("DocumentListController", UserType.Administrator);
            _permissions.CreateControllerPermission("EmailController", UserType.Administrator);
            _permissions.CreateControllerPermission("EmailJobController", UserType.Administrator);
            _permissions.CreateControllerPermission("EmailJobListController", UserType.Administrator);
            _permissions.CreateControllerPermission("EmailTemplateController", UserType.Administrator);
            _permissions.CreateControllerPermission("EmailTemplateListController", UserType.Administrator);
            _permissions.CreateControllerPermission("EmployeeController", UserType.Administrator);
            _permissions.CreateControllerPermission("EmployeeDashboardController", UserType.Administrator);
            _permissions.CreateControllerPermission("EmployeeListController", UserType.Administrator);
            _permissions.CreateControllerPermission("EquipmentController", UserType.Administrator);
            _permissions.CreateControllerPermission("EquipmentDashboardController", UserType.Administrator);
            _permissions.CreateControllerPermission("EquipmentListController", UserType.Administrator);
            _permissions.CreateControllerPermission("EquipmentTaskCalendarController", UserType.Administrator);
            _permissions.CreateControllerPermission("EquipmentTaskController", UserType.Administrator);
            _permissions.CreateControllerPermission("EquipmentTaskListController", UserType.Administrator);
            _permissions.CreateControllerPermission("EquipmentVendorController", UserType.Administrator);
            _permissions.CreateControllerPermission("EquipmentVendorListController", UserType.Administrator);
            _permissions.CreateControllerPermission("EventCalendarController", UserType.Administrator);
            _permissions.CreateControllerPermission("EventController", UserType.Administrator);
            _permissions.CreateControllerPermission("FacilitiesController", UserType.Administrator);
            _permissions.CreateControllerPermission("FacilitiesDashboardController", UserType.Administrator);
            _permissions.CreateControllerPermission("FacilitiesListController", UserType.Administrator);
            _permissions.CreateControllerPermission("FertilizerController", UserType.Administrator);
            _permissions.CreateControllerPermission("FertilizerListController", UserType.Administrator);
            _permissions.CreateControllerPermission("FieldController", UserType.Administrator);
            _permissions.CreateControllerPermission("FieldDashboardController", UserType.Administrator);

            _permissions.CreateControllerPermission("FieldListController", UserType.Administrator);
            _permissions.CreateControllerPermission("ForumController", UserType.Administrator);
            _permissions.CreateControllerPermission("HomeController", UserType.Administrator);
            _permissions.CreateControllerPermission("InventoryListController", UserType.Administrator);
            _permissions.CreateControllerPermission("KnowYourTurfController", UserType.Administrator);
            _permissions.CreateControllerPermission("KYTController", UserType.Administrator);
            _permissions.CreateControllerPermission("ListTypeBaseController", UserType.Administrator);
            _permissions.CreateControllerPermission("ListTypeListsController", UserType.Administrator);
            _permissions.CreateControllerPermission("MaterialController", UserType.Administrator);
            _permissions.CreateControllerPermission("MaterialListController", UserType.Administrator);
            _permissions.CreateControllerPermission("OrthogonalController", UserType.Administrator);
            _permissions.CreateControllerPermission("PermissionsController", UserType.Administrator);
            _permissions.CreateControllerPermission("PhotoController", UserType.Administrator);
            _permissions.CreateControllerPermission("PhotoListController", UserType.Administrator);
            _permissions.CreateControllerPermission("PurchaseOrderCommitController", UserType.Administrator);
            _permissions.CreateControllerPermission("PurchaseOrderController", UserType.Administrator);
            _permissions.CreateControllerPermission("PurchaseOrderLineItemController", UserType.Administrator);
            _permissions.CreateControllerPermission("PurchaseOrderLineItemListController", UserType.Administrator);
            _permissions.CreateControllerPermission("PurchaseOrderListController", UserType.Administrator);
            _permissions.CreateControllerPermission("TaskCalendarController", UserType.Administrator);
            _permissions.CreateControllerPermission("TaskController", UserType.Administrator);
            _permissions.CreateControllerPermission("TaskListController", UserType.Administrator);
            _permissions.CreateControllerPermission("TestController", UserType.Administrator);
            _permissions.CreateControllerPermission("VendorContactController", UserType.Administrator);
            _permissions.CreateControllerPermission("VendorContactListController", UserType.Administrator);
            _permissions.CreateControllerPermission("VendorController", UserType.Administrator);
            _permissions.CreateControllerPermission("VendorListController", UserType.Administrator);
            _permissions.CreateControllerPermission("WeatherController", UserType.Administrator);
            _permissions.CreateControllerPermission("WeatherListController", UserType.Administrator);

            _permissions.CreateMenuPermission(UserType.Administrator, "EquipmentTasks");
            _permissions.CreateMenuPermission(UserType.Administrator, "EquipmentTasksLists");
            _permissions.CreateMenuPermission(UserType.Administrator, "EquipmentTasks");
            _permissions.CreateMenuPermission(UserType.Administrator, "CompletedEquipmentTasks");
            _permissions.CreateMenuPermission(UserType.Administrator, "EquipmentTaskCalendar");
            _permissions.CreateMenuPermission(UserType.Administrator, "Home");
            _permissions.CreateMenuPermission(UserType.Administrator, "Fields");
            _permissions.CreateMenuPermission(UserType.Administrator, "Equipment");
            _permissions.CreateMenuPermission(UserType.Administrator, "Tasks");
            _permissions.CreateMenuPermission(UserType.Administrator, "Events");
            _permissions.CreateMenuPermission(UserType.Administrator, "Calculators");
            _permissions.CreateMenuPermission(UserType.Administrator, "Weather");
            _permissions.CreateMenuPermission(UserType.Administrator, "Forum");


            _permissions.CreateMenuPermission(UserType.Administrator, "Email Jobs");
            _permissions.CreateMenuPermission(UserType.Administrator, "Employees");
            _permissions.CreateMenuPermission(UserType.Administrator, "Facilities");
            _permissions.CreateMenuPermission(UserType.Administrator, "Enumerations");
            _permissions.CreateMenuPermission(UserType.Administrator, "Products");
            _permissions.CreateMenuPermission(UserType.Administrator, "Materials");
            _permissions.CreateMenuPermission(UserType.Administrator, "Fertilizers");
            _permissions.CreateMenuPermission(UserType.Administrator, "Chemicals");
            _permissions.CreateMenuPermission(UserType.Administrator, "Documents");
            _permissions.CreateMenuPermission(UserType.Administrator, "Photos");
            _permissions.CreateMenuPermission(UserType.Administrator, "Equipment Vendors");
            _permissions.CreateMenuPermission(UserType.Administrator, "Vendors");
            _permissions.CreateMenuPermission(UserType.Administrator, "Inventory");
            _permissions.CreateMenuPermission(UserType.Administrator, "Purchase Orders");
            _permissions.CreateMenuPermission(UserType.Administrator, "Current");
            _permissions.CreateMenuPermission(UserType.Administrator, "Completed");
            _permissions.CreateMenuPermission(UserType.Administrator, "Reports");
        
        }

        public void GrantFacilitiesPermissions()
        {
            _permissions.CreateControllerPermission("EventController", UserType.Facilities);
            _permissions.CreateControllerPermission("EventCalendarController", UserType.Facilities);
            _permissions.CreateControllerPermission("KnowYourTurfController", UserType.Facilities);
            _permissions.CreateControllerPermission("OrthogonalController", UserType.Facilities);
            
            _permissions.CreateMenuPermission(UserType.Facilities, "Events");
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