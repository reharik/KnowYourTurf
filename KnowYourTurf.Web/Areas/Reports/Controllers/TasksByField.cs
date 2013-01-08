using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Services;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web;
using KnowYourTurf.Web.Controllers;

namespace MethodFitness.Web.Areas.Reports.Controllers
{
    using KnowYourTurf.Web.Config;

    public class TasksByFieldController : AdminControllerBase
    {
        private readonly IRepository _repository;
        private readonly ISelectListItemService _selectListItemService;

        public TasksByFieldController(IRepository repository, ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _selectListItemService = selectListItemService;
        }
        public ActionResult Display_Template(ViewModel input)
        {
            return View("Display", new TasksByFieldViewModel());
        }

        public CustomJsonResult Display(ViewModel input)
        {
            var trainers = _repository.Query<User>(x => x.UserRoles.Any(y => y.Name == "Trainer"));
            var model = new TasksByFieldViewModel
            {
//                StartDate = DateTime.Now,
//                EndDate = DateTime.Now,
//                _TrainerEntityIdList = _selectListItemService.CreateList(trainers, x => x.FullNameFNF, x => x.EntityId, true),
//                _Title = WebLocalizationKeys.TRAINER_METRIC.ToString(),
//                ReportUrl = "/Areas/Reports/ReportViewer/TrainerMetric.aspx"
            };
            return new CustomJsonResult(model);
        }
    }

    public class TasksByFieldViewModel : ViewModel
    {
//        public IEnumerable<SelectListItem> _TrainerEntityIdList { get; set; }
//        [ValidateNonEmpty]
//        public int TrainerEntityId { get; set; }
//        [ValidateNonEmpty]
//        public DateTime StartDate { get; set; }
//        [ValidateNonEmpty]
//        public DateTime EndDate { get; set; }
//        public string ReportUrl { get; set; }
    }
}
