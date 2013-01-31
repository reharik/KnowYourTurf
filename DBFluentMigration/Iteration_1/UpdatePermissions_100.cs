using KnowYourTurf.Web.Security;

namespace DBFluentMigration.Iteration_1
{
    using KnowYourTurf.Core.Enums;

    public class UpdatePermissions_100 : IUpdatePermissions
    {
        private readonly IPermissions _permissions;

        public UpdatePermissions_100(IPermissions permissions)
        {
            _permissions = permissions;
        }

        public void Update()
        {
            GrantAdminPermissions();
        }

        private void GrantAdminPermissions()
        {
            _permissions.CreateControllerPermission(UserType.Administrator,"TasksByFieldController");
        }

    }
}