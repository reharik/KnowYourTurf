using System;
using System.IO;
using System.Reflection;
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

            new DataLoader().Load();

            SqlServerHelper.AddRhinoSecurity(sessionFactory);

            var securitySetup = ObjectFactory.Container.GetInstance<IGeneratorCommand>("defaultsecuritysetup");
            securitySetup.Execute(null);


            //_loader.ClearStrings();
            //_loader.LoadStrings(_repository);
        }
    }
}