using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Services;
using NHibernate;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class LoginController:Controller
    {
        private readonly ISecurityDataService _securityDataService;
        private readonly IAuthenticationContext _authenticationContext;
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public LoginController(ISecurityDataService securityDataService,
            IAuthenticationContext authenticationContext,
            ISaveEntityService saveEntityService,
            IRepository repository)
        {
            _securityDataService = securityDataService;
            _authenticationContext = authenticationContext;
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult Login()
        {
            var loginViewModel = new LoginViewModel
                                     {
                                         _saveUrl = UrlContext.GetUrlForAction<LoginController>(x=>x.Login(null))
                                     };
            return View(loginViewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel input)
        {
            var notification = new Notification {Message = WebLocalizationKeys.INVALID_USERNAME_OR_PASSWORD.ToString()};
            try
            {
                if (input.UserName.IsNotEmpty() && input.Password.IsNotEmpty())
                {
                    var user = _securityDataService.AuthenticateForUserId(input.UserName, input.Password);
                    if (user != null)
                    {
                        _authenticationContext.ThisUserHasBeenAuthenticated(user, input.RememberMe);
                        notification.Success = true;
                        notification.Message = string.Empty;
                        notification.Redirect = true;
                        notification.RedirectUrl = user.UserRoles.Any(x=>x.Name=="Facilities")
                            ?"/KnowYourTurf/Home#/eventcalendar/0/0/"+user.Client.EntityId
                            : "/KnowYourTurf/Home#/employeedashboard/"+user.EntityId;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Notification { Message = WebLocalizationKeys.ERROR_UNEXPECTED.ToString() };
                ex.Source = "CATCH RAISED";
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new CustomJsonResult(notification);
        }

        public ActionResult Log_in(LoginViewModel input)
        {
            var sf = ObjectFactory.GetInstance<ISessionFactory>();
            var session = sf.OpenSession();
            var unitOfWork = new UnitOfWork(session);
            var repository = new Repository(unitOfWork, null);
            var user = repository.Find<User>(input.EntityId);
            if(user.UserLoginInfo.ByPassToken!=input.Guid)
            {
                return RedirectToAction("Login");
            }
            var impersonator = repository.Find<User>(input.ImpersonatorId);
            _authenticationContext.ThisUserHasBeenAuthenticated(user, false, impersonator);
            user.UserLoginInfo.ByPassToken = Guid.Empty;
            user.ChangedDate = DateTime.Now;
            user.ChangedBy = impersonator;
            
            repository.Save(user);
            repository.Commit();
            var redirectUrl = user.UserRoles.Any(x => x.Name == "Facilities")
                            ? "/KnowYourTurf/Home#/eventcalendar/0/0/" + user.Client.EntityId
                            : "/KnowYourTurf/Home#/employeedashboard/" + user.EntityId;
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
        [ValidateNonEmpty]
        public string UserName { get; set; }
        [ValidateNonEmpty]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ForgotPasswordUrl { get; set; }
        public string _saveUrl { get; set; }
        public Guid Guid { get; set; }
        public int ImpersonatorId { get; set; }
    }

}
