using CC.Core.DomainTools;
using KnowYourTurf.Core.Domain;
using NHibernate;
using StructureMap;


namespace Generator.Commands
{
    public class DataLoadCommand : IGeneratorCommand
    {
        private readonly IRepository _repository;

        public DataLoadCommand(IRepository repository)
        {
            _repository = repository;
        }

        public string Description { get { return "Rebuilds the db and data"; } }

        public void Execute(string[] args)
        {
            new DataLoader().Load(_repository);
        }
    }
}