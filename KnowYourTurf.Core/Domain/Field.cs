using System.Collections.Generic;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Core.Domain
{
    public class Field : DomainEntity
    {
        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        [ValidateNonEmpty]
        public virtual string Description { get; set; }
        public virtual string Abbreviation { get; set; }
        [ValidateNonEmpty, ValidateIntegerAttribute]
        public virtual int Size { get; set; }
        public virtual string ImageUrl { get; set; }
        [ValueOf(typeof(Status))]
        public virtual string Status { get; set; }
        #region Collections
        private readonly IList<Task> _pendingTasks = new List<Task>();
        public virtual IEnumerable<Task> GetPendingTasks() { return _pendingTasks; }
        public virtual void RemovePendingTask(Task task) { _pendingTasks.Remove(task); }
        public virtual void AddPendingTask(Task task)
        {
            if (!task.IsNew() && _pendingTasks.Contains(task)) return;
            _pendingTasks.Add(task);
        }
        private readonly IList<Task> _completedTasks = new List<Task>();
        public virtual IEnumerable<Task> GetCompletedTasks() { return _completedTasks; }
        public virtual void RemoveCompletedTask(Task task) { _completedTasks.Remove(task); }
        public virtual void AddCompletedTask(Task task)
        {
            if (!task.IsNew() && _completedTasks.Contains(task)) return;
            _completedTasks.Add(task);
        }

        private readonly IList<Event> _events = new List<Event>();
        public virtual IEnumerable<Event> GetEvents() { return _events; }
        public virtual void RemoveEvent(Event fieldEvent) { _events.Remove(fieldEvent); }
        public virtual void AddEvent(Event fieldEvent)
        {
            if (!fieldEvent.IsNew() && _events.Contains(fieldEvent)) return;
            _events.Add(fieldEvent);
        }
        
        private readonly IList<Document> _documents = new List<Document>();
        public virtual IEnumerable<Document> GetDocuments() { return _documents; }
        public virtual void RemoveDocument(Document fieldDocument) { _documents.Remove(fieldDocument); }
        public virtual void AddDocument(Document fieldDocument)
        {
            if (!fieldDocument.IsNew() && _documents.Contains(fieldDocument)) return;
            _documents.Add(fieldDocument);
        }

        private readonly IList<Photo> _photos = new List<Photo>();
        public virtual IEnumerable<Photo> GetPhotos() { return _photos; }
        public virtual void RemovePhoto(Photo fieldPhoto) { _photos.Remove(fieldPhoto); }
        public virtual void AddPhoto(Photo fieldPhoto)
        {
            if (!fieldPhoto.IsNew() && _photos.Contains(fieldPhoto)) return;
            _photos.Add(fieldPhoto);
        }
        #endregion

    }

    
}