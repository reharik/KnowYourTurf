using Alpinely.TownCrier;
using AuthorizeNet;
using KnowYourTurf.Core.Domain.Persistence;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.FubuUI.HtmlConventionRegistries;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Web.Config;
using KnowYourTurf.Web.Menus;
using KnowYourTurf.Web.Services;
using FubuMVC.UI;
using FubuMVC.UI.Configuration;
using FubuMVC.UI.Tags;
using MethodFitness.Core;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using Rhino.Security.Interfaces;
using Rhino.Security.Services;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;
using Log4NetLogger = MethodFitness.Core.Log4NetLogger;
using StructureMapServiceLocator = KnowYourTurf.Core.Services.StructureMapServiceLocator;

namespace KnowYourTurf.Web
{
    public class KYTWebRegistry : Registry
    {
        public KYTWebRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.ConnectImplementationsToTypesClosing(typeof(IEntityListGrid<>));
                x.AssemblyContainingType(typeof(CoreLocalizationKeys));
                x.AssemblyContainingType(typeof(MergedEmailFactory));
                x.AssemblyContainingType(typeof(Gateway));
                x.WithDefaultConventions();
            });
            For<IGateway>().Use<Gateway>().Ctor<string>("apiLogin").EqualToAppSetting("Authorize.Net_apiLogin")
                .Ctor<string>("transactionKey").EqualToAppSetting("Authorize.Net_TransactionKey")
                .Ctor<bool>("testMode").EqualToAppSetting("Authorize.Net_testMode");

            For<HtmlConventionRegistry>().Add<KnowYourTurfHtmlConventions>();
            For<IServiceLocator>().Singleton().Use(new StructureMapServiceLocator());
            For<IElementNamingConvention>().Use<KnowYourTurfElementNamingConvention>();
            For(typeof(ITagGenerator<>)).Use(typeof(TagGenerator<>));
            For<TagProfileLibrary>().Singleton();
            For<INHSetupConfig>().Use<KYTNHSetupConfig>();

            For<ISessionFactoryConfiguration>().Singleton()
                .Use<SqlServerSessionSourceConfiguration>()
                .Ctor<SqlServerSessionSourceConfiguration>("connectionStr")
                .EqualToAppSetting("KnowYourTurf.sql_server_connection_string");
            For<ISessionFactory>().Singleton().Use(ctx => ctx.GetInstance<ISessionFactoryConfiguration>().CreateSessionFactory());

            For<ISession>().HybridHttpOrThreadLocalScoped().Use(context => context.GetInstance<ISessionFactory>().OpenSession(new SaveUpdateInterceptor()));
            For<ISession>().HybridHttpOrThreadLocalScoped().Add(context => context.GetInstance<ISessionFactory>().OpenSession(new SaveUpdateInterceptor())).Named("NoCompanyFilter");
            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<UnitOfWork>();
            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Add(context => new UnitOfWork()).Named("NoFilters");

            For<IRepository>().Use<Repository>();
            For<IRepository>().Add(x => new Repository()).Named("NoFilters");


            For<IMergedEmailFactory>().Use<MergedEmailFactory>();
            For<ITemplateParser>().Use<TemplateParser>();

            For<ILocalizationDataProvider>().Use<LocalizationDataProvider>();
            For<IAuthenticationContext>().Use<WebAuthenticationContext>();

            For<IMenuConfig>().Use<MainMenu>();
            For<IMergedEmailFactory>().LifecycleIs(new UniquePerRequestLifecycle()).Use<MergedEmailFactory>();

            For<IAuthorizationService>().HybridHttpOrThreadLocalScoped().Use<AuthorizationService>();
            For<IAuthorizationRepository>().HybridHttpOrThreadLocalScoped().Use<CustomAuthorizationRepository>();
            For<IPermissionsBuilderService>().HybridHttpOrThreadLocalScoped().Use<PermissionsBuilderService>();
            For<IPermissionsService>().HybridHttpOrThreadLocalScoped().Use<PermissionsService>();
            For<ISecuritySetupService>().Use<DefaultSecuritySetupService>();

            For(typeof(IGridBuilder<>)).Use(typeof(GridBuilder<>));
            
            For<ILogger>().Use(() => new Log4NetLogger(typeof(string)));

        }
    }
}
