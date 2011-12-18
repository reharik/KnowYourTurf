using System.Web.Mvc;
using System.Web.Routing;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using Rhino.Security.Interfaces;
using StructureMap;

namespace KnowYourTurf.Web.Filters
{
    public class AdminFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sessionContext = ObjectFactory.Container.GetInstance<ISessionContext>();
            var authorizationService = ObjectFactory.Container.GetInstance<IAuthorizationService>();
            var currentUser = sessionContext.GetCurrentUser();

            if(!authorizationService.IsAllowed(currentUser, "/AdminOrGreater"))
            {
                var values = new RouteValueDictionary(new
                {
                    action = "Home",
                    controller = "Home"
                });
                filterContext.Result = new RedirectToRouteResult(values);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}