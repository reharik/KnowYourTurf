using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Models
{
    public class TaskViewModel:ViewModel
    {
        public IEnumerable<SelectListItem> FieldList { get; set; }
        public IEnumerable<SelectListItem> EquipmentList { get; set; }
        public IEnumerable<SelectListItem> TaskTypeList { get; set; }
        public string Product { get; set; }
        public IDictionary<string,IEnumerable<SelectListItem>> ProductList { get; set; }
        public Task Item { get; set; }
        public bool Copy { get; set; }

        public IEnumerable<TokenInputDto> AvailableEmployees { get; set; }
        public List<TokenInputDto> SelectedEmployees { get; set; }
        public IEnumerable<TokenInputDto> AvailableEquipment { get; set; }
        public IEnumerable<TokenInputDto> SelectedEquipment { get; set; }

        public string EmployeeInput { get; set; }
        public string EquipmentInput { get; set; }

        public IEnumerable<string> EmployeeNames { get; set; }
        public IEnumerable<string> EquipmentNames { get; set; }
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