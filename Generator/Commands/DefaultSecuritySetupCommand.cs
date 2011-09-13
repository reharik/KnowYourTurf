using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Services;
using StructureMap;

namespace Generator.Commands
{
    public class DefaultSecuritySetupCommand : IGeneratorCommand
    {
        private readonly IRepository _repository;
        private readonly ISecuritySetupService _securitySetupService;

        public DefaultSecuritySetupCommand(IRepository repository,ISecuritySetupService securitySetupService)
        {
            _repository = repository;
            _securitySetupService = securitySetupService;
        }

        public string Description { get { return "Loads security options defaults"; } }

        public void Execute(string[] args)
        {
            _securitySetupService.ExecuteAll();
            _repository.UnitOfWork.Commit();;
        }
    }
}