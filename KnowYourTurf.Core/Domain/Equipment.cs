using System;
using System.Collections.Generic;
using System.Linq;
using CC.Core.CustomAttributes;
using CC.Core.Domain;
using Castle.Components.Validator;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Core.Domain
{
    public class Equipment : DomainEntity, IPersistableObject
    {
        public virtual Site Site { get; private set; }

        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        [TextArea]
        public virtual string Description { get; set; }
        public virtual string Make { get; set; }
        public virtual string Model { get; set; }
        public virtual string SerialNumber { get; set; }
        [TextArea]
        public virtual string WarrentyInfo { get; set; }
        [ValidateNonEmpty]
        [ValidateDecimal]
        public virtual double TotalHours { get; set; }
        
        /// <summary>
        /// Aggregate Root that should not be modified through Equipment
        /// must have set on readonly field right now for model binder.
        /// </summary>
        private FieldVendor mFieldVendor;
        public virtual FieldVendor FieldVendor { get { return mFieldVendor; } set { mFieldVendor = value; } }
        public virtual void SetVendor(FieldVendor fieldVendor)
        {
            mFieldVendor = fieldVendor;
        }

        #region Collections
        private readonly IList<EquipmentTask> _tasks = new List<EquipmentTask>();
        public virtual IEnumerable<EquipmentTask> Tasks { get { return _tasks; } }
        public virtual IEnumerable<EquipmentTask> GetPendingTasks()
        {
            return _tasks.Where(x => !x.Complete && x.ScheduledDate.Value.Date >= DateTime.Now.Date);
        }
        public virtual void RemovePendingTask(EquipmentTask task) { _tasks.Remove(task); }
        public virtual void AddPendingTask(EquipmentTask task)
        {
            if (!task.IsNew() && _tasks.Contains(task)) return;
            _tasks.Add(task);
        }
        public virtual IEnumerable<EquipmentTask> GetCompletedTasks() { return _tasks.Where(x => x.Complete); }
        public virtual void RemoveCompletedTask(EquipmentTask task) { _tasks.Remove(task); }
        public virtual void AddCompletedTask(EquipmentTask task)
        {
            if (!task.IsNew() && _tasks.Contains(task)) return;
            _tasks.Add(task);
            task.Equipment = this;
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

        public virtual void SetSite(Site site)
        {
            Site = site;
        }
    }
}