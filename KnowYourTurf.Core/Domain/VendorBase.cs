using System.Collections.Generic;
using CC.Core.CustomAttributes;
using CC.Core.Domain;
using CC.Core.Enumerations;
using CC.Core.Localization;
using Castle.Components.Validator;
using Status = KnowYourTurf.Core.Enums.Status;

namespace KnowYourTurf.Core.Domain
{
    public class VendorBase : DomainEntity, IPersistableObject
    {
        private readonly IList<VendorContact> _contacts = new List<VendorContact>();

        [ValidateNonEmpty]
        public virtual string Company { get; set; }

        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }

        [ValueOf(typeof(State))]
        public virtual string State { get; set; }

        public virtual string ZipCode { get; set; }
        public virtual string Country { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Website { get; set; }
        public virtual string LogoUrl { get; set; }

        [TextArea]
        public virtual string Notes { get; set; }

        [ValueOf(typeof(Status))]
        public virtual string Status { get; set; }

        public virtual IEnumerable<VendorContact> Contacts { get { return _contacts; } }
        public virtual void RemoveContact(VendorContact contact) { _contacts.Remove(contact); }

        public virtual void AddContact(VendorContact contact)
        {
            if (!contact.IsNew() && _contacts.Contains(contact)) return;
            _contacts.Add(contact);
            contact.Vendor = this;
        }
    }
}