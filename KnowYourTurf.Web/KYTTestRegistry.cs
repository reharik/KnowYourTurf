using FubuMVC.UI;
using FubuMVC.UI.Configuration;
using FubuMVC.UI.Tags;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Domain.Persistence;
using KnowYourTurf.Core.Domain.Tools;
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
using PurchaseOrderLineItemGrid = KnowYourTurf.Web.Grids.PurchaseOrderLineItemGrid;
using ReceivePurchaseOrderLineItemGrid = KnowYourTurf.Web.Grids.ReceivePurchaseOrderLineItemGrid;

namespace KnowYourTurf.Web
{
    public class KYTTestRegistry : Registry
    {
        public KYTTestRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.AssemblyContainingType(typeof(CoreLocalizationKeys));
                x.AddAllTypesOf<ICalculatorHandler>().NameBy(t => t.Name);
                x.AddAllTypesOf<IEmailTemplateHandler>().NameBy(t => t.Name);
                x.WithDefaultConventions();
            });
            For<HtmlConventionRegistry>().Add<KnowYourTurfHtmlConventions>();
            For<IServiceLocator>().Singleton().Use(new StructureMapServiceLocator());
            For<IElementNamingConvention>().Use<KnowYourTurfElementNamingConvention>();
            For(typeof(ITagGenerator<>)).Use(typeof(TagGenerator<>));
            For<TagProfileLibrary>().Singleton();

            For<INHSetupConfig>().Use<NullNHSetupConfig>();

            For<ISessionFactoryConfiguration>().Singleton()
                .Use<NullSqlServerSessionSourceConfiguration>();
            For<ISessionFactory>().Use<NullSessionFactory>();

            For<ISession>().Use<NullSession>();
            //For<IUnitOfWork>().Use(ctx => ctx.GetInstance<INHibernateUnitOfWork>());

            For<IUnitOfWork>().Use<NullNHibernateUnitOfWork>();
            For<IGetCompanyIdService>().Use<DataLoaderGetCompanyIdService>();
            
            For<ILocalizationDataProvider>().Use<LocalizationDataProvider>();
            For<IAuthenticationContext>().Use<WebAuthenticationContext>();
            For<IMenuConfig>().Use<MenuConfig>();
            For<IMenuConfig>().Add<SetupMenuConfig>().Named("SetupMenu");
            For<IAuthorizationService>().HybridHttpOrThreadLocalScoped().Use<AuthorizationService>();
            For<IAuthorizationRepository>().HybridHttpOrThreadLocalScoped().Use<AuthorizationRepository>();
            For<IPermissionsBuilderService>().HybridHttpOrThreadLocalScoped().Use<PermissionsBuilderService>();
            For<IPermissionsService>().HybridHttpOrThreadLocalScoped().Use<PermissionsService>();
            For(typeof(IGridBuilder<>)).Use(typeof(GridBuilder<>));
        }
    }
}
