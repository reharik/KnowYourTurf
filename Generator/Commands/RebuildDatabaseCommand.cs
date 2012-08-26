using KnowYourTurf.Core.Domain;
using NHibernate;
using StructureMap;


namespace Generator.Commands
{
    public class RebuildDatabaseCommand : IGeneratorCommand
    {
        private readonly IRepository _repository;

        public RebuildDatabaseCommand(IRepository repository)
        {
            _repository = repository;
        }

        public string Description { get { return "Rebuilds the db and data"; } }

        public void Execute(string[] args)
        {

            ObjectFactory.Configure(x => x.For<ISessionFactory>().Singleton().Use(ctx => ctx.GetInstance<ISessionFactoryConfiguration>().CreateSessionFactoryAndGenerateSchema()));
            var sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
//            SqlServerHelper.DeleteReaddDb(sessionFactory);

            new DataLoader().Load();
            SqlServerHelper.AddRhinoSecurity(sessionFactory);

            var securitySetup = ObjectFactory.Container.GetInstance<IGeneratorCommand>("defaultsecuritysetup");
            securitySetup.Execute(null);


            //_loader.ClearStrings();
            //_loader.LoadStrings(_repository);
        }
    }
}