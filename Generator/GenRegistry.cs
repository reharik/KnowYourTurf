using CC.Core.Domain;
using CC.Core.DomainTools;
using CC.Core.Localization;
using CC.Security;
using CC.Security.Interfaces;
using CC.Security.Services;
using CC.UI.Helpers;
using KnowYourTurf.Core.Domain.Persistence;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html.Menu;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Config;
using KnowYourTurf.Web.Menus;
using KnowYourTurf.Web.Services;
using KnowYourTurf.Web.Services.EmailHandlers;
using NHibernate;
using StructureMap.Configuration.DSL;
using Log4NetLogger = KnowYourTurf.Core.Log4NetLogger;

namespace KnowYourTurf.Web
{
    public class GenRegistry : Registry
    {
        public GenRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.ConnectImplementationsToTypesClosing(typeof(IEntityListGrid<>));
                x.AssemblyContainingType(typeof(CoreLocalizationKeys));
                x.AssemblyContainingType(typeof(WebLocalizationKeys));
                x.AssemblyContainingType<Entity>();
                x.AssemblyContainingType<IUser>();
                x.AssemblyContainingType<HtmlConventionRegistry>(); 
                x.AddAllTypesOf<ICalculatorHandler>().NameBy(t => t.Name);
                x.AddAllTypesOf<RulesEngineBase>().NameBy(t => t.Name);
                x.AddAllTypesOf<IEmailTemplateHandler>().NameBy(t => t.Name);
                x.WithDefaultConventions();
            });

            For<INHSetupConfig>().Use<KYTNHSetupConfig>();
            For<IKYTMenuBuilder>().Use<KYTGenMenuBuilder>();

            For<ISessionFactoryConfiguration>().Singleton()
                .Use<SqlServerSessionSourceConfiguration>()
                .Ctor<SqlServerSessionSourceConfiguration>("connectionStr")
                .EqualToAppSetting("KnowYourTurf.sql_server_connection_string");
            For<ISessionFactory>().Singleton().Use(ctx => ctx.GetInstance<ISessionFactoryConfiguration>().CreateSessionFactory());

            For<ISession>().HybridHttpOrThreadLocalScoped().Add(
                context => context.GetInstance<ISessionFactory>().OpenSession(new SaveUpdateInterceptor()));

            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<UnitOfWork>();

            For<IRepository>().Use<Repository>();

            For<IGetCompanyIdService>().Use<DataLoaderGetCompanyIdService>();

            For<ILocalizationDataProvider>().Use<LocalizationDataProvider>();
            For<IAuthenticationContext>().Use<WebAuthenticationContext>();

            For<IMenuConfig>().Use<MainMenu>();

            For<IAuthorizationService>().HybridHttpOrThreadLocalScoped().Use<AuthorizationService>();
            For<IAuthorizationRepository>().HybridHttpOrThreadLocalScoped().Use<CustomAuthorizationRepository>();
            For<IPermissionsBuilderService>().HybridHttpOrThreadLocalScoped().Use<PermissionsBuilderService>();
            For<IPermissionsService>().HybridHttpOrThreadLocalScoped().Use<PermissionsService>();
            For<ISecuritySetupService>().Use<DefaultSecuritySetupService>();
            For<ILogger>().Use(() => new Log4NetLogger(typeof(string)));

            For<IGetCompanyIdService>().Use<DataLoaderGetCompanyIdService>();
        }
    }
}
