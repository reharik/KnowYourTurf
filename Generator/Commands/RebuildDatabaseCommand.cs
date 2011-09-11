using KnowYourTurf.Core.Domain;
using NHibernate;
using NHibernate.Cfg;
using StructureMap;
using StructureMap.Pipeline;
using ISessionFactoryConfiguration = KnowYourTurf.Core.Domain.ISessionFactoryConfiguration;

//using ISessionFactoryConfiguration = KnowYourTurf.Core.Domain.ISessionFactoryConfiguration;

namespace Generator.Commands
{
    public class RebuildDatabaseCommand : IGeneratorCommand
    {
        private readonly ILocalizedStringLoader _loader;
        private readonly IRepository _repository;

        public RebuildDatabaseCommand(IRepository repository, ILocalizedStringLoader loader)
        {
            _loader = loader;
            _repository = repository;
        }

        public string Description { get { return "Rebuilds the db and data"; } }

        public void Execute(string[] args)
        {
            
            
            ObjectFactory.Configure(x=>x.For<ISessionFactory>().Singleton().Use(ctx => ctx.GetInstance<ISessionFactoryConfiguration>().CreateSessionFactoryAndGenerateSchema()));
            var sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
           // SqlServerHelper.KillAllFKs(sessionFactory);
    
                
            string extraDataLoadKey = null;

            if (args.Length > 1)
            {
                _loader.SetFileBasePath(args[1]);
            }

            if (args.Length > 2)
            {
                extraDataLoadKey = args[2];
            }

            new DataLoader().Load(extraDataLoadKey);

            //_loader.ClearStrings();
            //_loader.LoadStrings(_repository);
        }
    }
}