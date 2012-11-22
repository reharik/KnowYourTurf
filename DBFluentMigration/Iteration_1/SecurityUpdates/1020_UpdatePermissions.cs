using KnowYourTurf.Core.Enums;
using KnowYourTurf.Web.Security;

namespace DBFluentMigration.Iteration_1.SecurityUpdates
{
    public class UpdatePermissions
    {
        private readonly IPermissions _permissions;

        public UpdatePermissions(IPermissions permissions)
        {
            _permissions = permissions;
        }

        public void update()
        {
            updateEmployees();
            updateAdmins();
        }

        public void updateEmployees()
        {
            _permissions.CreateControllerPermission("EquipmentDashboardController", UserType.Employee);
            _permissions.CreateControllerPermission("EquipmentTaskCalendarController", UserType.Employee);
            _permissions.CreateControllerPermission("EquipmentTaskController", UserType.Employee);
            _permissions.CreateControllerPermission("EquipmentTaskListController", UserType.Employee);

            _permissions.CreateMenuPermission(UserType.Employee, "EquipmentTasks");
            _permissions.CreateMenuPermission(UserType.Employee, "EquipmentTasksLists");
            _permissions.CreateMenuPermission(UserType.Employee, "EquipmentTasks");
            _permissions.CreateMenuPermission(UserType.Employee, "CompletedEquipmentTasks");
            _permissions.CreateMenuPermission(UserType.Employee, "EquipmentTaskCalendar");
        }

        public void updateAdmins()
        {
            _permissions.CreateControllerPermission("PartController", UserType.Administrator, 10);
            _permissions.CreateControllerPermission("EquipmentDashboardController", UserType.Administrator, 10);
            _permissions.CreateControllerPermission("EquipmentTaskCalendarController", UserType.Administrator, 10);
            _permissions.CreateControllerPermission("EquipmentTaskTypeController", UserType.Administrator, 10);
            _permissions.CreateControllerPermission("EquipmentTypeController", UserType.Administrator, 10);
            _permissions.CreateControllerPermission("EquipmentTaskController", UserType.Administrator, 10);
            _permissions.CreateControllerPermission("EquipmentTaskListController", UserType.Administrator, 10);
            _permissions.CreateControllerPermission("EquipmentVendorController", UserType.Administrator, 10);
            _permissions.CreateControllerPermission("EquipmentVendorListController", UserType.Administrator, 10);


            //fix for fb206
            _permissions.CreateControllerPermission("CompletedPurchaseOrderDisplayController", UserType.Administrator, 10);


            _permissions.CreateMenuPermission(UserType.Administrator, "EquipmentVendors", 10);
            _permissions.CreateMenuPermission(UserType.Administrator, "EquipmentTasks", 10);
            _permissions.CreateMenuPermission(UserType.Administrator, "EquipmentTasksLists", 10);
            _permissions.CreateMenuPermission(UserType.Administrator, "EquipmentTasks", 10);
            _permissions.CreateMenuPermission(UserType.Administrator, "CompletedEquipmentTasks", 10);
            _permissions.CreateMenuPermission(UserType.Administrator, "EquipmentTaskCalendar", 10);
        }
    }
}