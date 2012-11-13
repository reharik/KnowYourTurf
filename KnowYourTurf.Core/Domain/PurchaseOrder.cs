using System;
using System.Collections.Generic;
using System.Linq;
using CC.Core.Domain;
using Castle.Components.Validator;

namespace KnowYourTurf.Core.Domain
{
    public class PurchaseOrder : DomainEntity, IPersistableObject
    {
        [ValidateNonEmpty]
        public virtual FieldVendor FieldVendor { get; set; }
        [ValidateDecimalAttribute]
        public virtual double? SubTotal { get; set; }
        [ValidateDecimalAttribute]
        public virtual double? Tax { get; set; }
        [ValidateDecimalAttribute]
        public virtual double? Fees { get; set; }
        [ValidateDecimalAttribute]
        public virtual double? Total { get; set; }
        public virtual DateTime? DateReceived { get; set; }
        public virtual bool Completed { get; set; }

        private readonly IList<PurchaseOrderLineItem> _lineItems = new List<PurchaseOrderLineItem>();
        public virtual IEnumerable<PurchaseOrderLineItem> LineItems { get { return _lineItems.AsEnumerable(); } }
        public virtual void RemoveLineItem(PurchaseOrderLineItem purchaseOrderLineItem) { _lineItems.Remove(purchaseOrderLineItem); purchaseOrderLineItem.PurchaseOrder = null; }
        public virtual void AddLineItem(PurchaseOrderLineItem purchaseOrderLineItem)
        {
            if (_lineItems.Contains(purchaseOrderLineItem)) return;
            _lineItems.Add(purchaseOrderLineItem);
            purchaseOrderLineItem.PurchaseOrder = this;
        }
    }
}