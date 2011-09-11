using System;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
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

        public ActionResult AddEdit(AddEditEventViewModel input)
        {
            var _event = input.EntityId > 0 ? _repository.Find<Event>(input.EntityId) : new Event();
            _event.ScheduledDate = input.ScheduledDate.HasValue ? input.ScheduledDate.Value : _event.ScheduledDate;
            _event.StartTime = input.StartTime.HasValue ? input.StartTime.Value : _event.StartTime;
            var fields = _selectListItemService.CreateList<Field>(x => x.Name, x => x.EntityId, true,true);
            var _eventTypes = _selectListItemService.CreateList<EventType>(x => x.Name, x => x.EntityId, true);
            var model = new EventViewModel
                            {
                                FieldList = fields,
                                EventTypeList = _eventTypes,
                                Event = _event,
                                Copy = input.Copy,
            };
            if (_event.EntityId > 0)
            {
                model.Field = _event.Field.EntityId;
                model.EventType = _event.EventType.EntityId;
            }
            return PartialView("EventAddUpdate", model);
        }

        public ActionResult Display(ViewModel input)
        {
            var _event = _repository.Find<Event>(input.EntityId);
            var model = new EventViewModel
                            {
                                Event = _event,
                                AddEditUrl = UrlContext.GetUrlForAction<EventController>(x => x.AddEdit(null)) + "/" + _event.EntityId
            };
            return PartialView("EventView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            //TODO needs rule engine
            var _event = _repository.Find<Event>(input.EntityId);
            _repository.HardDelete(_event);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(EventViewModel input)
        {
            var _event = input.Event.EntityId > 0? _repository.Find<Event>(input.Event.EntityId):new Event();
            mapToDomain(input, _event);
            var crudManager = _saveEntityService.ProcessSave(_event);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        // getting the repo version of _event in action so I can tell if the _event was completed in past
        // maybe not so cool.  don't know
        private Event mapToDomain(EventViewModel model, Event _event)
        {
            var _eventModel = model.Event;
            var field = _repository.Find<Field>(model.Field);
            var eventType = _repository.Find<EventType>(model.EventType);
            _event.ScheduledDate = _eventModel.ScheduledDate;
            _event.StartTime = DateTime.Parse(_eventModel.ScheduledDate.Value.ToShortDateString() + " " + _eventModel.StartTime.Value.ToShortTimeString());
            if(_eventModel.EndTime.HasValue)
            {
                _event.EndTime = DateTime.Parse(_eventModel.ScheduledDate.Value.ToShortDateString() + " " + _eventModel.EndTime.Value.ToShortTimeString());
            }

            _event.EventType = eventType;
            _event.Field = field;
            _event.Notes = _eventModel.Notes;
            return _event;
        }
    }

   
}