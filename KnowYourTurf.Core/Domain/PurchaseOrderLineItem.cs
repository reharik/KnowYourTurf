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
        [ValidateNonEmpty]
        public virtual BaseProduct Product { get; set; }
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
            return Equals(obj.Product.EntityId, Product.EntityId) && Equals(obj.GetType(), GetType());
        }
    }

   
}
