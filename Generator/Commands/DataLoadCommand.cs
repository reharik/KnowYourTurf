using CC.Core.DomainTools;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using NHibernate;
using StructureMap;


namespace Generator.Commands
{
    public class DataLoadCommand : IGeneratorCommand
    {
        private readonly IRepository _repository;
        private readonly ISecurityDataService _securityDataService;

        public DataLoadCommand(IRepository repository, ISecurityDataService securityDataService)
        {
            _repository = repository;
            _securityDataService = securityDataService;
        }

        public string Description { get { return "Rebuilds the db and data"; } }

        public void Execute(string[] args)
        {
            new DataLoader().Load(_repository, _securityDataService);
        }
    }
}