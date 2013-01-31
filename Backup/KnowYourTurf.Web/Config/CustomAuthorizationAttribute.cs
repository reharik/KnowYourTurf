using System.Web.Mvc;
using CC.Core.Html;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Web.Controllers;
using StructureMap;

namespace KnowYourTurf.Web.Config
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new AjaxAwareRedirectResult(UrlContext.GetUrlForAction<LoginController>(x=>x.Login(null)));
            }
        }
    }
}