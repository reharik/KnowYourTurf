using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Web.Models.VendorContact
{
    public class VendorContactViewModel:ViewModel
    {
        public string _saveUrl { get; set; }
        [ValidateNonEmpty]
        public virtual string FirstName { get; set; }
        [ValidateNonEmpty]
        public virtual string LastName { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        [ValueOf(typeof(State))]
        public virtual string State { get; set; }
        public virtual string ZipCode { get; set; }
        [ValidateNonEmpty]
        public virtual string Phone { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Email { get; set; }
        [ValueOf(typeof(Status))]
        public virtual string Status { get; set; }
        [TextArea]
        public virtual string Notes { get; set; }

        public IEnumerable<SelectListItem> _StatusList { get; set; }
        public IEnumerable<SelectListItem> _StateList { get; set; }
    }
}