using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using FubuMVC.Core;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class TaskCalendarController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public TaskCalendarController(IRepository repository,ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult TaskCalendar(ViewModel input)
        {
            var model = new CalendarViewModel
                       {
                           CalendarDefinition = new CalendarDefinition
                                                   {
                                                       Url = UrlContext.GetUrlForAction<TaskCalendarController>(x => x.Events(null))+"?RootId="+input.RootId,
                                                       AddUpdateTemplateUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddUpdate_Template(null)),
                                                       AddUpdateUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddUpdate(null)),
                                                       AddUpdateRoute = "task",
                                                       DisplayTemplateUrl = UrlContext.GetUrlForAction<TaskController>(x => x.Display_Template(null)),
                                                       DisplayUrl = UrlContext.GetUrlForAction<TaskController>(x => x.Display(null)),
                                                       DisplayRoute = "taskdisplay",
                                                       DeleteUrl = UrlContext.GetUrlForAction<TaskController>(x => x.Delete(null)),
                                                       EventChangedUrl = UrlContext.GetUrlForAction<TaskCalendarController>(x => x.EventChanged(null))
                                                   }
                       };
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public JsonResult EventChanged(TaskChangedViewModel input)
        {
            var task = _repository.Find<Task>(input.EntityId);
            task.ScheduledDate = input.ScheduledDate;
            task.ScheduledEndTime = input.EndTime;
            task.ScheduledStartTime = input.StartTime;
            var crudManager = _saveEntityService.ProcessSave(task);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Events(GetEventsViewModel input)
        {
            var events = new List<CalendarEvent>();
            var startDateTime = DateTimeUtilities.ConvertFromUnixTimestamp(input.start);
            var endDateTime = DateTimeUtilities.ConvertFromUnixTimestamp(input.end);
            var category = _repository.Find<Category>(input.RootId);
            var tasks = category.GetAllTasks().Where(x => x.ScheduledDate >= startDateTime && x.ScheduledDate <= endDateTime);
            tasks.Each(x =>
                       events.Add(new CalendarEvent
                                      {
                                          EntityId = x.EntityId,
                                          title = x.Field.Abbreviation + ": " + x.TaskType.Name,
                                          start = x.ScheduledStartTime.ToString(),
                                          end = x.ScheduledEndTime.ToString(),
                                          color = x.Field.FieldColor
                                      })
                );
            return Json(events, JsonRequestBehavior.AllowGet);
        }
    }

    public class TaskChangedViewModel:ViewModel
    {
        public DateTime? ScheduledDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}