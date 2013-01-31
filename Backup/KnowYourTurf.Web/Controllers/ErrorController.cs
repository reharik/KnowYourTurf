using System.Web.Mvc;
using KnowYourTurf.Core;

namespace KnowYourTurf.Web.Controllers
{
    public class ErrorController:Controller  
    {
        public ActionResult Trouble()
        {
            return View("Error");
        }
    }
}