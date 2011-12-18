using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Html;

namespace KnowYourTurf.Web.Controllers
{
    public class KnowYourTurfController:KYTController
    {
         public ActionResult Home(ViewModel input)
         {
             var knowYourTurfViewModel = new KnowYourTurfViewModel
                                                 {
//                                                     UserProfileUrl = UrlContext.GetUrlForAction<TrainerController>(x => x.AddUpdate(null))
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

    }
}