using CC.Core.Domain;
using Castle.Components.Validator;

namespace KnowYourTurf.Core.Domain
{
    public class Equipment : DomainEntity, IPersistableObject
    {
        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        /// <summary>
        /// Aggregate Root that should not be modified through Equipment
        /// must have set on readonly field right now for model binder.
        /// </summary>
        private Vendor _vendor;
        public virtual Vendor Vendor { get { return _vendor; } set { _vendor = value; } }
        public virtual void SetVendor(Vendor vendor)
        {
            _vendor = vendor;
        }
        ////
        [ValidateNonEmpty]
        [ValidateDecimal]
        public virtual double TotalHours { get; set; }
        public virtual string FileUrl { get; set; }
    }
}