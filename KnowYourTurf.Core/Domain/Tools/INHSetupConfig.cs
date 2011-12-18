using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using KnowYourTurf.Core.Config;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class KYTNHSetupConfig : INHSetupConfig
    {
        public IPersistenceConfigurer DBConfiguration(string connectionString)
        {
            return MsSqlConfiguration
                .MsSql2008
                .ConnectionString(x => x.Is(connectionString))
                .UseOuterJoin();
//                .ShowSql();
        }

        public Action<MappingConfiguration> MappingConfiguration()
        {
            //m => m.AutoMappings.Add(AutoMap.AssemblyOf<User>()
            //                                    .Where(ns => ns.Namespace == "KnowYourTurf.Core.Domain")
            //                                        .IgnoreBase(typeof(DomainEntity))))
            return (m => m.FluentMappings.AddFromAssemblyOf<User>());
        }

        public void GenerateSchema(Configuration config)
        {
            new SchemaExport(config).Create(true, true);
        }
    }

}