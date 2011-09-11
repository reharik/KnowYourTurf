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
                     });
           
            For<IConventionFinder>().Use<DefaultConventionFinder>();
            For<ILocalizedStringLoader>().Use<LocalizedStringLoader>();
            For<IGetCompanyIdService>().Use<DataLoaderGetCompanyIdService>();

        }
    }
}