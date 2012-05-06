using System.Web.Mvc;
using KnowYourTurf.Core;

namespace KnowYourTurf.Web.Controllers
{
    public class ForumController:KYTController
    {
        public ActionResult Forum(ViewModel input)
         {
             return View(new ViewModel());
         }
    }
}