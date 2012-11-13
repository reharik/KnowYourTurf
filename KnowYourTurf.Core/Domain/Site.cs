using System.Collections.Generic;
using CC.Core;
using CC.Core.Domain;

namespace KnowYourTurf.Core.Domain
{
    public class Site:DomainEntity, IPersistableObject
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual IEnumerable<Event> GetAllEvents()
        {
            var events = new List<Event>();
            _fields.ForEachItem(x => events.InsertRange(0, x.Events));
            return events;
        }

        public virtual IEnumerable<Task> GetAllTasks()
        {
            var tasks = new List<Task>();
            _fields.ForEachItem(x => tasks.InsertRange(0, x.Tasks));
            return tasks;
        }
        
        #region Collections
        private IList<Field> _fields = new List<Field>();
        public virtual IEnumerable<Field> Fields { get { return _fields; } }
        public virtual void ClearField() { _fields = new List<Field>(); }
        public virtual void RemoveField(Field field) { _fields.Remove(field); }
        public virtual void AddField(Field field)
        {
            if (!field.IsNew() && _fields.Contains(field)) return;
            _fields.Add(field);
            field.SetSite(this);
        }
        private IList<Equipment> _equipment = new List<Equipment>();
        public virtual IEnumerable<Equipment> Equipment { get { return _equipment; } }
        public virtual void ClearEquipment() { _equipment = new List<Equipment>(); }
        public virtual void RemoveEquipment(Equipment equipment) { _equipment.Remove(equipment); }
        public virtual void AddEquipment(Equipment equipment)
        {
            if (!equipment.IsNew() && _equipment.Contains(equipment)) return;
            _equipment.Add(equipment);
            equipment.SetSite(this);
        }
        #endregion
    }
}