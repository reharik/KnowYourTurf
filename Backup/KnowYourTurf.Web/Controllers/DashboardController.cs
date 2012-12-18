using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using KnowYourTurf.Core;
using KnowYourTurf.Web.Controllers;

namespace DecisionCritical.Web.Controllers
{
    public class DashboardController:KYTController
    {
         public ActionResult Dashboard(ViewModel input)
         {
             return View(new DashboardViewModel());
         }
    }

    public class DashboardViewModel : ViewModel
    {
        public string c { get; set; }
        public string u { get; set; }
        public string mode { get; set; }
        public string FirstTimeUrl { get; set; }
    }
}