using CC.Security.Interfaces;

namespace KnowYourTurf.Web.Security
{
    public interface IOperations
    {
        void CreateControllerOptions();
        void CreateMenuItemOptions();
        void CreateMiscItems();
        void CreateOperationForControllerType(string controllerName);
        void CreateOperationForMenuItem(string menuItemName);
        void CreateOperation(string operation);
        void RemoveOperationForControllerType(string controllerName);
        void RemoveOperationForMenuItem(string menuItemName);
        void RemoveOperation(string operation);
    }
    /// <summary>
    /// 
    /// 
    /// DO NOT MAKE THIS STRONGLY TYPED!
    /// NEED TO PREVENT REFACTORING!
    /// 
    /// 
    /// </summary>
    public class Operations : IOperations
    {
        private readonly IAuthorizationRepository _authorizationRepository;

        public Operations(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        public void CreateControllerOptions()
        {
            CreateOperationForControllerType("AdminController");
            CreateOperationForControllerType("AdminDashboardController");
            CreateOperationForControllerType("AdminListController");
            CreateOperationForControllerType("CalculatorController");
            CreateOperationForControllerType("CalculatorListController");
            CreateOperationForControllerType("ChemicalController");
            CreateOperationForControllerType("ChemicalListController");
            CreateOperationForControllerType("CompletedPurchaseOrderDisplayController");
            CreateOperationForControllerType("CompletedPurchaseOrderListController");
            CreateOperationForControllerType("DashboardController");
            CreateOperationForControllerType("DocumentController");
            CreateOperationForControllerType("DocumentListController");
            CreateOperationForControllerType("EmailController");
            CreateOperationForControllerType("EmailJobController");
            CreateOperationForControllerType("EmailJobListController");
            CreateOperationForControllerType("EmailTemplateController");
            CreateOperationForControllerType("EmailTemplateListController");
            CreateOperationForControllerType("EmployeeController");
            CreateOperationForControllerType("EmployeeDashboardController");
            CreateOperationForControllerType("EmployeeListController");
            CreateOperationForControllerType("EquipmentController");
            CreateOperationForControllerType("EquipmentDashboardController");
            CreateOperationForControllerType("EquipmentListController");
            CreateOperationForControllerType("EquipmentTaskCalendarController");
            CreateOperationForControllerType("EquipmentTaskController");
            CreateOperationForControllerType("EquipmentTaskListController");
            CreateOperationForControllerType("EquipmentVendorController");
            CreateOperationForControllerType("EquipmentVendorListController");
            CreateOperationForControllerType("EventCalendarController");
            CreateOperationForControllerType("EventController");
            CreateOperationForControllerType("FacilitiesController");
            CreateOperationForControllerType("FacilitiesListController");
            CreateOperationForControllerType("FertilizerController");
            CreateOperationForControllerType("FertilizerListController");
            CreateOperationForControllerType("FieldController");
            CreateOperationForControllerType("FieldDashboardController");
            CreateOperationForControllerType("FieldListController");
            CreateOperationForControllerType("ForumController");
            CreateOperationForControllerType("HomeController");
            CreateOperationForControllerType("InventoryListController");
            CreateOperationForControllerType("KnowYourTurfController");
            CreateOperationForControllerType("KYTController");
            CreateOperationForControllerType("ListTypeListController");
            CreateOperationForControllerType("MaterialController");
            CreateOperationForControllerType("MaterialListController");
            CreateOperationForControllerType("OrthogonalController");
            CreateOperationForControllerType("PermissionsController");
            CreateOperationForControllerType("PhotoController");
            CreateOperationForControllerType("PhotoListController");
            CreateOperationForControllerType("PurchaseOrderCommitController");
            CreateOperationForControllerType("PurchaseOrderController");
            CreateOperationForControllerType("PurchaseOrderLineItemController");
            CreateOperationForControllerType("PurchaseOrderLineItemListController");
            CreateOperationForControllerType("PurchaseOrderListController");
            CreateOperationForControllerType("TaskCalendarController");
            CreateOperationForControllerType("TaskListController");
            CreateOperationForControllerType("TaskController");
            CreateOperationForControllerType("VendorContactController");
            CreateOperationForControllerType("VendorContactListController");
            CreateOperationForControllerType("VendorController");
            CreateOperationForControllerType("VendorListController");
            CreateOperationForControllerType("WeatherController");
            CreateOperationForControllerType("WeatherListController");
        }

        public void CreateMenuItemOptions()
        {
            CreateOperationForMenuItem("Home");
            CreateOperationForMenuItem("Fields");
            CreateOperationForMenuItem("Calculators");
            CreateOperationForMenuItem("Tasks");
            CreateOperationForMenuItem("Task List");
            CreateOperationForMenuItem("Tasks");
            CreateOperationForMenuItem("Completed");
            CreateOperationForMenuItem("Task Calendar");
            CreateOperationForMenuItem("Events");
            CreateOperationForMenuItem("Equipment");
            CreateOperationForMenuItem("Equipment Tasks");
            CreateOperationForMenuItem("Equipment Tasks Lists");
            CreateOperationForMenuItem("Equipment Tasks");
            CreateOperationForMenuItem("Completed Equipment Tasks");
            CreateOperationForMenuItem("Equipment Task Calendar");
            CreateOperationForMenuItem("Weather");
            CreateOperationForMenuItem("Forum");
            CreateOperationForMenuItem("Admin Tools");
            CreateOperationForMenuItem("Email Jobs");
            CreateOperationForMenuItem("Employees");
            CreateOperationForMenuItem("Facilities");
            CreateOperationForMenuItem("Enumerations");
            CreateOperationForMenuItem("Products");
            CreateOperationForMenuItem("Materials");
            CreateOperationForMenuItem("Fertilizers");
            CreateOperationForMenuItem("Chemicals");
            CreateOperationForMenuItem("Documents");
            CreateOperationForMenuItem("Photos");
            CreateOperationForMenuItem("Equipment Vendors");
            CreateOperationForMenuItem("Vendors");
            CreateOperationForMenuItem("Inventory");
            CreateOperationForMenuItem("Purchase Orders");
        }

        public void CreateMiscItems()
        {
            CreateOperation("/Calendar/CanSeeOthersAppointments");
            CreateOperation("/Calendar/CanEditOtherAppointments");
            CreateOperation("/Calendar/CanEnterRetroactiveAppointments");
            CreateOperation("/Calendar/CanEditPastAppointments");
            CreateOperation("/Calendar/SetAppointmentForOthers");
            CreateOperation("/UserRoles");
            CreateOperation("/AdminOrGreater");
            CreateOperation("/KYTAdmin");
        }
        public void CreateOperationForControllerType(string controllerName) 
        {
            _authorizationRepository.CreateOperation("/" + controllerName);
        }

        public void CreateOperationForMenuItem(string menuItemName)
        {
            _authorizationRepository.CreateOperation("/MenuItem/" + menuItemName);
        }

        public void CreateOperation(string operation)
        {
            _authorizationRepository.CreateOperation(operation);
        }

        public void RemoveOperationForControllerType(string controllerName) 
        {
            _authorizationRepository.RemoveOperation("/" + controllerName);
        }

        public void RemoveOperationForMenuItem(string menuItemName)
        {
            _authorizationRepository.RemoveOperation("/MenuItem/" + menuItemName);
        }

        public void RemoveOperation(string operation)
        {
            _authorizationRepository.RemoveOperation(operation);
        }

        
    }
}