using System.Collections.Generic;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.CustomAttributes;
using CC.Core.Enumerations;
using CC.Core.Localization;
using Castle.Components.Validator;
using Status = KnowYourTurf.Core.Enums.Status;

namespace KnowYourTurf.Web.Models
{
    public class UserViewModel : ViewModel
    {
        public TokenInputViewModel UserRoles { get; set; }
        public string _pendingGridUrl { get; set; }
        public string _completedGridUrl { get; set; }
        public string _pendingEMGridUrl { get; set; }
        public string _completedEMGridUrl { get; set; }
        public bool _returnToList { get; set; }

        public bool DeleteImage { get; set; }
        public string EmployeeId { get; set; }
        [ValidateNonEmpty]
        public string FirstName { get; set; }
        [ValidateNonEmpty]
        public string LastName { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string UserLoginInfoPassword { get; set; }
        public string PasswordConfirmation { get; set; }
        [ValidateNonEmpty]
        [ValueOf(typeof(Status))]
        public string UserLoginInfoStatus { get; set; }
        public IEnumerable<SelectListItem> _UserLoginInfoStatusList { get; set; }
        [ValidateNonEmpty]
        public string Email { get; set; }
        [ValidateNonEmpty]
        public string PhoneMobile { get; set; }
        public string FileUrl { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        [ValueOf(typeof(State))]
        public string State { get; set; }
        public string ZipCode { get; set; }
        [TextArea]
        public string Notes { get; set; }
        public IEnumerable<SelectListItem> _StateList { get; set; }
        public string _saveUrl { get; set; }
    }
}