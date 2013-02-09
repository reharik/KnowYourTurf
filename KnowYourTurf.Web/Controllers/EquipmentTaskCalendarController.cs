using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using CC.Core;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class EquipmentTaskCalendarController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISelectListItemService _selectListItemService;

        public EquipmentTaskCalendarController(IRepository repository,ISaveEntityService saveEntityService,ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _selectListItemService = selectListItemService;
        }

        public ActionResult EquipmentTaskCalendar_Template(CalendarViewModel input)
        {
            return View("EquipmentTaskCalendar", new CalendarViewModel());
        }
        public ActionResult EquipmentTaskCalendar(ViewModel input)
        {
            var taskTypes = _selectListItemService.CreateList<EquipmentTaskType>(x => x.Name, x => x.EntityId, true);
            var model = new CalendarViewModel
            {
                _TaskTypeList = taskTypes,
                CalendarDefinition = new CalendarDefinition
                                                   {
                                                       Url = UrlContext.GetUrlForAction<EquipmentTaskCalendarController>(x => x.Events(null))+"?RootId="+input.RootId,
                                                       AddUpdateTemplateUrl = UrlContext.GetUrlForAction<EquipmentTaskController>(x => x.AddUpdate_Template(null)),
                                                       AddUpdateUrl = UrlContext.GetUrlForAction<EquipmentTaskController>(x => x.AddUpdate(null)),
                                                       AddUpdateRoute = "equipmentTask",
                                                       DisplayTemplateUrl = UrlContext.GetUrlForAction<EquipmentTaskController>(x => x.Display_Template(null)),
                                                       DisplayUrl = UrlContext.GetUrlForAction<EquipmentTaskController>(x => x.Display(null)),
                                                       DisplayRoute = "equipmentTaskdisplay",
                                                       DeleteUrl = UrlContext.GetUrlForAction<EquipmentTaskController>(x => x.Delete(null)),
                                                       EventChangedUrl = UrlContext.GetUrlForAction<EquipmentTaskCalendarController>(x => x.EventChanged(null))
                                                   }
                       };
            return new CustomJsonResult(model);
        }

        public JsonResult EventChanged(EquipmentTaskChangedViewModel input)
        {
            var equipmentTask = _repository.Find<EquipmentTask>(input.EntityId);
            equipmentTask.ScheduledDate = input.ScheduledDate;
            equipmentTask.EndTime = input.EndTime;
            equipmentTask.StartTime = input.StartTime;
            var crudManager = _saveEntityService.ProcessSave(equipmentTask);
            var notification = crudManager.Finish();
            return new CustomJsonResult(notification);
        }

        public JsonResult Events(GetEventsViewModel input)
        {
            var events = new List<CalendarEvent>();
            var startDateTime = DateTimeUtilities.ConvertFromUnixTimestamp(input.start);
            var endDateTime = DateTimeUtilities.ConvertFromUnixTimestamp(input.end);
            var equipment = input.taskType>0? _repository.Query<Equipment>( x => x.Tasks.Any(y => y.ScheduledDate >= startDateTime && y.ScheduledDate <= endDateTime && y.TaskType.EntityId == input.taskType))
                : _repository.Query<Equipment>(x => x.Tasks.Any(y => y.ScheduledDate >= startDateTime && y.ScheduledDate <= endDateTime));
            var equipTasks = new List<EquipmentTask>();
            equipment.ForEachItem(x=> equipTasks.AddRange(x.GetAllEquipmentTasks(y => y.ScheduledDate >= startDateTime && y.ScheduledDate <= endDateTime)));
            equipTasks.ForEachItem(z => events.Add(new CalendarEvent
                                                       {
                                                           EntityId = z.EntityId,
                                                           title = z.Equipment.Name + ": " + z.TaskType.Name,
                                                           start = z.StartTime.ToString(),
                                                           end = z.EndTime.ToString(),
                                                           color = z.Equipment.EquipmentType.TaskColor
                                                       })
                );

            return new CustomJsonResult(events);
        }
    }

    public class EquipmentTaskChangedViewModel:ViewModel
    {
        public DateTime? ScheduledDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}