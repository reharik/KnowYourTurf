using CC.Core.DomainTools;
using CC.Core.Html.Menu;
using CC.Core.Localization;
using CC.Security.Interfaces;
using CC.Security.Services;
using KnowYourTurf.Core.Domain.Persistence;
using KnowYourTurf.Core.Enums;
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
                x.AddAllTypesOf<ICalculatorHandler>().NameBy(t => t.Name);
                x.AddAllTypesOf<RulesEngineBase>().NameBy(t => t.Name);
                x.AddAllTypesOf<IEmailTemplateHandler>().NameBy(t => t.Name);
                x.WithDefaultConventions();
            });

            For<INHSetupConfig>().Use<KYTNHSetupConfig>();

            For<ISessionFactoryConfiguration>().Singleton()
                .Use<SqlServerSessionSourceConfiguration>()
                .Ctor<SqlServerSessionSourceConfiguration>("connectionStr")
                .EqualToAppSetting("KnowYourTurf.sql_server_connection_string");
            For<ISessionFactory>().Singleton().Use(ctx => ctx.GetInstance<ISessionFactoryConfiguration>().CreateSessionFactory());

            For<ISession>().HybridHttpOrThreadLocalScoped().Use(context => context.GetInstance<ISessionFactory>().OpenSession(new SaveUpdateInterceptorWithCompanyFilter()));
            For<ISession>().HybridHttpOrThreadLocalScoped().Add(context => context.GetInstance<ISessionFactory>().OpenSession(new SaveUpdateInterceptor())).Named("NoFiltersSpecialInterceptor");
            For<ISession>().HybridHttpOrThreadLocalScoped().Add(context => context.GetInstance<ISessionFactory>().OpenSession()).Named("NoFiltersOrInterceptor");

            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<UnitOfWork>();
            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Add(context => new UnitOfWork(RepoConfig.NoFilters)).Named("NoFilters");
            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Add(context => new UnitOfWork(RepoConfig.NoFiltersOrInterceptor)).Named("NoFiltersOrInterceptor");
            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Add(context => new UnitOfWork(RepoConfig.NoFiltersSpecialInterceptor)).Named("NoFiltersSpecialInterceptor");

            For<IRepository>().Use<Repository>();
            For<IRepository>().Add(x => new Repository(RepoConfig.NoFilters)).Named("NoFilters");
            For<IRepository>().Add(x => new Repository(RepoConfig.NoFiltersOrInterceptor)).Named("NoFiltersOrInterceptor");
            For<IRepository>().Add(x => new Repository(RepoConfig.NoFiltersSpecialInterceptor)).Named("NoFiltersSpecialInterceptor");

            For<IGetCompanyIdService>().Use<DataLoaderGetCompanyIdService>();

            For<ILocalizationDataProvider>().Use<LocalizationDataProvider>();
            For<IAuthenticationContext>().Use<WebAuthenticationContext>();

            For<IMenuBuilder>().Use<GenMenuBuilder>();
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
