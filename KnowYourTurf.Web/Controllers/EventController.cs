using System;
using System.Linq;
using System.Web.Mvc;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Models;
using AutoMapper;
using NHibernate.Linq;


namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class EventController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISelectListItemService _selectListItemService;

        public EventController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _selectListItemService = selectListItemService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("EventAddUpdate", new EventViewModel());
        }

        public ActionResult AddUpdate(EventViewModel input)
        {
            // need Site to get fields for dropdown;
            var category = _repository.Find<Site>(input.RootId);
            var _event = input.EntityId > 0 ? category.GetAllEvents().FirstOrDefault(x=>x.EntityId == input.EntityId) : new Event();
            _event.ScheduledDate = input.ScheduledDate.IsNotEmpty() ? DateTime.Parse(input.ScheduledDate) : _event.ScheduledDate.Value.Date;
            _event.StartTime = input.StartTime.IsNotEmpty() ? DateTime.Parse(input.StartTime) : _event.StartTime;
            var fields = _selectListItemService.CreateList(category.Fields,x => x.Name, x => x.EntityId, true);
            var _eventTypes = _selectListItemService.CreateList<EventType>(x => x.Name, x => x.EntityId, true);
            var model = Mapper.Map<Event,EventViewModel>(_event);
                            
            model.RootId = input.RootId;
            model._FieldEntityIdList = fields;
            model._EventTypeEntityIdList = _eventTypes;
            model._Title = WebLocalizationKeys.EVENT_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<EventController>(x => x.Save(null));
            model.StartTime = _event.StartTime.HasValue ? _event.StartTime.Value.ToShortTimeString() : "";
            model.EndTime = _event.EndTime.HasValue ? _event.EndTime.Value.ToShortTimeString() : "";
            model.ScheduledDate = _event.ScheduledDate.Value.ToShortDateString();
            return new CustomJsonResult(model);
        }

        public ActionResult Display_Template(ViewModel input)
        {
            return View("EventDisplay", new EventViewModel());
        }
        public ActionResult Display(ViewModel input)
        {
            var field = _repository.Query<Field>(x=>x.Events.Any(e=>e.EntityId == input.EntityId)).FirstOrDefault();
            var _event = field.Events.FirstOrDefault(x => x.EntityId == input.EntityId);
            var model = Mapper.Map<Event, EventViewModel>(_event);
            model.StartTime = _event.StartTime.Value.ToShortTimeString();
            model.EndTime = _event.EndTime.HasValue ? _event.EndTime.Value.ToShortTimeString() : "";
            model.ScheduledDate = _event.ScheduledDate.HasValue ? _event.ScheduledDate.Value.ToShortDateString() : "";
            model._Title = WebLocalizationKeys.EVENT_INFORMATION.ToString();
            return new CustomJsonResult(model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var category = _repository.Query<Site>(x => x.EntityId == input.RootId)
                .Fetch(x => x.Fields.FirstOrDefault(y => y.Events.Any(z => z.EntityId == input.EntityId))).FirstOrDefault();

            var field = category.Fields.FirstOrDefault(y => y.Events.Any(z => z.EntityId == input.EntityId));
            var _event =field.Events.FirstOrDefault(x => x.EntityId == input.EntityId);
            field.RemoveEvent(_event);
            var crudManager = _saveEntityService.ProcessSave(field);
            var notification = crudManager.Finish();
            return new CustomJsonResult(notification);
        }

        public ActionResult Save(EventViewModel input)
        {
            var category = _repository.Find<Site>(input.RootId);
            var _event = input.EntityId > 0 ? category.GetAllEvents().FirstOrDefault(x => x.EntityId == input.EntityId) : new Event();
            var field = mapToDomain(input, _event);
            var crudManager = _saveEntityService.ProcessSave(field);
            var notification = crudManager.Finish();
            return new CustomJsonResult(notification);
        }

        // getting the repo version of _event in action so I can tell if the _event was completed in past
        // maybe not so cool.  don't know
        private Field mapToDomain(EventViewModel input, Event _event)
        {
            var eventType = _repository.Find<EventType>(input.EventTypeEntityId);
            _event.ScheduledDate = DateTime.Parse(input.ScheduledDate);
            _event.StartTime = null;
            _event.StartTime = DateTime.Parse(input.ScheduledDate + " " + input.StartTime);
            _event.EndTime = null;
            if (!string.IsNullOrEmpty(input.EndTime))
            {
                _event.EndTime = DateTime.Parse(input.ScheduledDate + " " + input.EndTime);
            }
            if (_event.Field ==null || _event.Field.EntityId != input.FieldEntityId)
            {
                var field = _repository.Find<Field>(input.FieldEntityId);
                field.AddEvent(_event);
            }
            _event.EventType = eventType;
            _event.Notes = input.Notes;
            return _event.Field;
        }
    }

   
}