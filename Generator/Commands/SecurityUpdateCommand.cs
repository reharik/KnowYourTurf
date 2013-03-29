using System.Collections.Generic;
using CC.Core.DomainTools;
using DBFluentMigration.Iteration_1;
using DBFluentMigration.Iteration_2;
using KnowYourTurf.Web.Security;
using CC.Core;
using StructureMap;

namespace Generator.Commands
{
    public class SecurityUpdateCommand: IGeneratorCommand
    {
        private readonly IEnumerable<IUpdatePermissions> _permissions;
        private readonly IEnumerable<IUpdateOperations> _operations;
        private readonly IRepository _repository;

        public SecurityUpdateCommand(IRepository repository)
        {
            _permissions = ObjectFactory.Container.GetAllInstances<IUpdatePermissions>();
            _operations = ObjectFactory.Container.GetAllInstances<IUpdateOperations>();
            _repository = repository;
        }

        public string Description { get { return "Sets Permissions to initial state"; } }

        public void Execute(string[] args)
        {
            _operations.ForEachItem(x => x.Update());
            _permissions.ForEachItem(x => x.Update());
            _repository.Commit();
            
        }
    }
}