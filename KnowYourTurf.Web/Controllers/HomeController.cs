using System;
using System.Web.Mvc;
using KnowYourTurf.Core;

namespace KnowYourTurf.Web.Controllers
{
    public class HomeController:KYTController
    {
        public ActionResult Home()
        {
            return View(new ViewModel());
        }
    }
}