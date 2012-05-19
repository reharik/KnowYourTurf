using System.Collections.Generic;

namespace KnowYourTurf.Core.Domain
{
    public class Company : DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual double TaxRate { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual int NumberOfCategories { get; set; }
        #region Collections
        private IList<Category> _categories = new List<Category>();
        public virtual IEnumerable<Category> Categories { get { return _categories; } }
        public virtual void ClearCategory() { _categories = new List<Category>(); }
        public virtual void RemoveCategory(Category category) { _categories.Remove(category); }
        public virtual void AddCategory(Category category)
        {
            if (!category.IsNew() && _categories.Contains(category)) return;
            _categories.Add(category);
        }
        #endregion
    }

    public class Category:DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        #region Collections
        private IList<Field> _fields = new List<Field>();
        public virtual IEnumerable<Field> Fields { get { return _fields; } }
        public virtual void ClearField() { _fields = new List<Field>(); }
        public virtual void RemoveField(Field field) { _fields.Remove(field); }
        public virtual void AddField(Field field)
        {
            if (!field.IsNew() && _fields.Contains(field)) return;
            _fields.Add(field);
        }
        private IList<Task> _tasks = new List<Task>();
        public virtual IEnumerable<Task> Tasks { get { return _tasks; } }
        public virtual void ClearTask() { _tasks = new List<Task>(); }
        public virtual void RemoveTask(Task task) { _tasks.Remove(task); }
        public virtual void AddTask(Task task)
        {
            if (!task.IsNew() && _tasks.Contains(task)) return;
            _tasks.Add(task);
        }
        private IList<Event> _events = new List<Event>();
        public virtual IEnumerable<Event> Events { get { return _events; } }
        public virtual void ClearEvent() { _events = new List<Event>(); }
        public virtual void RemoveEvent(Event _event) { _events.Remove(_event); }
        public virtual void AddEvent(Event _event)
        {
            if (!_event.IsNew() && _events.Contains(_event)) return;
            _events.Add(_event);
        }
        #endregion
    }
}