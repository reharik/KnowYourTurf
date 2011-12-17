using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Services;

namespace Generator.Commands
{
    public class DefaultSecuritySetupCommand : IGeneratorCommand
    {
        private readonly IRepository _repository;
        private readonly ISecuritySetupService _securitySetupService;

        public DefaultSecuritySetupCommand(IRepository repository, ISecuritySetupService securitySetupService)
        {
            _repository = repository;
            _securitySetupService = securitySetupService;
        }

        public string Description { get { return "Loads security options defaults"; } }

        public void Execute(string[] args)
        {
            _securitySetupService.ExecuteAll();
        }
    }
}