using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using KnowYourTurf.Core.Config;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class NullNHSetupConfig : INHSetupConfig
    {
        public IPersistenceConfigurer DBConfiguration(string connectionString)
        {
            return null;
        }

        public Action<MappingConfiguration> MappingConfiguration()
        {
            return null;
        }

        public void GenerateSchema(Configuration config)
        {
            return;
        }
    }

}