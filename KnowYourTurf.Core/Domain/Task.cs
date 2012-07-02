using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using FubuMVC.Core;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Core.Domain
{
    public class Task : DomainEntity, IPersistableObject
    {

        /// <summary>
        /// Aggregate Root that should not be modified through Task
        /// must have set on readonly field right now for model binder.
        /// </summary>
        private Field _readOnlyField;
        public virtual Field ReadOnlyField { get { return _readOnlyField; } set { _readOnlyField = value; } }
        public virtual void SetField(Field field)
        {
            _readOnlyField = field;
        }
        ////
        [ValidateNonEmpty]
        public virtual TaskType TaskType { get; set; }
        [ValidateNonEmpty]
        public virtual DateTime? ScheduledDate { get; set; }
        [ValidateNonEmpty]
        public virtual DateTime? ScheduledStartTime { get; set; }
        public virtual DateTime? ScheduledEndTime { get; set; }
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

        /// <summary>
        /// Aggregate Root that should not be modified through Task
        /// </summary>
        private InventoryProduct _readOnlyInventoryProduct;
        public virtual InventoryProduct ReadOnlyInventoryProduct { get { return _readOnlyInventoryProduct; } set { _readOnlyInventoryProduct = value; } }
        public virtual void SetInventoryProduct(InventoryProduct inventoryProduct)
        {
            _readOnlyInventoryProduct = inventoryProduct;
        }
        ////
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
                CreatedBy = CreatedBy,
                TaskType = TaskType,
                Notes = Notes,
                ScheduledDate = ScheduledDate,
                ScheduledEndTime = ScheduledEndTime,
                ScheduledStartTime = ScheduledStartTime
            };
            newTask.SetInventoryProduct(ReadOnlyInventoryProduct);
            newTask.SetField(_readOnlyField);
            Employees.ForEachItem(newTask.AddEmployee);
            return newTask;
        }

        // these violate law of demiter.  fix with methods on aggroots
        public virtual string GetProductName()
        {
            return ReadOnlyInventoryProduct != null ? ReadOnlyInventoryProduct.ReadOnlyProduct.Name : "";
        }
        
        public virtual string GetProductIdAndName()
        {
            return ReadOnlyInventoryProduct != null ? ReadOnlyInventoryProduct.EntityId + "_" + ReadOnlyInventoryProduct.ReadOnlyProduct.InstantiatingType + "s" : "";
        }

        public virtual bool IsAssignedToEmployee(long employeeId)
        {
            var employee = Employees.FirstOrDefault(x=>x.EntityId == employeeId);
            return employee != null;
        }
        
        
    }


}