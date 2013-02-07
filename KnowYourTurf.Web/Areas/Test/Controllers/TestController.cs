
namespace KnowYourTurf.Web.Areas.Test.Controllers
{
    using System.Web.Mvc;

    using CC.Core.CoreViewModelAndDTOs;

    using KnowYourTurf.Web.Controllers;

    public class TestController : KYTController
    {

        public TestController()
        {
        }
        public ActionResult Display_Template(ViewModel input)
        {
            return this.View("Display", new ViewModel());
        }

      
    }
}
