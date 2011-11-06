using System.Collections.Generic;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Models
{
    public class EmployeeViewModel:ViewModel
    {
        public bool DeleteImage { get; set; }
        public Employee Employee { get; set; }
        public IEnumerable<TokenInputDto> AvailableItems { get; set; }
        public IEnumerable<TokenInputDto> SelectedItems { get; set; }
        public string RolesInput { get; set; }
    }
    public class AdminViewModel : ViewModel
    {
        public bool DeleteImage { get; set; }
        public Administrator Administrator { get; set; }
        public SelectBoxPickerDto UserRoleSelectBoxPickerDto { get; set; }
    }
    public class FacilitiesViewModel : ViewModel
    {
        public bool DeleteImage { get; set; }
        public Facilities Facilities { get; set; }
        public SelectBoxPickerDto UserRoleSelectBoxPickerDto { get; set; }
    }
}