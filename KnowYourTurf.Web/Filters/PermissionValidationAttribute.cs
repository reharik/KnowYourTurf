using System.Web.Mvc;
using KnowYourTurf.Security.Interfaces;
using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;
using StructureMap;

namespace KnowYourTurf.Web.Filters
{
    public class PermissionValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var actionName = filterContext.RouteData.Values["action"];
            var controllerOperation = string.Format("/{0}",filterContext.Controller.GetType().Name);
            var actionOperation = string.Format("{0}/{1}", controllerOperation, actionName);
            var authorizationService = ObjectFactory.Container.GetInstance<IAuthorizationService>();
            var repository = ObjectFactory.Container.GetInstance<IRepository>();
            var customPrincipal = (CustomPrincipal) filterContext.HttpContext.User;
            var user = repository.Find<User>(customPrincipal.UserId);
            if(!authorizationService.IsAllowed(user,controllerOperation))
            {
                if (!authorizationService.IsAllowed(user, actionOperation))
                {
                    filterContext.Controller.TempData["ErrorMessage"] = string.Format("You are not authorized to perform operation: {0}", actionOperation);
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
        }
    }
}