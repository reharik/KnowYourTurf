using System.Web.Mvc;
using System.Web.Routing;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using StructureMap;

namespace KnowYourTurf.Web.Filters
{
    public class AdminFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sessionContext = ObjectFactory.Container.GetInstance<ISessionContext>();
            var currentUser = sessionContext.GetCurrentUser();

            if(currentUser.UserRoles.IsEmpty() || !currentUser.UserRoles.Contains(UserRole.Admin.ToString()))
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