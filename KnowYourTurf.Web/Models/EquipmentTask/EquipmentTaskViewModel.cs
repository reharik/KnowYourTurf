using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.CustomAttributes;
using Castle.Components.Validator;

namespace KnowYourTurf.Web.Models
{
    public class EquipmentTaskViewModel:ViewModel
    {
        public IEnumerable<SelectListItem> _TaskTypeEntityIdList { get; set; }
        public IEnumerable<SelectListItem> _EquipmentEntityIdList { get; set; }
        public TokenInputViewModel Employees { get; set; }
        public TokenInputViewModel Parts { get; set; }

        
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
        public int EquipmentEntityId { get; set; }
        [TextArea]
        public string Notes { get; set; }
        public bool Complete { get; set; }

    }

    public class DisplayEquipmentTaskViewModel : ViewModel
    {
        public string TaskTypeName { get; set; }
        public string ScheduledDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string EquipmentName { get; set; }
        public string Notes { get; set; }
        public string _AddUpdateUrl { get; set; }
        public IEnumerable<string> _EmployeeNames { get; set; }
        public IEnumerable<string> _PartsNames { get; set; }
    }

    public class AddUpdateEquipmentTaskViewModel : ViewModel
    {
        public int Equipment { get; set; }
        public bool Copy { get; set; }
        public DateTime? ScheduledDate { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}