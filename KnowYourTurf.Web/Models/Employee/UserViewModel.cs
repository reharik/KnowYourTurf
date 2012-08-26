using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Models
{
    public class UserViewModel : ViewModel
    {
        public TokenInputViewModel UserRoles { get; set; }
        public string _pendingGridUrl { get; set; }
        public string _completedGridUrl { get; set; }
        public bool _returnToList { get; set; }




        public bool DeleteImage { get; set; }
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
        public string Notes { get; set; }
        public IEnumerable<SelectListItem> _StateList { get; set; }
        public string _saveUrl { get; set; }
    }
}