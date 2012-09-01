using System.Collections.Generic;

namespace KnowYourTurf.Core.Domain
{
    public class Category:DomainEntity, IPersistableObject
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
            field.SetCategory(this);
        }
        #endregion
    }
}