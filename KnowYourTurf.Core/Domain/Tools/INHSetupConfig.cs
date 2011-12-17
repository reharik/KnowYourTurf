using System;
using KnowYourTurf.Core.Config;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace KnowYourTurf.Core.Domain.Tools
{
    public class KYTNHSetupConfig : INHSetupConfig
    {
        public IPersistenceConfigurer DBConfiguration(string connectionString)
        {
            return MsSqlConfiguration
                .MsSql2008
                .ConnectionString(x => x.Is(connectionString))
                .UseOuterJoin()
                .ShowSql();
        }

        public Action<MappingConfiguration> MappingConfiguration()
        {
            //m => m.AutoMappings.Add(AutoMap.AssemblyOf<User>()
            //                                    .Where(ns => ns.Namespace == "KnowYourTurf.Core.Domain")
            //                                        .IgnoreBase(typeof(DomainEntity))))
            return (m => m.FluentMappings.AddFromAssemblyOf<User>()
                            .Conventions.Add(ForeignKey.EndsWith("Id"))
                            .Conventions.Add(DefaultLazy.Always())
                            .Conventions.Add<ForeignKeyConstraintNameConvention>()
                            .Conventions.Add<CustomManyToManyTableNameConvention>()
                            .Conventions.Add(DefaultCascade.SaveUpdate())
                            .Conventions.Add<TextAreaConvention>());
        }

        public void GenerateSchema(Configuration config)
        {
            new SchemaExport(config).Create(true, true);
        }
    }

}