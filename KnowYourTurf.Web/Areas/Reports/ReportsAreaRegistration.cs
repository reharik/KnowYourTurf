using System.Web.Mvc;

namespace KnowYourTurf.Web.Areas.Reports
{
    public class ReportsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Reports";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Reports_default",
                "Reports/{controller}/{action}/{EntityId}",
                new { action = "Index", EntityId = UrlParameter.Optional }
            );
        }
    }
}
