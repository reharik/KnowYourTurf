namespace DBFluentMigration.Iteration_1
{
    using KnowYourTurf.Core.Enums;
    using KnowYourTurf.Web.Security;

    public class UpdatePermissions_200:IUpdatePermissions
    {
        private readonly IPermissions _permissions;

        public UpdatePermissions_200(IPermissions permissions)
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

        }
    }
}