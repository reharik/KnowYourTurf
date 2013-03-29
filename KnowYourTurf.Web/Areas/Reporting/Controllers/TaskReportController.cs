
namespace KnowYourTurf.Web.Areas.Reporting.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using CC.Core.CoreViewModelAndDTOs;
    using CC.Core.DomainTools;
    using CC.Core.Services;

    using Castle.Components.Validator;

    using KnowYourTurf.Core.Domain;
    using KnowYourTurf.Web.Controllers;
    using KnowYourTurf.Web.Config;
    using KnowYourTurf.Web.Services;

    public class TaskReportController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISelectListItemService _selectListItemService;

        public TaskReportController(IRepository repository,ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _selectListItemService = selectListItemService;
        }

        public ActionResult Display_Template(ViewModel input)
        {
            return this.View("Display", new TaskReportViewModel());
        }

        public CustomJsonResult Display(ViewModel input)
        {
            var fieldEntityIdList = _selectListItemService.CreateList<Field>(x => x.Name, x => x.EntityId, true);
            var taskTypeEntityIdList = _selectListItemService.CreateList<TaskType>(x => x.Name, x => x.EntityId, true);
            var employees = _repository.Query<User>(x => x.UserRoles.Any(y => y.Name == "Employee"));
            var employeeList = _selectListItemService.CreateList(employees, x => x.FullNameLNF, x => x.EntityId, true);
            var products = ((KYTSelectListItemService)_selectListItemService).CreateProductSelectListItems();
            var model = new TaskReportViewModel
            {
                _FieldEntityIdList = fieldEntityIdList,
                _TaskTypeEntityIdList = taskTypeEntityIdList,
                _EmployeeEntityIdList = employeeList,
                _ProductEntityIdList = products,
                _Title = WebLocalizationKeys.TASK_REPORT.ToString(),
                ReportUrl = "/Areas/Reporting/ReportViewer/TaskReport.aspx",
                ClientId = ((User)input.User).ClientId

            };
            return new CustomJsonResult(model);
        }
    }

    public class TaskReportViewModel : ViewModel
    {
        public IEnumerable<SelectListItem> _FieldEntityIdList { get; set; }
        public IEnumerable<SelectListItem> _TaskTypeEntityIdList { get; set; }
        public IEnumerable<SelectListItem> _EmployeeEntityIdList { get; set; }
        public GroupedSelectViewModel _ProductEntityIdList { get; set; }
        public int ProductEntityId { get; set; }
        public int FieldEntityId { get; set; }
        public int TaskTypeEntityId { get; set; }
        public int EmployeeEntityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReportUrl { get; set; }
        public int ClientId { get; set; }
    }
}
