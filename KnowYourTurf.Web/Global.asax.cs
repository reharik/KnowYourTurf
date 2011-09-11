
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Web.Config;
using KnowYourTurf.Web.Controllers;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace KYT.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
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
}