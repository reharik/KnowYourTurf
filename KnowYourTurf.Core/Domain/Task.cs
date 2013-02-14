using System;
using System.Collections.Generic;
using System.Linq;
using CC.Core;
using CC.Core.CustomAttributes;
using CC.Core.Domain;
using CC.Core.Localization;
using Castle.Components.Validator;
using KnowYourTurf.Core.Enums;

namespace KnowYourTurf.Core.Domain
{
    public class Task : DomainEntity, IPersistableObject
    {
        public virtual Field Field { get; set; }
        [ValidateNonEmpty]
        public virtual TaskType TaskType { get; set; }
        [ValidateNonEmpty]
        public virtual DateTime? ScheduledDate { get; set; }
        [ValidateNonEmpty]
        public virtual DateTime? StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
        public virtual string ActualTimeSpent { get; set; }
        [ValidateDouble]
        public virtual double? QuantityNeeded { get; set; }
        [ValidateDouble]
        public virtual double? QuantityUsed { get; set; }
        [TextArea]
        public virtual string Notes { get; set; }
        [ValueOf(typeof(UnitType))]
        public virtual string UnitType { get; set; }

        public virtual bool Deleted { get; set; }
        public virtual bool Complete { get; set; }

        public virtual InventoryProduct InventoryProduct { get; set; }
        public virtual bool InventoryDecremented { get; set; }

        #region Collections
        public virtual void ClearEquipment() { _equipment = new List<Equipment>(); }
        private IList<Equipment> _equipment = new List<Equipment>();
        public virtual IEnumerable<Equipment> Equipment { get { return _equipment; } }
        public virtual void RemoveEquipment(Equipment equipment) { _equipment.Remove(equipment); }
        public virtual void AddEquipment(Equipment equipment)
        {
            if (!equipment.IsNew() && _equipment.Contains(equipment)) return;
            _equipment.Add(equipment);
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

        public virtual Task CloneTask()
        {
            var newTask = new Task
                              {
                                  TaskType = TaskType,
                                  Notes = Notes,
                                  ScheduledDate = ScheduledDate,
                                  EndTime = EndTime,
                                  StartTime = StartTime,
                                  InventoryProduct = InventoryProduct,
                                  Field = Field
                              };
            Employees.ForEachItem(newTask.AddEmployee);
            return newTask;
        }

        // these violate law of demiter.  fix with methods on aggroots
        public virtual string GetProductName()
        {
            return InventoryProduct != null ? InventoryProduct.Product.Name : "";
        }
        
        public virtual string GetProductIdAndName()
        {
            return InventoryProduct != null ? InventoryProduct.EntityId + "_" + InventoryProduct.Product.InstantiatingType + "s" : "";
        }

        public virtual bool IsAssignedToEmployee(int employeeId)
        {
            var employee = Employees.FirstOrDefault(x=>x.EntityId == employeeId);
            return employee != null;
        }
        
        
    }


}