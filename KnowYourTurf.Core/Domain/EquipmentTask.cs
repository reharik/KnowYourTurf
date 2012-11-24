using System;
using System.Collections.Generic;
using System.Linq;
using CC.Core;
using CC.Core.Domain;
using CC.Core.Localization;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using KnowYourTurf.Core.Enums;

namespace KnowYourTurf.Core.Domain
{
    public class EquipmentTask : DomainEntity, IPersistableObject
    {
        public virtual Equipment Equipment { get; set; }
        [ValidateNonEmpty]
        public virtual EquipmentTaskType TaskType { get; set; }
        [ValidateNonEmpty]
        public virtual DateTime? ScheduledDate { get; set; }
        [ValidateNonEmpty]
        public virtual DateTime? StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
        public virtual string ActualTimeSpent { get; set; }
        [TextArea]
        public virtual string Notes { get; set; }
        [ValueOf(typeof(UnitType))]

        public virtual bool Deleted { get; set; }
        public virtual bool Complete { get; set; }

        #region Collections
        public virtual void ClearPart() { _parts = new List<Part>(); }
        private IList<Part> _parts = new List<Part>();
        public virtual IEnumerable<Part> Parts { get { return _parts; } }
        public virtual void RemovePart(Part part) { _parts.Remove(part); }
        public virtual void AddPart(Part part)
        {
            if (!part.IsNew() && _parts.Contains(part)) return;
            _parts.Add(part);
        }

        public virtual void ClearEmployees() { _employees = new List<User>(); }
        private IList<User> _employees = new List<User>();
        public virtual IEnumerable<User> Employees { get { return _employees; } }

        public virtual void RemoveEmployee(User employee) { _employees.Remove(employee); }
        public virtual void AddEmployee(User employee)
        {
            if (!employee.IsNew() && _employees.Contains(employee)) return;
            _employees.Add(employee);
        }
        #endregion

        public virtual EquipmentTask CloneTask()
        {
            var newTask = new EquipmentTask
                              {
                                  TaskType = TaskType,
                                  Notes = Notes,
                                  ScheduledDate = ScheduledDate,
                                  Equipment = Equipment
                              };
            Employees.ForEachItem(newTask.AddEmployee);
            Parts.ForEachItem(newTask.AddPart);
            return newTask;
        }

        public virtual bool IsAssignedToEmployee(int employeeId)
        {
            var employee = Employees.FirstOrDefault(x=>x.EntityId == employeeId);
            return employee != null;
        }
        
        
    }
}