using System;
using System.Collections.Generic;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using KnowYourTurf.Core.Enumerations;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using System.Linq;
using Rhino.Security;

namespace KnowYourTurf.Core.Domain
{
    public class  User : DomainEntity, IUser
    {
        public virtual string UserId { get; set; }
        [ValidateNonEmpty]
        public virtual string FirstName { get; set; }
        public virtual string MiddleInitial { get; set; }
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
        [ValueOf(typeof(Status))]
        public virtual string Status { get; set; }
        public virtual string Color { get; set; }
        
        public virtual UserLoginInfo UserLoginInfo { get; set; }
        public virtual string FullNameLNF
        {
            get { return LastName + ", " + FirstName; }
        }
        public virtual string FullNameFNF
        {
            get { return FirstName + " " + LastName; }
        }
       
        #region Collections
        private IList<UserRole> _userRoles = new List<UserRole>();
        public virtual void EmptyUserRoles() { _userRoles.Clear(); }
        public virtual IEnumerable<UserRole> UserRoles { get { return _userRoles; } }
        public virtual void RemoveUserRole(UserRole userRole)
        {
            _userRoles.Remove(userRole);
        }
        public virtual void AddUserRole(UserRole userRole)
        {
            if (_userRoles.Contains(userRole)) return;
            _userRoles.Add(userRole);
        }

        #endregion

        public virtual SecurityInfo SecurityInfo
        {
            get { return new SecurityInfo(FullNameLNF, EntityId); }
        }

    }

    public class UserLoginInfo : DomainEntity
    {
        [ValidateNonEmpty]
        public virtual string LoginName { get; set; }
        [ValidateNonEmpty]
        public virtual string Password { get; set; }
        [ValidateSqlDateTime]
        public virtual string Salt { get; set; }
        public virtual bool CanLogin { get; set; }
        [ValidateSqlDateTime]
        public virtual DateTime? LastVisitDate { get; set; }
        public virtual Guid ByPassToken { get; set; }

        #region Collections
        #endregion
    }
}