using System;
using System.Linq;
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

        public ActionResult AddUpdate(AddUpdateEventViewModel input)
        {
            var category = _repository.Find<Category>(input.RootId);
            var _event = input.EntityId > 0 ? category.Events.FirstOrDefault(x=>x.EntityId == input.EntityId) : new Event();
            _event.ScheduledDate = input.ScheduledDate.HasValue ? input.ScheduledDate.Value : _event.ScheduledDate;
            _event.StartTime = input.StartTime.HasValue ? input.StartTime.Value : _event.StartTime;
            var fields = _selectListItemService.CreateList(category.Fields,x => x.Name, x => x.EntityId, true);
            var _eventTypes = _selectListItemService.CreateList<EventType>(x => x.Name, x => x.EntityId, true);
            var model = new EventViewModel
                            {
                                RootId = input.RootId,
                                FieldList = fields,
                                EventTypeList = _eventTypes,
                                Event = _event,
                                Copy = input.Copy,
                                Title = WebLocalizationKeys.EVENT_INFORMATION.ToString()
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
                                AddUpdateUrl = UrlContext.GetUrlForAction<EventController>(x => x.AddUpdate(null)) + "/" + _event.EntityId,
                                Title = WebLocalizationKeys.EVENT_INFORMATION.ToString()
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
            var category = _repository.Find<Category>(input.RootId);
            Event _event;
            if (input.Event.EntityId > 0) { _event = category.Events.FirstOrDefault(x => x.EntityId == input.Event.EntityId); }
            else
            {
                _event = new Event();
                category.AddEvent(_event);
            }

            mapToDomain(input, _event);
            var crudManager = _saveEntityService.ProcessSave(category);
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