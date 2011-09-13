using Alpinely.TownCrier;
using FubuMVC.UI;
using FubuMVC.UI.Configuration;
using FubuMVC.UI.Tags;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Domain.Persistence;
using KnowYourTurf.Core.Html.FubuUI.HtmlConventionRegistries;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Config;
using KnowYourTurf.Web.Controllers;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Services;
using KnowYourTurf.Web.Services.EmailHandlers;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using Rhino.Security.Interfaces;
using Rhino.Security.Services;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;
using PurchaseOrderLineItemGrid = KnowYourTurf.Web.Grids.PurchaseOrderLineItemGrid;
using ReceivePurchaseOrderLineItemGrid = KnowYourTurf.Web.Grids.ReceivePurchaseOrderLineItemGrid;

namespace KnowYourTurf.Web
{
    public class KYTWebRegistry : Registry
    {
        public KYTWebRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.AssemblyContainingType(typeof (CoreLocalizationKeys));
                         x.AddAllTypesOf<ICalculatorHandler>().NameBy(t => t.Name);
                         x.AddAllTypesOf<RulesEngineBase>().NameBy(t => t.Name);
                         x.AddAllTypesOf<IEmailTemplateHandler>().NameBy(t => t.Name);
                         x.AddAllTypesOf(typeof (IListTypeListGrid<>));
                         x.WithDefaultConventions();
                     });
            For<HtmlConventionRegistry>().Add<KnowYourTurfHtmlConventions>();
            For<IServiceLocator>().Singleton().Use(new StructureMapServiceLocator());
            For<IElementNamingConvention>().Use<KnowYourTurfElementNamingConvention>();
            For(typeof (ITagGenerator<>)).Use(typeof (TagGenerator<>));
            For<TagProfileLibrary>().Singleton();
            For<INHSetupConfig>().Use<KYTNHSetupConfig>();

            For<ISessionFactoryConfiguration>().Singleton()
                .Use<SqlServerSessionSourceConfiguration>()
                .Ctor<SqlServerSessionSourceConfiguration>("connectionStr")
                .EqualToAppSetting("KnowYourTurf.sql_server_connection_string");
            For<ISessionFactory>().Singleton().Use(
                ctx => ctx.GetInstance<ISessionFactoryConfiguration>().CreateSessionFactory());

            For<ISession>().HybridHttpOrThreadLocalScoped().Use(
                context =>
                context.GetInstance<ISessionFactory>().OpenSession(new SaveUpdateInterceptorWithCompanyFilter()));
            For<ISession>().HybridHttpOrThreadLocalScoped().Add(
                context => context.GetInstance<ISessionFactory>().OpenSession(new SaveUpdateInterceptor())).Named(
                    "NoCompanyFilter");
           //For<IUnitOfWork>().Use(ctx => ctx.GetInstance<INHibernateUnitOfWork>());

            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<UnitOfWork>();
            For<ITemplateParser>().Use<TemplateParser>();
            For<IMergedEmailFactory>().LifecycleIs(new UniquePerRequestLifecycle()).Use<MergedEmailFactory>();

            For<ILocalizationDataProvider>().Use<LocalizationDataProvider>();
            For<IAuthenticationContext>().Use<WebAuthenticationContext>();
            For<IMenuConfig>().Use<MenuConfig>();
            For<IMenuConfig>().Add<SetupMenuConfig>().Named("SetupMenu");

            For<IAuthorizationService>().HybridHttpOrThreadLocalScoped().Use<AuthorizationService>();
            For<IAuthorizationRepository>().HybridHttpOrThreadLocalScoped().Use<CustomAuthorizationRepository>();
            For<IPermissionsBuilderService>().HybridHttpOrThreadLocalScoped().Use<PermissionsBuilderService>();
            For<IPermissionsService>().HybridHttpOrThreadLocalScoped().Use<PermissionsService>();
            For(typeof (IGridBuilder<>)).LifecycleIs(new UniquePerRequestLifecycle()).Use(typeof (GridBuilder<>));
            For<ISecuritySetupService>().Use<DefaultSecuritySetupService>();
        }
    }
}
