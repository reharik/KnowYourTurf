using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModelAndDTOs;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enumerations;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;
using Rhino.Security.Interfaces;

namespace KnowYourTurf.Web.Areas.Schedule.Controllers
{
    public class AppointmentCalendarController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISessionContext _sessionContext;
        private readonly IAuthorizationService _authorizationService;

        public AppointmentCalendarController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISessionContext sessionContext,
            IAuthorizationService authorizationService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _sessionContext = sessionContext;
            _authorizationService = authorizationService;
        }

        public ActionResult AppointmentCalendar()
        {
            var userEntityId = _sessionContext.GetUserEntityId();
            var user = _repository.Find<User>(userEntityId);

            var model = new CalendarViewModel
            {
                CalendarDefinition = new CalendarDefinition
                {
                    Url = UrlContext.GetUrlForAction<AppointmentCalendarController>(x => x.Events(null), AreaName.Schedule),
                    AddEditUrl = UrlContext.GetUrlForAction<AppointmentController>(x => x.AddUpdate(null), AreaName.Schedule),
                    DisplayUrl = UrlContext.GetUrlForAction<AppointmentController>(x => x.Display(null), AreaName.Schedule),
                    DeleteUrl = UrlContext.GetUrlForAction<AppointmentController>(x => x.Delete(null), AreaName.Schedule),
                    EventChangedUrl = UrlContext.GetUrlForAction<AppointmentCalendarController>(x => x.EventChanged(null), AreaName.Schedule),
                    CanEditPastAppointments = _authorizationService.IsAllowed(user, "/Calendar/CanEditPastAppointments"),
                    CanEnterRetroactiveAppointments = _authorizationService.IsAllowed(user, "/Calendar/CanEnterRetroactiveAppointments"),
                    CanSeeOthersAppointments = _authorizationService.IsAllowed(user, "/Calendar/CanSeeOthersAppointments"),
                    TrainerId = user.EntityId
                }
            };
            return View(model);
        }

        public JsonResult EventChanged(AppointmentChangedViewModel input)
        {
            var appointment = _repository.Find<Appointment>(input.EntityId);
            appointment.ScheduledDate = input.ScheduledDate;
            appointment.ScheduledEndTime = input.EndTime;
            appointment.ScheduledStartTime = input.StartTime;
            var crudManager = _saveEntityService.ProcessSave(appointment);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Events(GetEventsViewModel input)
        {
            var userEntityId = _sessionContext.GetUserEntityId();
            var user = _repository.Find<User>(userEntityId);
            var canSeeOthers = _authorizationService.IsAllowed(user, "/Calendar/CanSeeOtherAppointments");
            var events = new List<CalendarEvent>();
            var startDateTime = DateTimeUtilities.ConvertFromUnixTimestamp(input.start);
            var endDateTime = DateTimeUtilities.ConvertFromUnixTimestamp(input.end);
            var appointments = _repository.Query<Appointment>(x => x.ScheduledDate >= startDateTime && x.ScheduledDate <= endDateTime);
            appointments.Each(x => GetValue(x, events, user, canSeeOthers) );
            return Json(events, JsonRequestBehavior.AllowGet);
        }

        private void GetValue(Appointment x, List<CalendarEvent> events, User user, bool canSeeOthers)
        {
            var calendarEvent = new CalendarEvent
                                    {
                                        EntityId = x.EntityId,
                                        title = x.Location.Name,
                                        start = x.ScheduledStartTime.ToString(),
                                        end = x.ScheduledEndTime.ToString(),
                                        color = x.Trainer.Color,
                                        trainerId = x.Trainer.EntityId
                                    };

            if (x.Trainer != user && !canSeeOthers)
            {
                calendarEvent.color = "#fffff";
                calendarEvent.title = string.Empty;
                calendarEvent.className = "hiddenEvent";
            }
            events.Add(calendarEvent);
        }
    }

    public class AppointmentPermissionsDto
    {
        public bool CanSeeOthers { get; set; }
        public bool CanEditOthers { get; set; }
        public bool CanEnterRetroactive { get; set; }
        public bool CanEditRetroactive { get; set; }
    }

    public class AppointmentChangedViewModel : ViewModel
    {
        public DateTime? ScheduledDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }



    }
}