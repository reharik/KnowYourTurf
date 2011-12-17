using System;
using KnowYourTurf.Core.Config;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using Rhino.Security;

namespace KnowYourTurf.Core.Domain
{
    public interface INHSetupConfig
    {
        IPersistenceConfigurer DBConfiguration(string connectionString);
        Action<MappingConfiguration> MappingConfiguration();
        void GenerateSchema(Configuration configuration);
    }

    public interface ISessionFactoryConfiguration
    {
        ISessionFactory CreateSessionFactory();
        ISessionFactory CreateSessionFactoryAndGenerateSchema();
    }

    public class SqlServerSessionSourceConfiguration : ISessionFactoryConfiguration
    {
        private readonly INHSetupConfig _config;
        private readonly string _connectionStr;

        public SqlServerSessionSourceConfiguration(INHSetupConfig config, string connectionStr)
        {
            _config = config;
            _connectionStr = connectionStr;
        }

        public ISessionFactory CreateSessionFactoryAndGenerateSchema()
        {
            return Fluently.Configure()
                .Database(_config.DBConfiguration(_connectionStr))
              //  .Mappings(m => m.FluentMappings.Add(typeof(CompanyConditionFilter)))
               // .Mappings(m => m.FluentMappings.Add(typeof(OrgConditionFilter)))
                .Mappings(_config.MappingConfiguration())
                .ExposeConfiguration(x=>
                {
                    _config.GenerateSchema(x);
                    x.SetProperty("adonet.batch_size", "100");
                    x.SetProperty("generate_statistics", "true");
                    Security.Configure<User>(x, SecurityTableStructure.Prefix);
                })
                .BuildSessionFactory();
        }

        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(_config.DBConfiguration(_connectionStr))
               // .Mappings(m => m.FluentMappings.Add(typeof(CompanyConditionFilter)))
              //  .Mappings(m => m.FluentMappings.Add(typeof(OrgConditionFilter)))
              //  .Mappings(m => m.FluentMappings.Add(typeof(DeletedConditionFilter)))
                .Mappings(_config.MappingConfiguration())
                .ExposeConfiguration(x =>
                {
                    x.SetProperty("adonet.batch_size", "100");
                    x.SetProperty("generate_statistics", "true");
                    Security.Configure<User>(x, SecurityTableStructure.Prefix);
                })
                .BuildSessionFactory();
        }

    }
}