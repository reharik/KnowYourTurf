using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Core.Domain
{
    public class Employee : User
    {
        [ValidateNonEmpty]
        public virtual string EmployeeId { get; set; }
        [ValueOf(typeof(EmployeeType))]
        public virtual string EmployeeType { get; set; }
        public virtual string EmergencyContact { get; set; }
        public virtual string EmergencyContactPhone { get; set; }
        #region Collections
        private readonly IList<Task> _tasks = new List<Task>();
        public virtual IEnumerable<Task> GetTasks() { return _tasks; }
        public virtual void RemoveTask(Task task) { _tasks.Remove(task); }
        public virtual void AddTask(Task task)
        {
            if (!task.IsNew() && _tasks.Contains(task)) return;
            _tasks.Add(task);
        }

        #endregion

        public virtual bool IsEmployeeAvailableForTask(Task task)
        {
            var startTime = DateTimeUtilities.StandardToMilitary(task.ScheduledStartTime.ToString());
            var endTime = DateTimeUtilities.StandardToMilitary(task.ScheduledEndTime.ToString());
            var conflictingTask = GetTasks().FirstOrDefault(x => x!=task
                                                                 &&  (x.ScheduledDate == task.ScheduledDate
                                                                      && DateTimeUtilities.StandardToMilitary(x.ScheduledStartTime.ToString()) < endTime
                                                                      && DateTimeUtilities.StandardToMilitary(x.ScheduledEndTime.ToString()) > startTime));
            return conflictingTask == null;
        }
    }
}