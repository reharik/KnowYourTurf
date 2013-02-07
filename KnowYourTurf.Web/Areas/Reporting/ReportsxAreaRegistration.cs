namespace KnowYourTurf.Web.Areas.Reporting
{
    using System.Web.Mvc;

    public class ReportingAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Reporting";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Reporting_default",
                "Reporting/{controller}/{action}/{EntityId}",
                new { action = "Index", EntityId = UrlParameter.Optional }
            );
        }
    }
}
