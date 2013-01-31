using CC.Core.DomainTools;
using DBFluentMigration.Iteration_0;
using KnowYourTurf.Web.Security;

namespace Generator.Commands
{
    public class SecurityCommand: IGeneratorCommand
    {
        private readonly IPermissions _permissions;
        private readonly IOperations _operations;
        private readonly IUserGroups _userGroups;
        private readonly IRepository _repository;

        public SecurityCommand(IPermissions permissions,
            IOperations operations,
            IUserGroups userGroups,
            IRepository repository)
        {
            _permissions = permissions;
            _operations = operations;
            _userGroups = userGroups;
            _repository = repository;
        }

        public string Description { get { return "Sets Permissions to initial state"; } }

        public void Execute(string[] args)
        {
            _userGroups.CreateUserGroups();
            _userGroups.AssociateAllUsersWithTheirTypeGroup();
            new CreateInitialOperations(_operations).Update();
            new CreateInitialPermissions(_permissions).Update();
            _repository.Commit();
        }
    }
}