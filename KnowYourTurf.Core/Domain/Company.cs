using System.Collections.Generic;
using System.Linq;
using CC.Core.Domain;

namespace KnowYourTurf.Core.Domain
{
    public class Company : DomainEntity, IPersistableObject
    {
        public virtual string Name { get; set; }
        public virtual double TaxRate { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual int NumberOfCategories { get; set; }
        #region Collections
        private IList<Site> _categories = new List<Site>();
        public virtual IEnumerable<Site> Categories { get { return _categories; } }
        public virtual void ClearCategory() { _categories = new List<Site>(); }
        public virtual void RemoveCategory(Site site) { _categories.Remove(site); }
        public virtual void AddCategory(Site site)
        {
            if (!site.IsNew() && _categories.Contains(site)) return;
            _categories.Add(site);
        }
        #endregion
    }
}