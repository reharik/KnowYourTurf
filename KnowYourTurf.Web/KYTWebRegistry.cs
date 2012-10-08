using Alpinely.TownCrier;
using CC.Core.Domain;
using CC.Core.DomainTools;
using CC.Core.Html.CCUI.HtmlConventionRegistries;
using CC.Core.Html.Grid;
using CC.Core.Localization;
using CC.Core.Services;
using CC.Security;
using CC.Security.Interfaces;
using CC.Security.Services;
using CC.UI.Helpers;
using CC.UI.Helpers.Configuration;
using CC.UI.Helpers.Tags;
using KnowYourTurf.Core.Domain.Persistence;
using KnowYourTurf.Core.Domain.Tools;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Config;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Menus;
using KnowYourTurf.Web.Services;
using KnowYourTurf.Web.Services.EmailHandlers;
using KnowYourTurf.Web.Services.ViewOptions;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;
using Log4NetLogger = KnowYourTurf.Core.Log4NetLogger;
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
                x.AssemblyContainingType<Entity>();
                x.AssemblyContainingType<IUser>();
                x.AssemblyContainingType<HtmlConventionRegistry>();  
                x.AddAllTypesOf<ICalculatorHandler>().NameBy(t => t.Name);
                x.AddAllTypesOf<RulesEngineBase>().NameBy(t => t.Name);
                x.AddAllTypesOf<IEmailTemplateHandler>().NameBy(t => t.Name);
                x.WithDefaultConventions();
            });

            For<HtmlConventionRegistry>().Add<CCHtmlConventions2>();
            For<IServiceLocator>().Singleton().Use(new StructureMapServiceLocator());
            For<IElementNamingConvention>().Use<CCElementNamingConvention>();
            For(typeof(ITagGenerator<>)).Use(typeof(TagGenerator<>));
            For<TagProfileLibrary>().Singleton();
            For<INHSetupConfig>().Use<KYTNHSetupConfig>();

            For<ISessionFactoryConfiguration>().Singleton()
                .Use<SqlServerSessionSourceConfiguration>()
                .Ctor<SqlServerSessionSourceConfiguration>("connectionStr")
                .EqualToAppSetting("KnowYourTurf.sql_server_connection_string");
            For<ISessionFactory>().Singleton().Use(ctx => ctx.GetInstance<ISessionFactoryConfiguration>().CreateSessionFactory());

            For<ISession>().HybridHttpOrThreadLocalScoped().Use(context => context.GetInstance<ISessionFactory>().OpenSession(new SaveUpdateInterceptor()));
            For<ISession>().HybridHttpOrThreadLocalScoped().Add(context => context.GetInstance<ISessionFactory>().OpenSession()).Named("NoInterceptorNoFilters");

            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<KYTUnitOfWork>();
            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Add<UnitOfWork>().Named("NoFilters");
            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Add<NoInterceptorNoFiltersUnitOfWork>().Named("NoInterceptorNoFilters");

            For<IRepository>().Use<Repository>();
            For<IRepository>().Add<NoFilterRepository>().Named("NoFilters");
            For<IRepository>().Add<NoInterceptorNoFiltersRepository>().Named("NoInterceptorNoFilters");

            For<ISelectListItemService>().Use<KYTSelectListItemService>();
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
            For<ICCSessionContext>().Use<SessionContext>();

            For(typeof(IGridBuilder<>)).Use(typeof(GridBuilder<>));

            For<IEntityListGrid<Task>>().Use<TaskListGrid>();
            For<IEntityListGrid<Task>>().Add<CompletedTaskGrid>().Named("CompletedTasks");
            For<IEntityListGrid<Task>>().Add<PendingTaskGrid>().Named("PendingTasks");

            For<IEntityListGrid<User>>().Use<EmployeeListGrid>();
            For<IEntityListGrid<User>>().Add<AdminListGrid>().Named("Admins");
            For<IEntityListGrid<User>>().Add<FacilitiesListGrid>().Named("AddUpdate");

            For<IEntityListGrid<PurchaseOrder>>().Use<PurchaseOrderListGrid>();
            For<IEntityListGrid<PurchaseOrder>>().Add<CompletedPurchaseOrderListGrid>().Named("Completed");


            For<IEntityListGrid<PurchaseOrderLineItem>>().Use<PurchaseOrderLineItemGrid>();
            For<IEntityListGrid<PurchaseOrderLineItem>>().Add<ReceivePurchaseOrderLineItemGrid>().Named("Recieve");
            For<IEntityListGrid<PurchaseOrderLineItem>>().Add<CompetedPurchaseOrderLineItemGrid>().Named("Completed");
            For<IEntityListGrid<Material>>().Use<MaterialListGrid>();
            For<IEntityListGrid<Chemical>>().Use<ChemicalListGrid>();
            For<IEntityListGrid<Fertilizer>>().Use<FertilizerListGrid>();
            For<IRouteTokenConfig>().Add<FieldsRouteTokenList>();

            For<IEmailTemplateHandler>().Use<EmployeeDailyTaskHandler>().Named("Daily TasksHandler");

        }
    }
}
