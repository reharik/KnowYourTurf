using System;
using System.Collections.Generic;
using System.Globalization;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;
using Rhino.Security;

namespace KnowYourTurf.Core.Domain
{
    public class User : DomainEntity,IUser
    {
        public virtual string LoginName { get; set; }
        [ValidateNonEmpty]
        public virtual string Password { get; set; }
        [ValidateNonEmpty, ValueOf(typeof(Status))]
        public virtual string Status { get; set; }
        public virtual bool IsAuthenticated { get; set; }
        public virtual CultureInfo LanguageDefault { get; set; }
        public virtual string UserRoles { get; set; }
        [ValidateNonEmpty]
        public virtual string FirstName { get; set; }
        [ValidateNonEmpty]
        public virtual string LastName { get; set; }
        [ValidateNonEmpty]
        public virtual string Email { get; set; }
        [ValidateNonEmpty]
        public virtual string PhoneMobile { get; set; }
        public virtual string PhoneHome { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        [ValueOf(typeof(State))]
        public virtual string State { get; set; }
        public virtual string ZipCode { get; set; }
        [TextArea]
        public virtual string Notes { get; set; }
        public virtual DateTime? BirthDate { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual Company Company { get; set; }
        public virtual string FullName
        {
            get { return "{0} {1}".ToFormat(FirstName, LastName); }
        }

        #region Collections
        private readonly IList<EmailJob> _emailTemplates = new List<EmailJob>();
        public virtual IEnumerable<EmailJob> GetEmailTemplates() { return _emailTemplates; }
        public virtual void RemoveEmailTemplate(EmailJob emailJob) { _emailTemplates.Remove(emailJob); }
        public virtual void AddEmailTemplate(EmailJob emailJob)
        {
            if (!emailJob.IsNew() && _emailTemplates.Contains(emailJob)) return;
            _emailTemplates.Add(emailJob);
        }
        #endregion

        public virtual SecurityInfo SecurityInfo
        {
            get { return new SecurityInfo(FullName, EntityId); }
        }
    }
}