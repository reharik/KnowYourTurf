using KnowYourTurf.Web.Security;

namespace DBFluentMigration.Iteration_1.SecurityUpdates
{
    public class UpdateOperations
    {
        private readonly IOperations _operations;

        public UpdateOperations(IOperations operations)
        {
            _operations = operations;
        }

        public void Update()
        {
            _operations.CreateOperationForControllerType("PartController");
            _operations.CreateOperationForControllerType("EquipmentDashboardController");
            _operations.CreateOperationForControllerType("EquipmentTaskCalendarController");
            _operations.CreateOperationForControllerType("EquipmentTaskTypeController");
            _operations.CreateOperationForControllerType("EquipmentTypeController");
            _operations.CreateOperationForControllerType("EquipmentTaskController");
            _operations.CreateOperationForControllerType("EquipmentTaskListController");
            _operations.CreateOperationForControllerType("EquipmentVendorController");
            _operations.CreateOperationForControllerType("EquipmentVendorListController");

            _operations.CreateOperationForMenuItem("EquipmentVendors");
            _operations.CreateOperationForMenuItem("EquipmentTasks");
            _operations.CreateOperationForMenuItem("EquipmentTasksLists");
            _operations.CreateOperationForMenuItem("EquipmentTasks");
            _operations.CreateOperationForMenuItem("CompletedEquipmentTasks");
            _operations.CreateOperationForMenuItem("EquipmentTaskCalendar");

        }
    }
}