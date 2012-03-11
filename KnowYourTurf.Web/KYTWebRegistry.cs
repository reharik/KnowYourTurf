using Alpinely.TownCrier;
using KnowYourTurf.Core.Domain.Persistence;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.FubuUI.HtmlConventionRegistries;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Web.Config;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Menus;
using KnowYourTurf.Web.Services;
using FubuMVC.UI;
using FubuMVC.UI.Configuration;
using FubuMVC.UI.Tags;
using KnowYourTurf.Web.Services.EmailHandlers;
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
                x.AddAllTypesOf<ICalculatorHandler>().NameBy(t => t.Name);
                x.AddAllTypesOf<RulesEngineBase>().NameBy(t => t.Name);
                x.AddAllTypesOf<IEmailTemplateHandler>().NameBy(t => t.Name);
                x.WithDefaultConventions();
            });
            
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
            For<ISession>().HybridHttpOrThreadLocalScoped().Add(context => context.GetInstance<ISessionFactory>().OpenSession()).Named("NoFiltersOrInterceptor");

            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<UnitOfWork>();
            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Add(context => new UnitOfWork()).Named("NoFilters");
            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Add(context => new UnitOfWork(true)).Named("NoFiltersOrInterceptor");

            For<IRepository>().Use<Repository>();
            For<IRepository>().Add(x => new Repository(true)).Named("NoFiltersOrInterceptor");
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
            For<ILogger>().Use(() => new Log4NetLogger(typeof(string)));

            For(typeof(IGridBuilder<>)).Use(typeof(GridBuilder<>));

            For<IEntityListGrid<Task>>().Use<TaskListGrid>();
            For<IEntityListGrid<Task>>().Add<CompletedTaskGrid>().Named("CompletedTasks");
            For<IEntityListGrid<Task>>().Add<PendingTaskGrid>().Named("PendingTasks");

            For<IEntityListGrid<User>>().Use<EmployeeListGrid>();
            For<IEntityListGrid<User>>().Add<AdminListGrid>().Named("Admins");
            For<IEntityListGrid<User>>().Add<FacilitiesListGrid>().Named("Facilities");

            For<IEntityListGrid<PurchaseOrderLineItem>>().Use<PurchaseOrderLineItemGrid>();
            For<IEntityListGrid<PurchaseOrderLineItem>>().Add<ReceivePurchaseOrderLineItemGrid>().Named("Recieve");
            For<IEntityListGrid<Material>>().Use<MaterialListGrid>();
            For<IEntityListGrid<Chemical>>().Use<ChemicalListGrid>();
            For<IEntityListGrid<Fertilizer>>().Use<FertilizerListGrid>();

            For<IEmailTemplateHandler>().Use<EmployeeDailyTaskHandler>().Named("asdfHandler");

        }
    }
}
