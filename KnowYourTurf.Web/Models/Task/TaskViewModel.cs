using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;

namespace KnowYourTurf.Web.Models
{
    public class TaskViewModel:ViewModel
    {
        public GroupedSelectViewModel _FieldEntityIdList { get; set; }
        public GroupedSelectViewModel _InventoryProductProductEntityIdList { get; set; }
        public IEnumerable<SelectListItem> _TaskTypeEntityIdList { get; set; }

        public TokenInputViewModel Employees { get; set; }
        public TokenInputViewModel Equipment { get; set; }

        
        public string _saveUrl { get; set; }
        public bool Copy { get; set; }

        [ValidateNonEmpty]
        public int TaskTypeEntityId { get; set; }
        [ValidateNonEmpty]
        public DateTime? ScheduledDate { get; set; }
        [ValidateNonEmpty]
        public DateTime? ScheduledStartTime { get; set; }
        public DateTime? ScheduledEndTime { get; set; }
        public string ActualTimeSpent { get; set; }
        public int FieldEntityId { get; set; }
        public int InventoryProductProductEntityId { get; set; }
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
        public DateTime ScheduledDate { get; set; }
        public string FieldName { get; set; }
        public string InventoryProductProductName { get; set; }
        public string Notes { get; set; }
        public string _AddUpdateUrl { get; set; }
        public IEnumerable<string> _EmployeeNames { get; set; }
        public IEnumerable<string> _EquipmentNames { get; set; }
    }

    public class AddUpdateTaskViewModel:ViewModel
    {
        public string Param1 { get; set; }
        public long Field { get; set; }
        public string Product { get; set; }
        public double Quantity { get; set; }
        public bool Copy { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? ScheduledStartTime { get; set; }
    }
}