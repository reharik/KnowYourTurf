using KnowYourTurf.Web.Security;

namespace DBFluentMigration.Iteration_0
{
    public class CreateInitialOperations
    {
        private readonly IOperations _operations;

        public CreateInitialOperations(IOperations operations)
        {
            _operations = operations;
        }

        public void Update()
        {
            CreateControllerOptions();
            CreateMenuItemOptions();
            CreateMiscItems();
        }
        public void CreateControllerOptions()
        {
            _operations.CreateOperationForControllerType("AdminController");
            _operations.CreateOperationForControllerType("AdminDashboardController");
            _operations.CreateOperationForControllerType("AdminListController");
            _operations.CreateOperationForControllerType("CalculatorController");
            _operations.CreateOperationForControllerType("CalculatorListController");
            _operations.CreateOperationForControllerType("ChemicalController");
            _operations.CreateOperationForControllerType("ChemicalListController");
            _operations.CreateOperationForControllerType("CompletedPurchaseOrderDisplayController");
            _operations.CreateOperationForControllerType("CompletedPurchaseOrderListController");
            _operations.CreateOperationForControllerType("DashboardController");
            _operations.CreateOperationForControllerType("DocumentController");
            _operations.CreateOperationForControllerType("DocumentListController");
            _operations.CreateOperationForControllerType("EmailController");
            _operations.CreateOperationForControllerType("EmailJobController");
            _operations.CreateOperationForControllerType("EmailJobListController");
            _operations.CreateOperationForControllerType("EmailTemplateController");
            _operations.CreateOperationForControllerType("EmailTemplateListController");
            _operations.CreateOperationForControllerType("EmployeeController");
            _operations.CreateOperationForControllerType("EmployeeDashboardController");
            _operations.CreateOperationForControllerType("EmployeeListController");
            _operations.CreateOperationForControllerType("EquipmentController");
            _operations.CreateOperationForControllerType("EquipmentListController");
            _operations.CreateOperationForControllerType("EventCalendarController");
            _operations.CreateOperationForControllerType("EventController");
            _operations.CreateOperationForControllerType("FacilitiesController");
            _operations.CreateOperationForControllerType("FacilitiesListController");
            _operations.CreateOperationForControllerType("FertilizerController");
            _operations.CreateOperationForControllerType("FertilizerListController");
            _operations.CreateOperationForControllerType("FieldController");
            _operations.CreateOperationForControllerType("FieldDashboardController");
            _operations.CreateOperationForControllerType("FieldListController");
            _operations.CreateOperationForControllerType("ForumController");
            _operations.CreateOperationForControllerType("HomeController");
            _operations.CreateOperationForControllerType("InventoryListController");
            _operations.CreateOperationForControllerType("KnowYourTurfController");
            _operations.CreateOperationForControllerType("KYTController");
            _operations.CreateOperationForControllerType("ListTypeListController");
            _operations.CreateOperationForControllerType("MaterialController");
            _operations.CreateOperationForControllerType("MaterialListController");
            _operations.CreateOperationForControllerType("OrthogonalController");
            _operations.CreateOperationForControllerType("PermissionsController");
            _operations.CreateOperationForControllerType("PhotoController");
            _operations.CreateOperationForControllerType("PhotoListController");
            _operations.CreateOperationForControllerType("PurchaseOrderCommitController");
            _operations.CreateOperationForControllerType("PurchaseOrderController");
            _operations.CreateOperationForControllerType("PurchaseOrderLineItemController");
            _operations.CreateOperationForControllerType("PurchaseOrderLineItemListController");
            _operations.CreateOperationForControllerType("PurchaseOrderListController");
            _operations.CreateOperationForControllerType("TaskCalendarController");
            _operations.CreateOperationForControllerType("TaskListController");
            _operations.CreateOperationForControllerType("TaskController");
            _operations.CreateOperationForControllerType("VendorContactController");
            _operations.CreateOperationForControllerType("VendorContactListController");
            _operations.CreateOperationForControllerType("VendorController");
            _operations.CreateOperationForControllerType("VendorListController");
            _operations.CreateOperationForControllerType("WeatherController");
            _operations.CreateOperationForControllerType("WeatherListController");
        }

        public void CreateMenuItemOptions()
        {
            _operations.CreateOperationForMenuItem("Home");
            _operations.CreateOperationForMenuItem("Fields");
            _operations.CreateOperationForMenuItem("Calculators");
            _operations.CreateOperationForMenuItem("Tasks");
            _operations.CreateOperationForMenuItem("TaskList");
            _operations.CreateOperationForMenuItem("Tasks");
            _operations.CreateOperationForMenuItem("Completed");
            _operations.CreateOperationForMenuItem("TaskCalendar");
            _operations.CreateOperationForMenuItem("Events");
            _operations.CreateOperationForMenuItem("Equipment");
            _operations.CreateOperationForMenuItem("Weather");
            _operations.CreateOperationForMenuItem("Forum");
            _operations.CreateOperationForMenuItem("AdminTools");
            _operations.CreateOperationForMenuItem("EmailJobs");
            _operations.CreateOperationForMenuItem("Employees");
            _operations.CreateOperationForMenuItem("Facilities");
            _operations.CreateOperationForMenuItem("Enumerations");
            _operations.CreateOperationForMenuItem("Products");
            _operations.CreateOperationForMenuItem("Materials");
            _operations.CreateOperationForMenuItem("Fertilizers");
            _operations.CreateOperationForMenuItem("Chemicals");
            _operations.CreateOperationForMenuItem("Documents");
            _operations.CreateOperationForMenuItem("Photos");
            _operations.CreateOperationForMenuItem("Vendors");
            _operations.CreateOperationForMenuItem("Inventory");
            _operations.CreateOperationForMenuItem("PurchaseOrders");
        }

        public void CreateMiscItems()
        {
            _operations.CreateOperation("/Calendar/CanSeeOthersAppointments");
            _operations.CreateOperation("/Calendar/CanEditOtherAppointments");
            _operations.CreateOperation("/Calendar/CanEnterRetroactiveAppointments");
            _operations.CreateOperation("/Calendar/CanEditPastAppointments");
            _operations.CreateOperation("/Calendar/SetAppointmentForOthers");
            _operations.CreateOperation("/UserRoles");
            _operations.CreateOperation("/AdminOrGreater");
            _operations.CreateOperation("/KYTAdmin");
        }
    }
}