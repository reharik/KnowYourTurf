using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Security.Interfaces;
using StructureMap;

namespace KnowYourTurf.Web.Filters
{
    public class PermissionValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.RouteData.Values["action"];
            var controllerOperation = string.Format("/{0}", filterContext.Controller.GetType().Name);
            var actionOperation = string.Format("{0}/{1}", controllerOperation, actionName);
            var authorizationService = ObjectFactory.Container.GetInstance<IAuthorizationService>();
            if (filterContext.ActionParameters.ContainsKey("input"))
            {
                var user = ((ViewModel) filterContext.ActionParameters["input"]).User;
                if (!authorizationService.IsAllowed(user, controllerOperation))
                {
                    if (!authorizationService.IsAllowed(user, actionOperation))
                    {
                        filterContext.Controller.TempData["ErrorMessage"] =
                            string.Format("You are not authorized to perform operation: {0}", actionOperation);
                        filterContext.Result = new HttpUnauthorizedResult();
                    }
                }
            }
        }
    }
}