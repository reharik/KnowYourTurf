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
        /// </summary>
        private Vendor _readOnlyVendor;
        public virtual Vendor ReadOnlyVendor { get { return _readOnlyVendor; } }
        public virtual void SetVendor(Vendor vendor)
        {
            _readOnlyVendor = vendor;
        }
        ////
        [ValidateNonEmpty]
        [ValidateDecimal]
        public virtual double TotalHours { get; set; }
        public virtual string ImageUrl { get; set; }
    }
}