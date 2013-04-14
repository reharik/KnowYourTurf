using Alpinely.TownCrier;
using CC.Core.DomainTools;
using CC.Core.Html.CCUI.HtmlConventionRegistries;
using CC.Core.Html.Grid;
using CC.Core.Localization;
using CC.Security.Interfaces;
using CC.Security.Services;
using CC.UI.Helpers;
using CC.UI.Helpers.Configuration;
using CC.UI.Helpers.Tags;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Domain.Persistence;
using KnowYourTurf.Core.Domain.Tools;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Web.Menus;
using KnowYourTurf.Web.Services;

using Microsoft.Practices.ServiceLocation;
using NHibernate;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace KnowYourTurf.Web.Config
{
    public class KYTTestRegistry : Registry
    {
        public KYTTestRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.AssemblyContainingType(typeof (CoreLocalizationKeys));
                         x.AssemblyContainingType(typeof (MergedEmailFactory));
                         x.WithDefaultConventions();
                     });
            For<HtmlConventionRegistry>().Add<CCHtmlConventions2>();
            For<IElementNamingConvention>().Use<CCElementNamingConvention>();

            For<IServiceLocator>().Singleton().Use(new StructureMapServiceLocator());
            For(typeof (ITagGenerator<>)).Use(typeof (TagGenerator<>));
            For<TagProfileLibrary>().Singleton();



            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<UnitOfWork>();
            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Add<UnitOfWork>().Named("NoInterceptorNoFilters");

            For<IRepository>().Use<Repository>();
            For<IRepository>().Add<Repository>().Named("NoFilters");
            For<IRepository>().Add<Repository>().Named("NoInterceptorNoFilters");

            For<ILocalizationDataProvider>().Use<LocalizationDataProvider>();
            For<IAuthenticationContext>().Use<WebAuthenticationContext>();
            For<IMenuConfig>().Use<MainMenu>();

            For<IAuthorizationService>().HybridHttpOrThreadLocalScoped().Use<AuthorizationService>();
            For<IAuthorizationRepository>().HybridHttpOrThreadLocalScoped().Use<AuthorizationRepository>();
            For<IPermissionsBuilderService>().HybridHttpOrThreadLocalScoped().Use<PermissionsBuilderService>();
            For<IPermissionsService>().HybridHttpOrThreadLocalScoped().Use<PermissionsService>();
            For(typeof (IGridBuilder<>)).Use(typeof (GridBuilder<>));
            For<ILogger>().Use(() => new NullLogger());


            // RegisterGrids();
        }
    }
}