using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Services;
using StructureMap;

namespace KnowYourTurf.Web.Filters
{
    public class AddUserToViewFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var firstParameter = filterContext.ActionParameters.Values.FirstOrDefault();
            var viewModel = firstParameter as ViewModel;
            if(viewModel!=null)
            {
                var sessionContext = ObjectFactory.Container.GetInstance<ISessionContext>();
                viewModel.User = sessionContext.GetCurrentUser();
            }
            base.OnActionExecuting(filterContext);
        }
    }
}