using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Web.Models
{
    public class EmployeeDashboardViewModel : ViewModel
    {
        public IEnumerable<TokenInputDto> AvailableItems { get; set; }
        public IEnumerable<TokenInputDto> SelectedItems { get; set; }
        public string PendingGridUrl { get; set; }
        public string CompletedGridUrl { get; set; }
        public bool ReturnToList { get; set; }




        public string EmployeeId { get; set; }
        [ValidateNonEmpty]
        public string FirstName { get; set; }
        [ValidateNonEmpty]
        public string LastName { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyContactPhone { get; set; }
        public UserLoginInfoViewModel UserLoginInfo { get; set; }
        [ValidateNonEmpty]
        public string Email { get; set; }
        [ValidateNonEmpty]
        public string PhoneMobile { get; set; }
        public string ImageUrl { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Notes { get; set; }
        public string RolesInput { get; set; }
        public IEnumerable<SelectListItem> StateList { get; set; }
        public string SaveUrl { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        [ValidateNonEmpty]
        public string Password { get; set; }
        [ValidateNonEmpty]
        public string Status { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
    }
}