using KnowYourTurf.Core.Domain;
using NHibernate;
using StructureMap;


namespace Generator.Commands
{
    public class DropReaddDatabaseCommand : IGeneratorCommand
    {
        private readonly IRepository _repository;

        public DropReaddDatabaseCommand(IRepository repository)
        {
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