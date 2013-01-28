using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KnowYourTurf.Web.Controllers
{
    public class TestController: Controller
    {
         public ActionResult test()
         {
             return View(new TestModel {Item = new Test
                                                   {
                                                       FirstName = "fred",
                                                       Age = 32,
                                                       BDay=DateTime.Parse("1/5/1972"),
                                                       test2s = new[] {new Test2{Name = "fuck",Age = 2},new Test2{Name = "nutz",Age = 3}}
                                                   }});
         }
        public ActionResult save(TestModel model)
        {
            return null;
        }
    } 
    public class TestModel
    {
        public Test Item { get; set; }
    }
    public class Test
    {
        public IEnumerable<Test2> test2s { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public DateTime BDay { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class Test2
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}