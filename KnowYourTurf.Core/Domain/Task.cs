using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Core.Domain
{
    public class Task : DomainEntity
    {
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
        [ValidateNonEmpty]
        public virtual Field Field { get; set; }
        //this should be on the domain entity I just can't figure out how
        public virtual User CreatedBy { get; set; }

        public virtual InventoryProduct InventoryProduct { get; set; }
        public virtual bool InventoryDecremented { get; set; }

        #region Collections
        public virtual void ClearEquipment() { _equipment = new List<Equipment>(); }
        private IList<Equipment> _equipment = new List<Equipment>();
        public virtual IEnumerable<Equipment> GetEquipment() { return _equipment; }
        public virtual void RemoveEquipment(Equipment equipment) { _equipment.Remove(equipment); }
        public virtual void AddEquipment(Equipment equipment)
        {
            if (!equipment.IsNew() && _equipment.Contains(equipment)) return;
            _equipment.Add(equipment);
        }

        public virtual void ClearEmployees() { _employees = new List<Employee>(); }
        private IList<Employee> _employees = new List<Employee>();
        public virtual IEnumerable<Employee> GetEmployees() { return _employees; }
        public virtual void RemoveEmployee(Employee employee) { _employees.Remove(employee); }
        public virtual void AddEmployee(Employee employee)
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
                Field = Field,
                TaskType = TaskType,
                Notes = Notes,
                ScheduledDate = ScheduledDate,
                ScheduledEndTime = ScheduledEndTime,
                ScheduledStartTime = ScheduledStartTime,
                InventoryProduct = InventoryProduct
            };
            GetEmployees().Each(newTask.AddEmployee);
            return newTask;
        }

        public virtual string GetProductName()
        {
            return InventoryProduct != null ? InventoryProduct.Product.Name : "";
        }
        
        public virtual string GetProductIdAndName()
        {
            return InventoryProduct != null ? InventoryProduct.EntityId + "_" + InventoryProduct.Product.InstantiatingType + "s" : "";
        }

        public virtual bool IsAssignedToEmployee(long employeeId)
        {
            var employee = GetEmployees().FirstOrDefault(x=>x.EntityId == employeeId);
            return employee != null;
        }
    }


}