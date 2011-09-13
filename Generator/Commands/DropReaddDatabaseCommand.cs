using KnowYourTurf.Core.Domain;
using NHibernate;
using StructureMap;
using ISessionFactoryConfiguration = KnowYourTurf.Core.Domain.ISessionFactoryConfiguration;


namespace Generator.Commands
{
    public class DropReaddDatabaseCommand: IGeneratorCommand
    {
        private readonly ILocalizedStringLoader _loader;
        private readonly IRepository _repository;

        public DropReaddDatabaseCommand(IRepository repository, ILocalizedStringLoader loader)
        {
            _loader = loader;
            _repository = repository;
        }

        public string Description { get { return "Rebuilds the db and data"; } }

        public void Execute(string[] args)
        {
            ObjectFactory.Configure(x => x.For<ISessionFactory>().Singleton().Use(ctx => ctx.GetInstance<ISessionFactoryConfiguration>().CreateSessionFactory()));
            var sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
            SqlServerHelper.DeleteReaddDb(sessionFactory);
        }
    }
}