using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core.Domain
{
    public class PurchaseOrder : DomainEntity
    {
        [ValidateNonEmpty]
        public virtual Vendor Vendor { get; set; }
        [ValidateDecimalAttribute]
        public virtual decimal SubTotal { get; set; }
        [ValidateDecimalAttribute]
        public virtual decimal Tax { get; set; }
        [ValidateDecimalAttribute]
        public virtual decimal Fees { get; set; }
        [ValidateDecimalAttribute]
        public virtual decimal Total { get; set; }
        public virtual DateTime? DateReceived { get; set; }
        [ValueOf(typeof(TemporalStatus))]
        public virtual string Status { get; set; }
        public virtual bool Completed { get; set; }

        private readonly IList<PurchaseOrderLineItem> _lineItems = new List<PurchaseOrderLineItem>();
        public virtual IEnumerable<PurchaseOrderLineItem> GetLineItems() { return _lineItems.AsEnumerable(); }
        public virtual void RemoveLineItem(PurchaseOrderLineItem purchaseOrderLineItem) { _lineItems.Remove(purchaseOrderLineItem); purchaseOrderLineItem.PurchaseOrder = null; }
        public virtual void AddLineItem(PurchaseOrderLineItem purchaseOrderLineItem)
        {
            if (_lineItems.Contains(purchaseOrderLineItem)) return;
            _lineItems.Add(purchaseOrderLineItem);
            purchaseOrderLineItem.PurchaseOrder = this;
        }
    }
}