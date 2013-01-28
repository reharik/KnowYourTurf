using KnowYourTurf.Web.Security;

namespace DBFluentMigration.Iteration_1
{
    using KnowYourTurf.Core.Enums;

    public class UpdatePermissions
    {
        private readonly IPermissions _permissions;

        public UpdatePermissions(IPermissions permissions)
        {
            _permissions = permissions;
        }

        public void Update()
        {
            GrantAdminPermissions();
        }

        private void GrantAdminPermissions()
        {
            _permissions.CreateControllerPermission("TasksByFieldController", UserType.Administrator);
        }

    }
}