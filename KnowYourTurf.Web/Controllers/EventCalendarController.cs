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
                           CalendarDefinition = new CalendarDefinition
                                                   {
                                                       Url = UrlContext.GetUrlForAction<EventCalendarController>(x => x.Events(null)) + "?RootId=" + input.RootId,
                                                       AddUpdateTemplateUrl = UrlContext.GetUrlForAction<EventController>(x => x.AddUpdate_Template(null)),
                                                       AddUpdateUrl = UrlContext.GetUrlForAction<EventController>(x => x.AddUpdate(null)),
                                                       AddUpdateRoute = "appointment",
                                                       DisplayTemplateUrl = UrlContext.GetUrlForAction<EventController>(x => x.Display_Template(null)),
                                                       DisplayUrl = UrlContext.GetUrlForAction<EventController>(x => x.Display(null)),
                                                       DisplayRoute = "appointmentdisplay",
                                                       DeleteUrl = UrlContext.GetUrlForAction<EventController>(x => x.Delete(null)),
                                                       EventChangedUrl = UrlContext.GetUrlForAction<EventCalendarController>(x => x.EventChanged(null))
                                               
                                                   }
                       };
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public JsonResult EventChanged(EventChangedViewModel input)
        {
            var field = _repository.Query<Field>(x => x.Tasks.Any(y => y.EntityId == input.EntityId)).FirstOrDefault();
            var _event = field.Events.FirstOrDefault(x => x.EntityId == input.EntityId);
            _event.ScheduledDate = input.ScheduledDate;
            _event.StartTime = input.StartTime;
            _event.EndTime = input.EndTime;
            var crudManager = _saveEntityService.ProcessSave(field);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Events(GetEventsViewModel input)
        {
            var eventsItems = new List<CalendarEvent>();
            var startDateTime = DateTimeUtilities.ConvertFromUnixTimestamp(input.start);
            var endDateTime = DateTimeUtilities.ConvertFromUnixTimestamp(input.end);
            var category = _repository.Find<Category>(input.RootId);
            var events = category.GetAllEvents().Where(x => x.ScheduledDate >= startDateTime && x.ScheduledDate <= endDateTime);
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