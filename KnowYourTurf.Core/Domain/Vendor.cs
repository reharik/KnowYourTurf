using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using FluentNHibernate.MappingModel;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core.Domain
{
    public class Vendor : DomainEntity
    {
        [ValidateNonEmpty]
        public virtual string Company { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        [ValueOf(typeof(State))]
        public virtual string State { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string Country { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Website { get; set; }
        public virtual string LogoUrl { get; set; }
        [TextArea(2, 60)]
        public virtual string Notes { get; set; }
        [ValueOf(typeof(Status))]
        public virtual string Status { get; set; }

        #region Collections
        private readonly IList<VendorContact> _contacts = new List<VendorContact>();
        public virtual IEnumerable<VendorContact> GetContacts() { return _contacts; }
        public virtual void RemoveContact(VendorContact contact) { _contacts.Remove(contact); }
        public virtual void AddContact(VendorContact contact)
        {
            if (!contact.IsNew() && _contacts.Contains(contact)) return;
            _contacts.Add(contact);
            contact.Vendor = this;
        }

        public virtual void ClearProducts() { _products = new List<BaseProduct>(); }
        private IList<BaseProduct> _products = new List<BaseProduct>();
        public virtual IEnumerable<BaseProduct> GetProducts() { return _products; }
        public virtual void RemoveProduct(BaseProduct product) { _products.Remove(product); }
        public virtual void AddProduct(BaseProduct product)
        {
            if (product == null || !product.IsNew() && _products.Contains(product)) return;
            _products.Add(product);
        }

        public virtual IEnumerable<BaseProduct> GetAllProductsOfType(string productType)
        {
            return _products.Where(x => x.InstantiatingType == productType);
        }

        private readonly IList<PurchaseOrder> _purchaseOrders = new List<PurchaseOrder>();
        public virtual IEnumerable<PurchaseOrder> GetPurchaseOrders() { return _purchaseOrders; } 
        public virtual IEnumerable<PurchaseOrder> GetCompletedPurchaseOrders() { return _purchaseOrders.Where(x => x.Completed); }
        public virtual void AddPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            if (!purchaseOrder.IsNew() && _purchaseOrders.Contains(purchaseOrder)) return;
            _purchaseOrders.Add(purchaseOrder);
            purchaseOrder.Vendor = this;
        }
        public virtual void RemovePurchaseOrder(PurchaseOrder purchaseOrder) { _purchaseOrders.Remove(purchaseOrder); }
        public virtual IEnumerable<PurchaseOrder> GetPurchaseOrderInProcess() { return _purchaseOrders.Where(x => !x.Completed); }

        #endregion

        public virtual bool PurchaseOrderHasBeenCompleted(long purchaseOrderId)
        {
            var purchaseOrder = GetCompletedPurchaseOrders().FirstOrDefault(x => x.EntityId == purchaseOrderId);
            foreach (var li in purchaseOrder.GetLineItems())
                if (li.QuantityOrdered > li.TotalReceived) return false;
            return true;
        }


        public virtual decimal AmountOfSubTotalForPurchaseOrder(long purchaseOrderId)
        {
            var purchaseOrder = GetCompletedPurchaseOrders().FirstOrDefault(x => x.EntityId == purchaseOrderId);
            return purchaseOrder.GetLineItems().Sum(x => x.SubTotal.Value);
        }

        public virtual decimal TotalTaxForPurchaseOrder(long purchaseOrderId)
        {
            var purchaseOrder = GetCompletedPurchaseOrders().FirstOrDefault(x => x.EntityId == purchaseOrderId);
            return purchaseOrder.GetLineItems().Sum(x => x.Tax.Value);
        }

        public virtual decimal TotalAmountDueForPurchaseOrder(long purchaseOrderId)
        {
            var purchaseOrder = GetCompletedPurchaseOrders().FirstOrDefault(x => x.EntityId == purchaseOrderId);
            return purchaseOrder.GetLineItems().Sum(x => x.SubTotal.Value + x.Tax.Value);
        }
    }

    
}