using CC.Core.Enumerations;
using CC.Core.Localization;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using Status = KnowYourTurf.Core.Enums.Status;

namespace KnowYourTurf.Core.Domain
{
    public class VendorContact : DomainEntity
    {
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
        public virtual string Country { get; set; }
        [ValidateNonEmpty]
        public virtual string Phone { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Email { get; set; }
        public virtual string FullName { get { return FirstName + " " + LastName; } }
        [ValueOf(typeof(Status))]
        public virtual string Status { get; set; }
        [TextArea]
        public virtual string Notes { get; set; }
        [ValidateNonEmpty]
        public virtual Vendor Vendor { get; set; }
    }
}

   