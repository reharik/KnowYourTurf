using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Domain.Tools;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Services;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class LoginController:Controller
    {
        private readonly ISecurityDataService _securityDataService;
        private readonly IAuthenticationContext _authenticationContext;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IContainer _container;
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public LoginController(ISecurityDataService securityDataService,
            IAuthenticationContext authenticationContext,
            IEmailTemplateService emailTemplateService,
            IContainer container,
            IRepository repository,
            ISaveEntityService saveEntityService)
        {
            _securityDataService = securityDataService;
            _authenticationContext = authenticationContext;
            _emailTemplateService = emailTemplateService;
            _container = container;
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult Login()
        {
            var loginViewModel = new LoginViewModel
                                     {
                                     };
            return View(loginViewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel input)
        {
            var notification = new Notification {Message = WebLocalizationKeys.INVALID_USERNAME_OR_PASSWORD.ToString()};

            try
            {
                if (input.HasCredentials())
                {
                    var redirectUrl = string.Empty;
                    var user = _securityDataService.AuthenticateForUserId(input.UserName, input.Password);
                    if (user != null)
                    {
                        _authenticationContext.ThisUserHasBeenAuthenticated(user, input.RememberMe);
                        notification.Success = true;
                        notification.Message = string.Empty;
                        notification.Redirect = true;
                        notification.RedirectUrl = user.UserRoles.Any(x=>x.Name=="Facilities")
                            ?"/KnowYourTurf/Home#/EventCalendar/EventCalendar"
                            : "/KnowYourTurf/Home#/EmployeeDashboard/ViewEmployee/"+user.EntityId;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Notification { Message = WebLocalizationKeys.ERROR_UNEXPECTED.ToString() };
                ex.Source = "CATCH RAISED";
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return Json(notification);
        }
            
//            
//            if (input.HasCredentials())
//            {
//                var user = _securityDataService.AuthenticateForUserId(input.UserName, input.Password);
//                if(user!=null)
//                {
//                    var redirectUrl = _authenticationContext.ThisUserHasBeenAuthenticated(user, false);
//                    return Redirect(redirectUrl);
//                }
//            }
//            return Json(notification);
  //      }

        public ActionResult Log_in(LoginViewModel input)
        {
            var user = _repository.Find<User>(input.EntityId);
            if(user.UserLoginInfo.ByPassToken!=input.Guid)
            {
                return RedirectToAction("Login");
            }
            var redirectUrl = _authenticationContext.ThisUserHasBeenAuthenticated(user,false);
            user.UserLoginInfo.ByPassToken = Guid.Empty;
            var crudManager = _saveEntityService.ProcessSave(user);
            crudManager.Finish();

            return Redirect(redirectUrl);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        
    }

    

    public class LoginViewModel : ViewModel
    {
        public Guid Guid { get; set; }
        public string SiteName { get { return CoreLocalizationKeys.SITE_NAME.ToString(); } }
        [ValidateNonEmpty]
        public string UserName { get; set; }
        [ValidateNonEmpty]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string RegisterUrl { get; set; }
        public string ForgotPasswordUrl { get; set; }
        public string ForgotPasswordTitle { get; set; }
        
        public bool HasCredentials()
        {
            return UserName.IsNotEmpty() && Password.IsNotEmpty();
        }
    }

    public class RegisterViewModel : ViewModel
    {
        [ValidateNonEmpty]
        public string FirstName { get; set; }
        [ValidateNonEmpty]
        public string LastName { get; set; }
        [ValidateNonEmpty, ValidateEmail]
        public string Email { get; set; }
    }
}