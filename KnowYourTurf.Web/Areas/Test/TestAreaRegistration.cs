using System.Web.Mvc;

namespace KnowYourTurf.Web.Areas.Test
{
    public class TestAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Test";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Test_default",
                "Test/{controller}/{action}/{EntityId}",
                new { action = "Index", EntityId = UrlParameter.Optional }
            );
        }
    }
}
