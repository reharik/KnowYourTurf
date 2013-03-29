using KnowYourTurf.Web.Security;

namespace DBFluentMigration.Iteration_2
{
    public class UpdateOperations_200 : IUpdateOperations
    {
        private readonly IOperations _operations;

        public UpdateOperations_200(IOperations operations)
        {
            _operations = operations;
        }

        public void Update()
        {
            CreateMenuItemOptions();
            CreateControllerOptions();
        }

        private void CreateControllerOptions()
        {
            _operations.CreateOperationForControllerType("ClientController");
            _operations.CreateOperationForControllerType("ClientListController");
            _operations.CreateOperationForControllerType("LoginStatisticsListController");
            _operations.CreateOperationForControllerType("SiteConfigurationController");
            _operations.CreateOperationForControllerType("SystemSupportController");
            _operations.CreateOperationForControllerType("UserController");
            _operations.CreateOperationForControllerType("UserListController");
            _operations.CreateOperationForControllerType("EmployeeDailyTasksController");
            _operations.CreateOperationForControllerType("TaskReportController");
            _operations.CreateOperationForControllerType("EquipmentTaskReportController");
        }

        public void CreateMenuItemOptions()
        {
            _operations.CreateOperationForMenuItem("Site1");
            _operations.CreateOperationForMenuItem("Site2");
            _operations.CreateOperationForMenuItem("Site3");
            _operations.CreateOperationForMenuItem("Site4");
            _operations.CreateOperationForMenuItem("Site5");
            _operations.CreateOperationForMenuItem("Site6");
            _operations.CreateOperationForMenuItem("Site7");
            _operations.CreateOperationForMenuItem("Site8");
            _operations.CreateOperationForMenuItem("Site9");
            _operations.CreateOperationForMenuItem("Site10");

            _operations.CreateOperationForMenuItem("Users");
            _operations.CreateOperationForMenuItem("Clients");
            _operations.CreateOperationForMenuItem("LoginInformation");
            _operations.CreateOperationForMenuItem("PermissionUserGroups");
            _operations.CreateOperationForMenuItem("SystemOffLine");
            _operations.CreateOperationForMenuItem("EmployeeDailyTasks");
            _operations.CreateOperationForMenuItem("TaskReport");
            _operations.CreateOperationForMenuItem("EquipmentTaskReport");
        }
    }
}