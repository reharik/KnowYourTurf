using System.Collections.Generic;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Web.Services.ViewOptions;

namespace KnowYourTurf.Web.Controllers
{
    public class KnowYourTurfController:KYTController
    {
        private IRouteTokenConfig _routeTokenConfig;

        public KnowYourTurfController(IRouteTokenConfig routeTokenConfig)
        {
            _routeTokenConfig = routeTokenConfig;
        }

        public ActionResult Home(ViewModel input)
         {
             var knowYourTurfViewModel = new KnowYourTurfViewModel
                                                 {
                                                    SerializedRoutes = _routeTokenConfig.Build(true)
                                                 };
             return View(knowYourTurfViewModel);
         }
    }

    public class KnowYourTurfViewModel : ViewModel
    {
        public string c { get; set; }
        public string u { get; set; }
        public string mode { get; set; }
        public string FirstTimeUrl { get; set; }
        public string UserProfileUrl { get; set; }
        public IList<RouteToken> SerializedRoutes { get; set; }

    }
}