using System.IO;
using System.Web.Mvc;
using KnowYourTurf.Web.Filters;

namespace KnowYourTurf.Web.Controllers
{

//    [AddUserToViewFilter, CustomAuthorize, PermissionValidation]
    [CustomAuthorize, AddUserToViewModelFilter, PermissionValidation]
    public class KYTController : Controller
    {
        protected string RenderViewToString()
        {
            return RenderViewToString(null, null);
        }

        protected string RenderViewToString(string viewName)
        {
            return RenderViewToString(viewName, null);
        }

        protected string RenderViewToString(object model)
        {
            return RenderViewToString(null, model);
        }

        protected string RenderViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                //ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName,null);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }

    [AdminFilter]
    public class AdminControllerBase : KYTController
    {
    }

    public class AjaxAwareRedirectResult : RedirectResult
    {
        public AjaxAwareRedirectResult(string url)
            : base(url)
        {
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                string destinationUrl = UrlHelper.GenerateContentUrl(Url, context.HttpContext);

                JsonResult result = new JsonResult()
                {
                    Data = new { LoggedOut = true, RedirectUrl = destinationUrl }
                };
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                result.ExecuteResult(context);
            }
            else
                base.ExecuteResult(context);
        }
    }
}