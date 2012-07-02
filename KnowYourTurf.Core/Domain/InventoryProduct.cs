using System;
using Castle.Components.Validator;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core.Domain
{
    public class InventoryProduct : DomainEntity, IPersistableObject
    {
        public virtual DateTime? LastUsed { get; set; }
        [ValidateDouble]
        public virtual double? Quantity { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime? DatePurchased { get; set; }
        [ValidateDecimal]
        public virtual double? Cost { get; set; }
        [ValidateInteger]
        public virtual int SizeOfUnit { get; set; }
        [ValueOf(typeof(UnitType)), ValidateNonEmpty]
        public virtual string UnitType { get; set; }
        /// <summary>
        /// Aggregate Root that should not be modified through Email Job
        /// </summary>
        private Vendor _readOnlyLastVendor;
        public virtual Vendor ReadOnlyLastVendor { get { return _readOnlyLastVendor; } }
        public virtual void SetLastVendor(Vendor vendor)
        {
            _readOnlyLastVendor = vendor;
        }
        ////
        /// <summary>
        /// Aggregate Root that should not be modified through Inventory
        /// </summary>
        private BaseProduct _readOnlyProduct;
        [ValidateNonEmpty]
        public virtual BaseProduct ReadOnlyProduct { get { return _readOnlyProduct; } set { _readOnlyProduct = value; } }
        public virtual void SetProduct(BaseProduct product)
        {
            _readOnlyProduct = product;
        }
        ////
    }
}