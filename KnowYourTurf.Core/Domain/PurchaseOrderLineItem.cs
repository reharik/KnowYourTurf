using System;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core.Domain
{
    public class PurchaseOrderLineItem : DomainEntity, IEquatable<PurchaseOrderLineItem>
    {
        /// <summary>
        /// Aggregate Root that should not be modified through PurchaseOrderLineItem
        /// </summary>
        private BaseProduct _readOnlyProduct;
        [ValidateNonEmpty]
        public virtual BaseProduct ReadOnlyProduct { get { return _readOnlyProduct; } }
        public virtual void SetProduct(BaseProduct product)
        {
            _readOnlyProduct = product;
        }
        ////
        [ValidateNonEmpty,ValidateInteger]
        public virtual int? QuantityOrdered { get; set; }
        [ValidateDecimal]
        public virtual double? Price { get; set; }
        [ValidateDecimal]
        public virtual double? SubTotal { get; set; }
        [ValidateDecimal]
        public virtual double? Tax { get; set; }
        [ValidateInteger]
        public virtual int? TotalReceived { get; set; }
        public virtual bool Completed { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual DateTime? DateRecieved { get; set; }
        [ValidateInteger]
        public virtual int? SizeOfUnit { get; set; }
        [ValidateNonEmpty, ValueOf(typeof(UnitType))]
        public virtual string UnitType { get; set; }
        public virtual bool Taxable { get; set; }

        public virtual bool Equals(PurchaseOrderLineItem obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj.ReadOnlyProduct.EntityId, ReadOnlyProduct.EntityId) && Equals(obj.GetType(), GetType());
        }
    }

   
}
