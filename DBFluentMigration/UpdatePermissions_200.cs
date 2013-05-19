using KnowYourTurf.Core.Enums;
using KnowYourTurf.Web.Security;

namespace DBFluentMigration.Iteration_1
{

    public class UpdatePermissions_2001:IUpdatePermissions
    {
        private readonly IPermissions _permissions;

        public UpdatePermissions_2001(IPermissions permissions)
        {
            this._permissions = permissions;
        }

        public void Update()
        {
            this.GrantAdminPermissions();
        }

        private void GrantAdminPermissions()
        {
            this._permissions.CreateControllerPermission(UserType.Administrator, "TDAController");
            this._permissions.CreateMenuPermission(UserType.Administrator, "TDA");
            _permissions.CreatePermission(UserType.Administrator, "/EditPastTask");
        }
    }
}