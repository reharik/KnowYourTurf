using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FubuMVC.Core;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class EventCalendarController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public EventCalendarController(IRepository repository,ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult EventCalendar(ViewModel input)
        {
            var model = new CalendarViewModel
                       {
                           DeleteUrl = UrlContext.GetUrlForAction<EventController>(x => x.Delete(null)),
                           CalendarDefinition = new CalendarDefinition
                                                   {
                                                       Url = UrlContext.GetUrlForAction<EventCalendarController>(x => x.Events(null))+"?ParentId="+input.ParentId,
                                                       AddUpdateUrl = UrlContext.GetUrlForAction<EventController>(x => x.AddUpdate(null)),
                                                       DisplayUrl = UrlContext.GetUrlForAction<EventController>(x => x.Display(null)),
                                                       EventChangedUrl = UrlContext.GetUrlForAction<EventCalendarController>(x => x.EventChanged(null)) 
                                                   },
                           RootId = input.ParentId
                       };
            return View(model);
        }

        public JsonResult EventChanged(EventChangedViewModel input)
        {
            var _event = _repository.Find<Event>(input.EntityId);
            _event.ScheduledDate = input.ScheduledDate;
            _event.StartTime = input.StartTime;
            _event.EndTime = input.EndTime;
            var crudManager = _saveEntityService.ProcessSave(_event);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Events(GetEventsViewModel input)
        {
            var eventsItems = new List<CalendarEvent>();
            var startDateTime = DateTimeUtilities.ConvertFromUnixTimestamp(input.start);
            var endDateTime = DateTimeUtilities.ConvertFromUnixTimestamp(input.end);
            var category = _repository.Find<Category>(input.ParentId);
            var events = category.Events.Where(x => x.ScheduledDate >= startDateTime && x.ScheduledDate <= endDateTime);
            events.Each(x =>
                       eventsItems.Add(new CalendarEvent
                                      {
                                          EntityId = x.EntityId,
                                          title = x.Field.Abbreviation+": "+ x.EventType.Name,
                                          start = x.StartTime.ToString(),
                                          end = x.EndTime.ToString(),
                                          color = x.EventType.EventColor
                                      })
                );
            return Json(eventsItems, JsonRequestBehavior.AllowGet);
        }
    }

    public class EventChangedViewModel:ViewModel
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? ScheduledDate { get; set; }
    }
}