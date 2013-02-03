using System.Collections.Generic;
using System.Linq;
using CC.Core.Domain;

namespace KnowYourTurf.Core.Domain
{
    public class Client : DomainEntity, IPersistableObject
    {
        public virtual string Name { get; set; }
        public virtual double TaxRate { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual int NumberOfSites { get; set; }
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
        #endregion
    }
}