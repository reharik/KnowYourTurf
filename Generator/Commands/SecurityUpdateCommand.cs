using CC.Core.DomainTools;
using KnowYourTurf.Web.Security;

namespace Generator.Commands
{
    public class SecurityUpdateCommand: IGeneratorCommand
    {
        private readonly IPermissions _permissions;
        private readonly IOperations _operations;
        private readonly IRepository _repository;

        public SecurityUpdateCommand(IPermissions permissions,
            IOperations operations,
            IRepository repository)
        {
            _permissions = permissions;
            _operations = operations;
            _repository = repository;
        }

        public string Description { get { return "Sets Permissions to initial state"; } }

        public void Execute(string[] args)
        {
//            new UpdateOperations(_operations).Update();
//            new UpdatePermissions(_permissions).update();
//            _repository.Commit();
            
        }
    }
}