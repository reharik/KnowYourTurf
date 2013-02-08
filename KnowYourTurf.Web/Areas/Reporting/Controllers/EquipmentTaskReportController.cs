
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

    public class EquipmentTaskReportController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISelectListItemService _selectListItemService;

        public EquipmentTaskReportController(IRepository repository,ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _selectListItemService = selectListItemService;
        }

        public ActionResult Display_Template(ViewModel input)
        {
            return this.View("Display", new EquipmentTaskReportViewModel());
        }

        public CustomJsonResult Display(ViewModel input)
        {
            var equipmentEntityIdList = _selectListItemService.CreateList<Equipment>(x => x.Name, x => x.EntityId, true);
            var taskTypeEntityIdList = _selectListItemService.CreateList<TaskType>(x => x.Name, x => x.EntityId, true);
            var employees = _repository.Query<User>(x => x.UserRoles.Any(y => y.Name == "Employee"));
            var employeeList = _selectListItemService.CreateList(employees, x => x.FullNameLNF, x => x.EntityId, true);
            var model = new EquipmentTaskReportViewModel
            {
                _EquipmentEntityIdList = equipmentEntityIdList,
                _TaskTypeEntityIdList = taskTypeEntityIdList,
                _EmployeeEntityIdList = employeeList,
                _Title = WebLocalizationKeys.EQUIPMENT_TASK_REPORT.ToString(),
                ReportUrl = "/Areas/Reporting/ReportViewer/EquipmentTaskReport.aspx",
                ClientId = ((User)input.User).ClientId

            };
            return new CustomJsonResult(model);
        }
    }

    public class EquipmentTaskReportViewModel : ViewModel
    {
        public IEnumerable<SelectListItem> _EquipmentEntityIdList { get; set; }
        public IEnumerable<SelectListItem> _TaskTypeEntityIdList { get; set; }
        public IEnumerable<SelectListItem> _EmployeeEntityIdList { get; set; }
        public int EquipmentEntityId { get; set; }
        public int TaskTypeEntityId { get; set; }
        public int EmployeeEntityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReportUrl { get; set; }
        public int ClientId { get; set; }
        public bool Complete { get; set; }
    }
}
