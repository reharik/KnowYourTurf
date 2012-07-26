using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Web.Models
{
    public class EmployeeDashboardViewModel : ViewModel
    {
        public TokenInputViewModel UserRoles { get; set; }
        public string pendingGridUrl { get; set; }
        public string completedGridUrl { get; set; }
        public bool returnToList { get; set; }




        public string EmployeeId { get; set; }
        [ValidateNonEmpty]
        public string FirstName { get; set; }
        [ValidateNonEmpty]
        public string LastName { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyContactPhone { get; set; }
        [ValidateNonEmpty]
        public string UserLoginInfoPassword { get; set; }
        [ValidateNonEmpty]
        [ValueOfEnumeration(typeof(Status))]
        public string UserLoginInfoStatus { get; set; }
        public IEnumerable<SelectListItem> UserLoginInfoStatusList { get; set; }
        [ValidateNonEmpty]
        public string Email { get; set; }
        [ValidateNonEmpty]
        public string PhoneMobile { get; set; }
        public string ImageUrl { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        [ValueOfEnumeration(typeof(State))]
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Notes { get; set; }
        public string RolesInput { get; set; }
        public IEnumerable<SelectListItem> StateList { get; set; }
        public string saveUrl { get; set; }

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