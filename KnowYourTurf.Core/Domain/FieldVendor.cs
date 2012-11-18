using System.Collections.Generic;
using System.Linq;

namespace KnowYourTurf.Core.Domain
{
    public class FieldVendor : VendorBase
    {
        #region Collections

        public virtual void ClearProducts() { _products = new List<BaseProduct>(); }
        private IList<BaseProduct> _products = new List<BaseProduct>();
        public virtual IEnumerable<BaseProduct> Products { get { return _products; } }
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
        public virtual IEnumerable<PurchaseOrder> PurchaseOrders { get { return _purchaseOrders; } } 
        public virtual IEnumerable<PurchaseOrder> GetCompletedPurchaseOrders() { return _purchaseOrders.Where(x => x.Completed); }
        public virtual void AddPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            if (!purchaseOrder.IsNew() && _purchaseOrders.Contains(purchaseOrder)) return;
            _purchaseOrders.Add(purchaseOrder);
            purchaseOrder.Vendor =this;
        }
        public virtual void RemovePurchaseOrder(PurchaseOrder purchaseOrder) { _purchaseOrders.Remove(purchaseOrder); }
        public virtual IEnumerable<PurchaseOrder> GetPurchaseOrderInProcess() { return _purchaseOrders.Where(x => !x.Completed); }

        #endregion

        public virtual bool PurchaseOrderHasBeenCompleted(int purchaseOrderId)
        {
            var purchaseOrder = GetCompletedPurchaseOrders().FirstOrDefault(x => x.EntityId == purchaseOrderId);
            foreach (var li in purchaseOrder.LineItems)
                if (li.QuantityOrdered > li.TotalReceived) return false;
            return true;
        }


        public virtual double AmountOfSubTotalForPurchaseOrder(int purchaseOrderId)
        {
            var purchaseOrder = GetCompletedPurchaseOrders().FirstOrDefault(x => x.EntityId == purchaseOrderId);
            return purchaseOrder.LineItems.Sum(x => x.SubTotal.Value);
        }

        public virtual double TotalTaxForPurchaseOrder(int purchaseOrderId)
        {
            var purchaseOrder = GetCompletedPurchaseOrders().FirstOrDefault(x => x.EntityId == purchaseOrderId);
            return purchaseOrder.LineItems.Sum(x => x.Tax.Value);
        }

        public virtual double TotalAmountDueForPurchaseOrder(int purchaseOrderId)
        {
            var purchaseOrder = GetCompletedPurchaseOrders().FirstOrDefault(x => x.EntityId == purchaseOrderId);
            return purchaseOrder.LineItems.Sum(x => x.SubTotal.Value + x.Tax.Value);
        }
    }

    
}