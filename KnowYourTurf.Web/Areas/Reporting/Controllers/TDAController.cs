namespace KnowYourTurf.Web.Areas.Reporting.Controllers
{
    using System;
    using System.Web.Mvc;

    using CC.Core.CoreViewModelAndDTOs;

    using Castle.Components.Validator;

    using KnowYourTurf.Core.Services;
    using KnowYourTurf.Web.Config;
    using KnowYourTurf.Web.Controllers;

    public class TDAController : AdminControllerBase
    {
        private readonly ISessionContext _sessionContext;

        public TDAController(ISessionContext sessionContext)
        {
            this._sessionContext = sessionContext;
        }

        public ActionResult Display_Template(ViewModel input)
        {
            return this.View("Display", new TDAViewModel());
        }

        public CustomJsonResult Display(ViewModel input)
        {
            
            var model = new TDAViewModel
                            {
                                Date = DateTime.Now,
                                ClientId = this._sessionContext.GetClientId(),
                                _Title = WebLocalizationKeys.TDA.ToString(),
                                ReportUrl = "/Areas/Reporting/ReportViewer/TDA.aspx"
                            };
            return new CustomJsonResult(model);
        }
    }

    public class TDAViewModel : ViewModel
    {
        [ValidateNonEmpty]
        public DateTime Date { get; set; }
        public string ReportUrl { get; set; }
        public int ClientId { get; set; }
    }
}