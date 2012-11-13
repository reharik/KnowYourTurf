using System;
using CC.Core.Domain;
using CC.Core.Localization;
using Castle.Components.Validator;
using KnowYourTurf.Core.Enums;

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

        public virtual FieldVendor LastFieldVendor { get;  set; }
        [ValidateNonEmpty]
        public virtual BaseProduct Product { get; set; }
    }
}