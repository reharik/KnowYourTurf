using KnowYourTurf.Web.Security;

namespace Generator.Commands
{
    public class SecurityCommand: IGeneratorCommand
    {
        private readonly IPermissions _permissions;
        private readonly IOperations _operations;
        private readonly IUserGroups _userGroups;

        public SecurityCommand(IPermissions permissions,
            IOperations operations,
            IUserGroups userGroups)
        {
            _permissions = permissions;
            _operations = operations;
            _userGroups = userGroups;
        }

        public string Description { get { return "Sets Permissions to initial state"; } }

        public void Execute(string[] args)
        {
            _userGroups.CreateUserGroups();
            _userGroups.AssociateAllUsersWithTheirTypeGroup();
            _operations.CreateControllerOptions();
            _operations.CreateMenuItemOptions();
            _operations.CreateMiscItems();
            _permissions.GrantAdminPermissions();
            _permissions.GrantFacilitiesPermissions();
            _permissions.GrantEmployeePermissions();
        }
    }
}