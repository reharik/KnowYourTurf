using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;

namespace KnowYourTurf.Web.Models
{
    public class TaskViewModel:ViewModel
    {
        public GroupedSelectViewModel _FieldEntityIdList { get; set; }
        public GroupedSelectViewModel _InventoryProductEntityIdList { get; set; }
        public IEnumerable<SelectListItem> _TaskTypeEntityIdList { get; set; }

        public TokenInputViewModel Employees { get; set; }
        public TokenInputViewModel Equipment { get; set; }

        
        public string _saveUrl { get; set; }
        public bool Copy { get; set; }

        [ValidateNonEmpty]
        public int TaskTypeEntityId { get; set; }
        [ValidateNonEmpty]
        public string ScheduledDate { get; set; }
        [ValidateNonEmpty]
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ActualTimeSpent { get; set; }
        [ValidateNonEmpty]
        public int FieldEntityId { get; set; }
        public int InventoryProductEntityId { get; set; }
        [ValidateDouble]
        public double? QuantityNeeded { get; set; }
        [ValidateDouble]
        public double? QuantityUsed { get; set; }
        [TextArea]
        public string Notes { get; set; }
        public bool Complete { get; set; }

    }

    public class DisplayTaskViewModel:ViewModel
    {
        public string TaskTypeName { get; set; }
        public string ScheduledDate { get; set; }
        public string FieldName { get; set; }
        public string InventoryProductProductName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Notes { get; set; }
        public string _AddUpdateUrl { get; set; }
        public IEnumerable<string> _EmployeeNames { get; set; }
        public IEnumerable<string> _EquipmentNames { get; set; }
    }

    public class AddUpdateTaskViewModel:ViewModel
    {
        public string Param1 { get; set; }
        public int Field { get; set; }
        public string Product { get; set; }
        public double Quantity { get; set; }
        public bool Copy { get; set; }
        public string ScheduledDate { get; set; }
        public string ScheduledStartTime { get; set; }
    }
}