using System;
using System.Collections.Generic;
using System.Linq;
using CC.Core.Domain;
using CC.Core.Localization;
using Castle.Components.Validator;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Core.Domain
{
    public class Field : DomainEntity, IPersistableObject
    {
        public virtual Category Category { get; private set; }

        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        [ValidateNonEmpty]
        public virtual string Description { get; set; }
        public virtual string Abbreviation { get; set; }
        [ValidateNonEmpty, ValidateIntegerAttribute]
        public virtual int Size { get; set; }
        public virtual string FileUrl { get; set; }
        [ValueOf(typeof(Status))]
        public virtual string Status { get; set; }
        public virtual string FieldColor { get; set; }

        #region Collections
        private readonly IList<Task> _tasks = new List<Task>();
        public virtual IEnumerable<Task> Tasks { get { return _tasks; } }
        public virtual IEnumerable<Task> GetPendingTasks()
        {
            return _tasks.Where(x => !x.Complete && x.ScheduledStartTime >= DateTime.Now);
        }
        public virtual void RemovePendingTask(Task task) { _tasks.Remove(task); }
        public virtual void AddPendingTask(Task task)
        {
            if (!task.IsNew() && _tasks.Contains(task)) return;
            _tasks.Add(task);
        }
        public virtual IEnumerable<Task> GetCompletedTasks() { return _tasks.Where(x => x.Complete); }
        public virtual void RemoveCompletedTask(Task task) { _tasks.Remove(task); }
        public virtual void AddCompletedTask(Task task)
        {
            if (!task.IsNew() && _tasks.Contains(task)) return;
            _tasks.Add(task);
            task.Field = this;
        }

        private readonly IList<Event> _events = new List<Event>();
        public virtual IEnumerable<Event> Events { get { return _events; } }
        public virtual void RemoveEvent(Event fieldEvent) { _events.Remove(fieldEvent); }
        public virtual void AddEvent(Event fieldEvent)
        {
            if (!fieldEvent.IsNew() && _events.Contains(fieldEvent)) return;
            _events.Add(fieldEvent);
            fieldEvent.Field = this;
        }
        
        private readonly IList<Document> _documents = new List<Document>();
        public virtual IEnumerable<Document> Documents { get { return _documents; } }
        public virtual void RemoveDocument(Document fieldDocument) { _documents.Remove(fieldDocument); }
        public virtual void AddDocument(Document fieldDocument)
        {
            if (!fieldDocument.IsNew() && _documents.Contains(fieldDocument)) return;
            _documents.Add(fieldDocument);
        }

        private readonly IList<Photo> _photos = new List<Photo>();
        public virtual IEnumerable<Photo> Photos { get { return _photos; } }
        public virtual void RemovePhoto(Photo fieldPhoto) { _photos.Remove(fieldPhoto); }
        public virtual void AddPhoto(Photo fieldPhoto)
        {
            if (!fieldPhoto.IsNew() && _photos.Contains(fieldPhoto)) return;
            _photos.Add(fieldPhoto);
        }
        #endregion

        public virtual void SetCategory(Category category)
        {
            Category = category;
        }
    }

    
}