
using System;

namespace KnowYourTurf.Web.Areas.Reports.Controllers
{
    using System.Web.Mvc;

    using CC.Core.CoreViewModelAndDTOs;

    using Castle.Components.Validator;

    using KnowYourTurf.Core.Services;
    using KnowYourTurf.Web.Controllers;
    using KnowYourTurf.Web.Config;

    public class EmployeeDailyTasksController : AdminControllerBase
    {
        private readonly ISessionContext _sessionContext;

        public EmployeeDailyTasksController(ISessionContext sessionContext)
        {
            _sessionContext = sessionContext;
        }

        public ActionResult Display_Template(ViewModel input)
        {
            return this.View("Display", new EmployeeDailyTasksViewModel());
        }

        public CustomJsonResult Display(ViewModel input)
        {
            
            var model = new EmployeeDailyTasksViewModel
            {
                Date = DateTime.Now,
                ClientId = _sessionContext.GetClientId(),
                _Title = WebLocalizationKeys.EMPLOYEE_DAILY_TASKS.ToString(),
                ReportUrl = "/Areas/Reports/ReportViewer/EmployeeDailyTasks.aspx"
            };
            return new CustomJsonResult(model);
        }
    }

    public class EmployeeDailyTasksViewModel : ViewModel
    {
        [ValidateNonEmpty]
        public DateTime Date { get; set; }
        public string ReportUrl { get; set; }
        public int ClientId { get; set; }
    }
}
