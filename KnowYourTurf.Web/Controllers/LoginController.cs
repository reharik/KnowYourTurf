using System;
using System.Web.Mvc;
using System.Web.Security;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISecurityDataService _securityDataService;
        private readonly IAuthenticationContext _authContext;
        private readonly ISessionContext _sessionContext;

        public LoginController(ISecurityDataService securityDataService, IAuthenticationContext authContext,ISessionContext sessionContext)
        {
            _securityDataService = securityDataService;
            _authContext = authContext;
            _sessionContext = sessionContext;
        }

        public ActionResult Login()
        {
            var isAjaxCall = Request.Params["ajaxCall"];
            if(isAjaxCall == "True") Response.Redirect(Request.Url.ToString()); //return Json(new Notification{Success=false,SessionLoggedOut = true},JsonRequestBehavior.AllowGet);
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel input)
        {
            var notification = new Notification
                                   {Message = WebLocalizationKeys.INVALID_USERNAME_OR_PASSWORD.ToString()};
            if (input.HasCredentials())
            {
                var redirectUrl = string.Empty;
                var user = _securityDataService.AuthenticateForUserId(input.UserName, input.Password);
                if (user != null)
                {
                    redirectUrl = _authContext.ThisUserHasBeenAuthenticated(user, input.RememberMe);
                    notification.Success = true;
                    notification.Message = string.Empty;
                    notification.Redirect = true;
                   _sessionContext.AddUpdateSessionItem(
                    new SessionItem
                        {
                            SessionKey = WebLocalizationKeys.USER_ROLES.ToString(),
                            SessionObject = user.UserRoles
                        }
                    );
                }
                if (redirectUrl != "/Home/Home")
                {
                    notification.RedirectUrl = redirectUrl;
                }
                else if (user.UserType == UserType.Employee.ToString())
                {
                    notification.RedirectUrl = UrlContext.GetUrlForAction<EmployeeDashboardController>(x => x.ViewEmployee(null)) + "/" + user.EntityId;
                }
                else
                {
                    notification.RedirectUrl = redirectUrl;
                }
            }
            return Json(notification);

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }

    [Serializable]
    public class LoginViewModel : ViewModel
    {
        public string SiteName { get { return CoreLocalizationKeys.SITE_NAME.ToString(); } }
        [ValidateNonEmpty]
        public string UserName { get; set; }
        [ValidateNonEmpty]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool HasCredentials()
        {
            return UserName.IsNotEmpty() && Password.IsNotEmpty();
        }

    }
}
