using Alpinely.TownCrier;
using AuthorizeNet;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Domain.Tools;
using KnowYourTurf.Core.Html.FubuUI.HtmlConventionRegistries;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Menus;
using KnowYourTurf.Web.Services;
using FubuMVC.UI;
using FubuMVC.UI.Configuration;
using FubuMVC.UI.Tags;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Config;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using Rhino.Security.Interfaces;
using Rhino.Security.Services;
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
                         x.AssemblyContainingType(typeof(Gateway));
                         x.WithDefaultConventions();
                     });
            For<IGateway>().Use<Gateway>().Ctor<string>("apiLogin").EqualToAppSetting("Authorize.Net_apiLogin")
                .Ctor<string>("transactionKey").EqualToAppSetting("Authorize.Net_TransactionKey")
                .Ctor<bool>("testMode").EqualToAppSetting("Authorize.Net_testMode");

            For<HtmlConventionRegistry>().Add<KnowYourTurfHtmlConventions>();
            For<IServiceLocator>().Singleton().Use(new StructureMapServiceLocator());
            For<IElementNamingConvention>().Use<KnowYourTurfElementNamingConvention>();
            For(typeof (ITagGenerator<>)).Use(typeof (TagGenerator<>));
            For<TagProfileLibrary>().Singleton();
            For<ICastleValidationRunner>().Use<DummyCastleValidationRunnerSuccess>();
            For<ISaveEntityServiceWithoutPrincipal>().Use<NullSaveEntityServiceWithoutPrincipal>();
            For<INHSetupConfig>().Use<NullNHSetupConfig>();

            For<ISessionFactoryConfiguration>().Singleton().Use<NullSqlServerSessionSourceConfiguration>();
            For<ISessionFactory>().Use<NullSessionFactory>();

            For<ISession>().Use<NullSession>();
            For<ISession>().Use<NullSession>().Named("NoFiltersOrInterceptor");


            For<IUnitOfWork>().Use<NullNHibernateUnitOfWork>();
            For<IUnitOfWork>().Add<NullNHibernateUnitOfWork>().Named("NoFiltersOrInterceptor");
            For<IUnitOfWork>().Add<NullNHibernateUnitOfWork>().Named("NoFilters");
            //For<IGetCompanyIdService>().Use<DataLoaderGetCompanyIdService>();

            For<IRepository>().Use<Repository>();
            For<IRepository>().Add(x => new Repository()).Named("NoFiltersOrInterceptor");
            For<IRepository>().Add(x => new Repository(true)).Named("NoFilters");

            For<ILocalizationDataProvider>().Use<LocalizationDataProvider>();
            For<IAuthenticationContext>().Use<WebAuthenticationContext>();
            For<IMenuConfig>().Use<MainMenu>();
            For<IMergedEmailFactory>().LifecycleIs(new UniquePerRequestLifecycle()).Use<MergedEmailFactory>();

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