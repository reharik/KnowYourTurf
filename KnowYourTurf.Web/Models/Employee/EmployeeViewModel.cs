using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Models
{
    public class EmployeeViewModel:ViewModel
    {
        public bool DeleteImage { get; set; }
        public Employee Employee { get; set; }
        public SelectBoxPickerDto UserRoleSelectBoxPickerDto { get; set; }
    }
}