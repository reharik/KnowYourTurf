using KnowYourTurf.Web.Security;

namespace DBFluentMigration.Iteration_1
{
    using KnowYourTurf.Core.Enums;

    public class UpdatePermissions_200:IUpdatePermissions
    {
        private readonly IPermissions _permissions;

        public UpdatePermissions_200(IPermissions permissions)
        {
            _permissions = permissions;
        }

        public void Update()
        {
            GrantAdminPermissions();
            GrantEmployeePermissions();
        }

        private void GrantAdminPermissions()
        {
            _permissions.CreateMenuPermission(UserType.Administrator,"Site1");
            _permissions.CreateMenuPermission(UserType.Administrator,"Site2");
            _permissions.CreateMenuPermission(UserType.Administrator,"Site3");
            _permissions.CreateMenuPermission(UserType.Administrator,"Site4");
            _permissions.CreateMenuPermission(UserType.Administrator,"Site5");
            _permissions.CreateMenuPermission(UserType.Administrator,"Site6");
            _permissions.CreateMenuPermission(UserType.Administrator,"Site7");
            _permissions.CreateMenuPermission(UserType.Administrator,"Site8");
            _permissions.CreateMenuPermission(UserType.Administrator,"Site9");
            _permissions.CreateMenuPermission(UserType.Administrator,"Site10");
        }

        private void GrantEmployeePermissions()
        {
            _permissions.CreateMenuPermission(UserType.Employee, "Site1");
            _permissions.CreateMenuPermission(UserType.Employee, "Site2");
            _permissions.CreateMenuPermission(UserType.Employee, "Site3");
            _permissions.CreateMenuPermission(UserType.Employee, "Site4");
            _permissions.CreateMenuPermission(UserType.Employee, "Site5");
            _permissions.CreateMenuPermission(UserType.Employee, "Site6");
            _permissions.CreateMenuPermission(UserType.Employee, "Site7");
            _permissions.CreateMenuPermission(UserType.Employee, "Site8");
            _permissions.CreateMenuPermission(UserType.Employee, "Site9");
            _permissions.CreateMenuPermission(UserType.Employee, "Site10");
        }

    }
}