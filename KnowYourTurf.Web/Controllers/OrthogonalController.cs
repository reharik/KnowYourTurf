using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Config;
using KnowYourTurf.Web.Models;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class OrthogonalController : KYTController
    {
        private readonly IMenuConfig _menuConfig;
        private readonly ISessionContext _sessionContext;

        public OrthogonalController(IMenuConfig menuConfig, ISessionContext sessionContext)
        {
            _menuConfig = menuConfig;
            _sessionContext = sessionContext;
        }

        public PartialViewResult KnowYourTurfHeader()
        {
            User user = new User();
            if (User.Identity.IsAuthenticated)
            {
                user = _sessionContext.GetCurrentUser();
            }
            var inAdminMode = _sessionContext.RetrieveSessionObject(WebLocalizationKeys.INADMINMODE.ToString());
            if (inAdminMode == null)
            {
                _sessionContext.AddUpdateSessionItem(new SessionItem { SessionKey = WebLocalizationKeys.INADMINMODE.ToString(), SessionObject = false });
                inAdminMode = false;
            }
            HeaderViewModel model = new HeaderViewModel
            {
                User = user,
                LoggedIn = User.Identity.IsAuthenticated,
                IsAdmin = (user.UserRoles.IsNotEmpty() && user.UserRoles.Contains(UserRole.Admin.ToString())),
                InAdminMode = (bool)inAdminMode
            };
            return PartialView(model);
        }

        public PartialViewResult KnowYourTurfMenu()
        {
            var currentUser = _sessionContext.GetCurrentUser();
            var inAdminMode = _sessionContext.RetrieveSessionObject(WebLocalizationKeys.INADMINMODE.ToString());
            if (currentUser.UserRoles.IsNotEmpty() && currentUser.UserRoles.Contains(UserRole.Admin.ToString()) && (bool)inAdminMode)
            {
                IMenuConfig SetupMenuConfig = ObjectFactory.Container.GetInstance<IMenuConfig>("SetupMenu");
                return PartialView(new MenuViewModel
                {
                    MenuItems = SetupMenuConfig.Build()
                });
            }         
            return PartialView(new MenuViewModel
                                   {
                                       MenuItems = _menuConfig.Build()
                                   });
        }

        public ActionResult TurnOnOffAdmin()
        {
            var currentUser = _sessionContext.GetCurrentUser();
            var inAdminMode = _sessionContext.RetrieveSessionItem(WebLocalizationKeys.INADMINMODE.ToString());
            inAdminMode.SessionObject = !(bool) inAdminMode.SessionObject;
            _sessionContext.AddUpdateSessionItem(inAdminMode);
            return currentUser.UserRoles.IsEmpty() || !currentUser.UserRoles.Contains(UserRole.Admin.ToString())
                && (bool)inAdminMode.SessionObject
                    ? RedirectToAction("Enum", "ListType")
                    : RedirectToAction("Home", "Home");
        }
    }

  
}