using System.Web.Mvc;

namespace KnowYourTurf.Web.Controllers
{
    public class TestController: Controller
    {
         public ActionResult test()
         {
             return View(new TestModel {Item = new Test()});
         }
    }
    public class TestModel
    {
        public Test Item { get; set; }
    }
    public class Test
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}