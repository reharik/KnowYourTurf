using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;
using StructureMap;
using System.Linq;

namespace KnowYourTurf.Web.Filters
{
    public class AddUserToViewModelFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var repository = ObjectFactory.Container.GetInstance<IRepository>();
            var customPrincipal = (CustomPrincipal)filterContext.HttpContext.User;
            if (filterContext.ActionParameters.Any(x => x.Value is ViewModel || x.Value.GetType().IsSubclassOf(typeof (ViewModel))))
            {
                var user = repository.Find<User>(customPrincipal.UserId);
                ((ViewModel)filterContext.ActionParameters["input"]).User = user;
            }
        }
    }
}