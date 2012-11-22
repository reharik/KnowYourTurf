using System;
using System.Collections.Generic;
using System.Linq;
using CC.Core.CustomAttributes;
using CC.Core.Domain;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain.Persistence;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Core.Domain
{
    public class Equipment : DomainEntity, IPersistableObject
    {
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
        public virtual double Threshold { get; set; }
        [ValidateNonEmpty]
        public virtual EquipmentType EquipmentType { get; set; }
        
        /// <summary>
        /// Aggregate Root that should not be modified through Equipment
        /// must have set on readonly field right now for model binder.
        /// </summary>
        private EquipmentVendor _equipmentVendor;
        public virtual EquipmentVendor EquipmentVendor { get { return _equipmentVendor; } set { _equipmentVendor = value; } }
        public virtual void SetVendor(EquipmentVendor equipmentVendor)
        {
            _equipmentVendor = equipmentVendor;
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
        public virtual void RemoveDocument(Document document) { _documents.Remove(document); }
        public virtual void AddDocument(Document document)
        {
            if (!document.IsNew() && _documents.Contains(document)) return;
            _documents.Add(document);
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

        public virtual IEnumerable<EquipmentTask> GetAllEquipmentTasks(Func<EquipmentTask,bool> _where = null )
        {
            return _where != null ? Tasks.Where(_where) : Tasks;
        }
    }
}