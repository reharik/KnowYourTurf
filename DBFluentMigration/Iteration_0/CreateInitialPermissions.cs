using KnowYourTurf.Core.Enums;
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
            _permissions.CreateControllerPermission(UserType.Administrator, "AdminOrGreater");
            _permissions.CreateControllerPermission(UserType.Administrator, "AdminController");
            _permissions.CreateControllerPermission(UserType.Administrator,"AdminDashboardController");
            _permissions.CreateControllerPermission(UserType.Administrator,"AdminListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"CalculatorController");
            _permissions.CreateControllerPermission(UserType.Administrator,"CalculatorListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"ChemicalController");
            _permissions.CreateControllerPermission(UserType.Administrator,"ChemicalListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"CompletedPurchaseOrderDisplayController");
            _permissions.CreateControllerPermission(UserType.Administrator,"CompletedPurchaseOrderListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"DashboardController");
            _permissions.CreateControllerPermission(UserType.Administrator,"DocumentController");
            _permissions.CreateControllerPermission(UserType.Administrator,"DocumentListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EmailController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EmailJobController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EmailJobListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EmailTemplateController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EmailTemplateListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EmployeeController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EmployeeDashboardController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EmployeeListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EquipmentController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EquipmentDashboardController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EquipmentListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EquipmentTaskCalendarController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EquipmentTaskController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EquipmentTaskListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EquipmentVendorController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EquipmentVendorListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EventCalendarController");
            _permissions.CreateControllerPermission(UserType.Administrator,"EventController");
            _permissions.CreateControllerPermission(UserType.Administrator,"FacilitiesController");
            _permissions.CreateControllerPermission(UserType.Administrator,"FacilitiesDashboardController");
            _permissions.CreateControllerPermission(UserType.Administrator,"FacilitiesListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"FertilizerController");
            _permissions.CreateControllerPermission(UserType.Administrator,"FertilizerListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"FieldController");
            _permissions.CreateControllerPermission(UserType.Administrator,"FieldDashboardController");
                                                    
            _permissions.CreateControllerPermission(UserType.Administrator,"FieldListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"ForumController");
            _permissions.CreateControllerPermission(UserType.Administrator,"HomeController");
            _permissions.CreateControllerPermission(UserType.Administrator,"InventoryListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"KnowYourTurfController");
            _permissions.CreateControllerPermission(UserType.Administrator,"KYTController");
            _permissions.CreateControllerPermission(UserType.Administrator, "ListTypeListController");
            _permissions.CreateControllerPermission(UserType.Administrator, "TaskTypeController");
            _permissions.CreateControllerPermission(UserType.Administrator, "PartController");
            _permissions.CreateControllerPermission(UserType.Administrator, "DocumentCategoryController");
            _permissions.CreateControllerPermission(UserType.Administrator, "EventTypeController");
            _permissions.CreateControllerPermission(UserType.Administrator, "EquipmentTypeController");
            _permissions.CreateControllerPermission(UserType.Administrator, "PhotoCategoryController");
            _permissions.CreateControllerPermission(UserType.Administrator, "EquipmentTaskTypeController");
            _permissions.CreateControllerPermission(UserType.Administrator,"MaterialController");
            _permissions.CreateControllerPermission(UserType.Administrator,"MaterialListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"OrthogonalController");
            _permissions.CreateControllerPermission(UserType.Administrator,"PermissionsController");
            _permissions.CreateControllerPermission(UserType.Administrator,"PhotoController");
            _permissions.CreateControllerPermission(UserType.Administrator,"PhotoListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"PurchaseOrderCommitController");
            _permissions.CreateControllerPermission(UserType.Administrator,"PurchaseOrderController");
            _permissions.CreateControllerPermission(UserType.Administrator,"PurchaseOrderLineItemController");
            _permissions.CreateControllerPermission(UserType.Administrator,"PurchaseOrderLineItemListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"PurchaseOrderListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"TaskCalendarController");
            _permissions.CreateControllerPermission(UserType.Administrator,"TaskController");
            _permissions.CreateControllerPermission(UserType.Administrator,"TaskListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"VendorContactController");
            _permissions.CreateControllerPermission(UserType.Administrator,"VendorContactListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"VendorController");
            _permissions.CreateControllerPermission(UserType.Administrator,"VendorListController");
            _permissions.CreateControllerPermission(UserType.Administrator,"WeatherController");
            _permissions.CreateControllerPermission(UserType.Administrator,"WeatherListController");
            _permissions.CreateControllerPermission(UserType.Administrator, "TasksByFieldController");

            

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


            _permissions.CreateMenuPermission(UserType.Administrator, "AdminTools");
            _permissions.CreateMenuPermission(UserType.Administrator, "EmailJobs");
            _permissions.CreateMenuPermission(UserType.Administrator, "Employees");
            _permissions.CreateMenuPermission(UserType.Administrator, "Facilities");
            _permissions.CreateMenuPermission(UserType.Administrator, "Enumerations");
            _permissions.CreateMenuPermission(UserType.Administrator, "Products");
            _permissions.CreateMenuPermission(UserType.Administrator, "Materials");
            _permissions.CreateMenuPermission(UserType.Administrator, "Fertilizers");
            _permissions.CreateMenuPermission(UserType.Administrator, "Chemicals");
            _permissions.CreateMenuPermission(UserType.Administrator, "Documents");
            _permissions.CreateMenuPermission(UserType.Administrator, "Photos");
            _permissions.CreateMenuPermission(UserType.Administrator, "EquipmentVendors");
            _permissions.CreateMenuPermission(UserType.Administrator, "Vendors");
            _permissions.CreateMenuPermission(UserType.Administrator, "Inventory");
            _permissions.CreateMenuPermission(UserType.Administrator, "PurchaseOrders");
            _permissions.CreateMenuPermission(UserType.Administrator, "Current");
            _permissions.CreateMenuPermission(UserType.Administrator, "Completed");
            _permissions.CreateMenuPermission(UserType.Administrator, "Reports");

           

        }

        public void GrantFacilitiesPermissions()
        {
            _permissions.CreateControllerPermission(UserType.Facilities, "EventController");
            _permissions.CreateControllerPermission(UserType.Facilities,"EventCalendarController");
            _permissions.CreateControllerPermission(UserType.Facilities,"KnowYourTurfController");
            _permissions.CreateControllerPermission(UserType.Facilities,"OrthogonalController");
            
            _permissions.CreateMenuPermission(UserType.Facilities, "Events");
        }

        public void GrantEmployeePermissions()
        {
            _permissions.CreateControllerPermission(UserType.Employee, "KnowYourTurfController");
            _permissions.CreateControllerPermission(UserType.Employee, "OrthogonalController");
            _permissions.CreateControllerPermission(UserType.Employee, "CalculatorController");
            _permissions.CreateControllerPermission(UserType.Employee, "CalculatorListController");
            _permissions.CreateControllerPermission(UserType.Employee, "EmployeeDashboardController");
            _permissions.CreateControllerPermission(UserType.Employee, "EmployeeController");
            _permissions.CreateControllerPermission(UserType.Employee, "EquipmentController");
            _permissions.CreateControllerPermission(UserType.Employee, "EquipmentListController");
            _permissions.CreateControllerPermission(UserType.Employee, "EventController");
            _permissions.CreateControllerPermission(UserType.Employee, "EventCalendarController");
            _permissions.CreateControllerPermission(UserType.Employee, "FieldController");
            _permissions.CreateControllerPermission(UserType.Employee, "FieldDashboardController");
            _permissions.CreateControllerPermission(UserType.Employee, "FieldListController");
            _permissions.CreateControllerPermission(UserType.Employee, "ForumController");
            _permissions.CreateControllerPermission(UserType.Employee, "InventoryListController");
            _permissions.CreateControllerPermission(UserType.Employee, "PhotoController");
            _permissions.CreateControllerPermission(UserType.Employee, "DocumentController");
            _permissions.CreateControllerPermission(UserType.Employee, "PurchaseOrderCommitController");
            _permissions.CreateControllerPermission(UserType.Employee, "PurchaseOrderController");
            _permissions.CreateControllerPermission(UserType.Employee, "PurchaseOrderLineItemController");
            _permissions.CreateControllerPermission(UserType.Employee, "PurchaseOrderListController");
            _permissions.CreateControllerPermission(UserType.Employee, "TaskController");
            _permissions.CreateControllerPermission(UserType.Employee, "TaskCalendarController");
            _permissions.CreateControllerPermission(UserType.Employee, "TaskListController");
            _permissions.CreateControllerPermission(UserType.Employee, "VendorContactController");
            _permissions.CreateControllerPermission(UserType.Employee, "VendorContactListController");
            _permissions.CreateControllerPermission(UserType.Employee, "VendorController");
            _permissions.CreateControllerPermission(UserType.Employee, "VendorListController");
            _permissions.CreateControllerPermission(UserType.Employee, "WeatherController");
            _permissions.CreateControllerPermission(UserType.Employee, "WeatherListController");
            _permissions.CreateControllerPermission(UserType.Employee, "EquipmentDashboardController");
            _permissions.CreateControllerPermission(UserType.Employee, "EquipmentTaskCalendarController");
            _permissions.CreateControllerPermission(UserType.Employee, "EquipmentTaskController");
            _permissions.CreateControllerPermission(UserType.Employee, "EquipmentTaskListController");

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