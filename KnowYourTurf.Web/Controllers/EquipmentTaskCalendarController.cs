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
    public class EquipmentTaskCalendarController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public EquipmentTaskCalendarController(IRepository repository,ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult EquipmentTaskCalendar(ViewModel input)
        {
            var model = new CalendarViewModel
                       {
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
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public JsonResult EventChanged(EquipmentTaskChangedViewModel input)
        {
            var equipmentTask = _repository.Find<EquipmentTask>(input.EntityId);
            equipmentTask.ScheduledDate = input.ScheduledDate;
//            equipmentTask.EndTime = input.EndTime;
//            equipmentTask.StartTime = input.StartTime;
            var crudManager = _saveEntityService.ProcessSave(equipmentTask);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Events(GetEventsViewModel input)
        {
            var events = new List<CalendarEvent>();
            var startDateTime = DateTimeUtilities.ConvertFromUnixTimestamp(input.start);
            var endDateTime = DateTimeUtilities.ConvertFromUnixTimestamp(input.end);
            var equipment = _repository.Query<Equipment>( x => x.Tasks.Any(y => y.ScheduledDate >= startDateTime && y.ScheduledDate <= endDateTime));
            var equipTasks = new List<EquipmentTask>();
            equipment.ForEachItem(x=> equipTasks.AddRange(x.GetAllEquipmentTasks(y => y.ScheduledDate >= startDateTime && y.ScheduledDate <= endDateTime)));
            equipTasks.ForEachItem(z => events.Add(new CalendarEvent
                                                       {
                                                           EntityId = z.EntityId,
                                                           title = z.Equipment.Name + ": " + z.TaskType.Name,
                                                           start = z.ScheduledDate.ToString(),
                                                           end = z.ScheduledDate.ToString(),
                                                       })
                );

            return Json(events, JsonRequestBehavior.AllowGet);
        }
    }

    public class EquipmentTaskChangedViewModel:ViewModel
    {
        public DateTime? ScheduledDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}