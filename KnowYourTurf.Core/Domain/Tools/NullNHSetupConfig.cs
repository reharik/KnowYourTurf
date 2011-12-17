using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;

namespace KnowYourTurf.Core.Domain.Tools
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