using System.Web.Mvc;
using System.Web.Routing;

namespace DecisionCritical.Core.Config
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                                        {
                                                { "client", filterContext.RouteData.Values[ "client" ] },
                                                { "controller", "Login" },
                                                { "action", "Login" },
                                                { "ReturnUrl", filterContext.HttpContext.Request.RawUrl },
                                                {"area",null},
                                                {"ajaxCall",filterContext.HttpContext.Request.IsAjaxRequest()}
                                        });
            }
        }
    }
}