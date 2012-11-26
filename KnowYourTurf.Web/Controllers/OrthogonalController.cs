using System.Collections.Generic;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Html.Menu;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Config;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class OrthogonalController : KYTController
    {
        private readonly IMenuConfig _menuConfig;
        private readonly ISessionContext _sessionContext;
        private readonly IRepository _repository;
        private readonly IContainer _container;

        public OrthogonalController(IMenuConfig menuConfig,
                ISessionContext sessionContext,
            IRepository repository,
            IContainer container)
        {
            _menuConfig = menuConfig;
            _sessionContext = sessionContext;
            _repository = repository;
            _container = container;
        }

        public PartialViewResult KnowYourTurfHeader(ViewModel input)
        {
            HeaderViewModel model = new HeaderViewModel
                                        {
                                            User = (User) input.User,
                                            LoggedIn = User.Identity.IsAuthenticated,
                                            NotificationSuccessFunction = "kyt.popupCrud.controller.success",
                                            HelpUrl = UrlContext.GetUrlForAction<OrthogonalController>(x=>x.Help(null))
                                        };
            return PartialView(model);
        }

        public ActionResult Help(ViewModel input)
        {
            return View();
        }

        //remove true param when permissions are implemented
        public PartialViewResult MainMenu(ViewModel input)
        {
            return PartialView(new MenuViewModel
            {
                MenuItems = _menuConfig.Build()
            });
        }
    }

    public class MenuViewModel
    {
        public IList<MenuItem> MenuItems { get; set; }
    }

    public class HeaderViewModel : PartialViewResult
    {
        public string SiteName { get { return CoreLocalizationKeys.SITE_NAME.ToString(); } }
        public User User { get; set; }
        public bool LoggedIn { get; set; }
        public bool IsAdmin { get; set; }
        public bool InAdminMode { get; set; }
        public string UserProfileUrl { get; set; }
        public string NotificationSettingsUrl { get; set; }
        public string NotificationSuccessFunction { get; set; }

        public string HelpUrl { get; set; }
    }
}