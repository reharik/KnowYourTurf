using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace KnowYourTurf.Core.Domain.Tools.CustomAttributes
{
    public class LogAttribute : ActionFilterAttribute
    {
        protected DateTime start_time;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            start_time = DateTime.Now;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            RouteData routeData = filterContext.RouteData;
            TimeSpan duration = (DateTime.Now - start_time);
            string Controller = (string)routeData.Values["controller"];
            string Action = (string)routeData.Values["action"];
            DateTime CreatedAt = DateTime.Now;
            //Save all your required values, including user id and whatnot here.
            //The duration variable will allow you to see expensive page loads on the controller and whatnot, very handy when clients complain about something being slow.
        }
    }
}