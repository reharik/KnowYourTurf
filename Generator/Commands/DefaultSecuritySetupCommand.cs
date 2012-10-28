using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Services;
using NHibernate;
using StructureMap;

namespace Generator.Commands
{
    public class DefaultSecuritySetupCommand : IGeneratorCommand
    {
        private readonly ISecuritySetupService _securitySetupService;

        public DefaultSecuritySetupCommand( ISecuritySetupService securitySetupService)
        {
            _securitySetupService = securitySetupService;
        }

        public string Description { get { return "Loads security options defaults"; } }

        public void Execute(string[] args)
        {
            _securitySetupService.ExecuteAll();
        }
    }
}