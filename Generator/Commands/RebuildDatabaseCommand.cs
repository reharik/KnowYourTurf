using CC.Core.DomainTools;
using NHibernate;
using StructureMap;


namespace Generator.Commands
{
    using KnowYourTurf.Core.Domain;

    public class RebuildDatabaseCommand : IGeneratorCommand
    {
        private  IRepository _repository;

        public RebuildDatabaseCommand()
        {
        }

        public string Description { get { return "Rebuilds the db and data"; } }

        public void Execute(string[] args)
        {
//            ObjectFactory.Configure(x => x.For<ISessionFactory>().Singleton().Use(ctx => ctx.GetInstance<ISessionFactoryConfiguration>().CreateSessionFactory()));
          //  var sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
//            SqlServerHelper.DeleteReaddDb(sessionFactory);

            ObjectFactory.Configure(x => x.For<ISessionFactory>().Singleton().Use(ctx => ctx.GetInstance<ISessionFactoryConfiguration>().CreateSessionFactoryAndGenerateSchema()));
            var sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
            var repository = ObjectFactory.Container.GetInstance<IRepository>();
            new DataLoader().Load(repository);
//            SqlServerHelper.AddRhinoSecurity(sessionFactory);

//            ObjectFactory.ResetDefaults();
//            ObjectFactory.Initialize(x =>
//            {
//                x.AddRegistry(new GenRegistry());
//                x.AddRegistry(new CommandRegistry());
//            });
//            var securitySetup = ObjectFactory.Container.GetInstance<IGeneratorCommand>("defaultsecuritysetup");
//            securitySetup.Execute(null);














//
//            new DataLoader().Load();
//            SqlServerHelper.AddRhinoSecurity(sessionFactory);
//
//            var securitySetup = ObjectFactory.Container.GetInstance<IGeneratorCommand>("defaultsecuritysetup");
//            securitySetup.Execute(null);


            //_loader.ClearStrings();
            //_loader.LoadStrings(_repository);
        }
    }
}