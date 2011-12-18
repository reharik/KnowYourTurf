//using System.Web.Mvc;
//using KnowYourTurf.Core;
//using KnowYourTurf.Core.Domain;
//using KnowYourTurf.Core.Enums;
//using KnowYourTurf.Core.Services;
//using KnowYourTurf.Web.Config;
//using KnowYourTurf.Web.Models;
//using Rhino.Security.Interfaces;
//using StructureMap;
//
//namespace KnowYourTurf.Web.Controllers
//{
//    public class OrthogonalController : Controller
//    {
//        private readonly IMenuConfig _menuConfig;
//        private readonly ISessionContext _sessionContext;
//        private readonly IAuthorizationService _authorizationService;
//
//        public OrthogonalController(IMenuConfig menuConfig, ISessionContext sessionContext,IAuthorizationService authorizationService)
//        {
//            _menuConfig = menuConfig;
//            _sessionContext = sessionContext;
//            _authorizationService = authorizationService;
//        }
//
//        public PartialViewResult KnowYourTurfHeader()
//        {
//            User user = new User();
//            if (_sessionContext.IsAuthenticated())
//            {
//                user = _sessionContext.GetCurrentUser();
//            }
//            var inAdminMode = _sessionContext.RetrieveSessionObject(WebLocalizationKeys.INADMINMODE.ToString());
//            if (inAdminMode == null)
//            {
//                _sessionContext.AddUpdateSessionItem(new SessionItem { SessionKey = WebLocalizationKeys.INADMINMODE.ToString(), SessionObject = false });
//                inAdminMode = false;
//            }
//            HeaderViewModel model = new HeaderViewModel
//            {
//                User = user,
//                LoggedIn = _sessionContext.IsAuthenticated(),
//                IsAdmin = _authorizationService.IsAllowed(user, "/AdminOrGreater"),
//                InAdminMode = (bool)inAdminMode
//            };
//            return PartialView(model);
//        }
//
//        public PartialViewResult KnowYourTurfMenu()
//        {
//            var currentUser = _sessionContext.GetCurrentUser();
//            var inAdminMode = _sessionContext.RetrieveSessionObject(WebLocalizationKeys.INADMINMODE.ToString());
//            if (_authorizationService.IsAllowed(currentUser, "/AdminOrGreater") && (bool)inAdminMode)
//            {
//                IMenuConfig SetupMenuConfig = ObjectFactory.Container.GetInstance<IMenuConfig>("SetupMenu");
//                return PartialView(new MenuViewModel
//                {
//                    MenuItems = SetupMenuConfig.Build()
//                });
//            }         
//            return PartialView(new MenuViewModel
//                                   {
//                                       MenuItems = _menuConfig.Build()
//                                   });
//        }
//
//        public ActionResult TurnOnOffAdmin()
//        {
//            var currentUser = _sessionContext.GetCurrentUser();
//            var inAdminMode = _sessionContext.RetrieveSessionItem(WebLocalizationKeys.INADMINMODE.ToString());
//            inAdminMode.SessionObject = !(bool) inAdminMode.SessionObject;
//            _sessionContext.AddUpdateSessionItem(inAdminMode);
//            return _authorizationService.IsAllowed(currentUser, "/AdminOrGreater")
//                && (bool)inAdminMode.SessionObject
//                    ? RedirectToAction("ListType", "ListTypeList")
//                    : RedirectToAction("Home", "Home");
//        }
//    }
//
//  
//}