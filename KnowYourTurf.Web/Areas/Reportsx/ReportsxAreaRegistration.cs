using System.Web.Mvc;

namespace KnowYourTurf.Web.Areas.Reports
{
    public class ReportsxAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Reportsx";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Reportsx_default",
                "Reportsx/{controller}/{action}/{EntityId}",
                new { action = "Index", EntityId = UrlParameter.Optional }
            );
        }
    }
}
