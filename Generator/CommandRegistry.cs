using DBFluentMigration.Iteration_1;
using DBFluentMigration.Iteration_2;
using FluentNHibernate.Conventions;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using StructureMap.Configuration.DSL;

namespace Generator
{
    public class CommandRegistry : Registry
    {
        public CommandRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.AddAllTypesOf<IGeneratorCommand>().NameBy(t => t.toCannonicalCommandName());
                         x.AssemblyContainingType(typeof(IUpdateOperations));
                         x.AddAllTypesOf<IUpdateOperations>();
                         x.AddAllTypesOf<IUpdatePermissions>();
                     });
           
            For<IConventionFinder>().Use<DefaultConventionFinder>();
         //   For<ILocalizedStringLoader>().Use<LocalizedStringLoader>();

        }
    }
}