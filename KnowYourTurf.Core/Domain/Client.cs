using System.Collections.Generic;
using System.Linq;
using CC.Core.CustomAttributes;
using CC.Core.Domain;
using CC.Core.Enumerations;
using CC.Core.Localization;

namespace KnowYourTurf.Core.Domain
{
    public class Client : DomainEntity, IPersistableObject
    {
        public virtual string Name { get; set; }
        public virtual double TaxRate { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual int NumberOfSites { get; set; }
        public virtual string Address { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        [ValueOf(typeof(State))]
        public virtual string State { get; set; }
        [TextArea]
        public virtual string Notes { get; set; }
        #region Collections
        private IList<Site> _sites = new List<Site>();
        public virtual IEnumerable<Site> Sites { get { return _sites; } }
        public virtual void ClearSite() { _sites = new List<Site>(); }
        public virtual void RemoveSite(Site site) { _sites.Remove(site); }
        public virtual void AddSite(Site site)
        {
            if (!site.IsNew() && _sites.Contains(site)) return;
            _sites.Add(site);
        }

        private IList<UserRole> _userRoles = new List<UserRole>();
        public virtual IEnumerable<UserRole> UserRoles { get { return _userRoles; } }
        public virtual void ClearUserRole() { _userRoles = new List<UserRole>(); }
        public virtual void RemoveUserRole(UserRole userRole) { _userRoles.Remove(userRole); }
        public virtual void AddUserRole(UserRole userRole)
        {
            if (!userRole.IsNew() && _userRoles.Contains(userRole)) return;
            _userRoles.Add(userRole);
        }
        #endregion
    }
}