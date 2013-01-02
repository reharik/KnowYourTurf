using System;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using CC.Core.DomainTools;
using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Web.Config;
using KnowYourTurf.Web.Controllers;
using Elmah;
using StructureMap;
using System.Linq;

namespace KnowYourTurf.Web
{

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ElmahHandledErrorLoggerFilter());
            filters.Add(new HandleErrorAttribute());
        }
        // ELMAH Filtering
        protected void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            FilterError404(e);
        }

        protected void ErrorMail_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            FilterError404(e);
        }

        // Dismiss 404 errors for ELMAH
        private void FilterError404(ExceptionFilterEventArgs e)
        {
            if (e.Exception.GetBaseException() is HttpException)
            {
                HttpException ex = (HttpException)e.Exception.GetBaseException();
                    
                if (ex.GetHttpCode() == 404)
                    e.Dismiss();
            }
//            if (e.Exception.Source != "CATCH RAISED")
//            {
//                Response.Redirect(@"/Error/Trouble"); //...nothing seems to get to error page anyway    
//            }
        }
        //end Elmah

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("elmah.axd");
            routes.MapRoute(
               "KnowYourTurf",
               "KnowYourTurf",
               new { controller = "KnowYourTurf", action = "Home" }); routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{EntityId}", // URL with parameters
                new { controller = "Login", action = "Login", EntityId = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
            Bootstrapper.Bootstrap();
        }

        protected void Application_EndRequest()
        {
            var unitOfWork = ObjectFactory.Container.GetInstance<IUnitOfWork>();
            unitOfWork.Dispose();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            var userData = authTicket.UserData;

            CustomIdentitiy identity = new CustomIdentitiy(authTicket);
            CustomPrincipal principal = new CustomPrincipal(identity, userData);
            HttpContext.Current.User = principal;
            Thread.CurrentPrincipal = principal;
        }
    }

    public class ElmahHandledErrorLoggerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Long only handled exceptions, because all other will be caught by ELMAH anyway.
            if (context.ExceptionHandled)
                ErrorSignal.FromCurrentContext().Raise(context.Exception);
        }
    }
}