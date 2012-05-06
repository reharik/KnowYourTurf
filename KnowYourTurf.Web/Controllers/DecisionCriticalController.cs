using System.Collections.Generic;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Web.Services.ViewOptions;

namespace KnowYourTurf.Web.Controllers
{
    public class KnowYourTurfController:KYTController
    {
        private IViewOptionConfig _viewOptionConfig;

        public KnowYourTurfController(IViewOptionConfig viewOptionConfig)
        {
            _viewOptionConfig = viewOptionConfig;
        }

        public ActionResult Home(ViewModel input)
         {
             var knowYourTurfViewModel = new KnowYourTurfViewModel
                                                 {
                                                    SerializedRoutes = _viewOptionConfig.Build(true)
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
        public IList<ViewOption> SerializedRoutes { get; set; }

    }
}