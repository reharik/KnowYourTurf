using FubuMVC.Core;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public interface IPurchaseOrderLineItemService
    {
        void AddNewItem(ref PurchaseOrder purchaseOrder, PurchaseOrderLineItem purchaseOrderLineItem);
    }

    public class PurchaseOrderLineItemService : IPurchaseOrderLineItemService
    {
        private readonly ISessionContext _sessionContext;

        public PurchaseOrderLineItemService(ISessionContext sessionContext)
        {
            _sessionContext = sessionContext;
        }

        public void AddNewItem(ref PurchaseOrder purchaseOrder, PurchaseOrderLineItem purchaseOrderLineItem)
        {
            if(purchaseOrderLineItem.QuantityOrdered.HasValue && purchaseOrderLineItem.Price.HasValue)
            {
                purchaseOrderLineItem.SubTotal = purchaseOrderLineItem.QuantityOrdered * purchaseOrderLineItem.Price;
            }
            if(purchaseOrderLineItem.Taxable && purchaseOrderLineItem.SubTotal.HasValue)
            {
                var currentCompany = _sessionContext.GetCurrentCompany();
                purchaseOrderLineItem.Tax = purchaseOrderLineItem.SubTotal.Value*currentCompany.TaxRate*.01;
            }
            purchaseOrder.AddLineItem(purchaseOrderLineItem);
            var sub = 0d;
            var tax = 0d;
            purchaseOrder.LineItems.Each(x =>
                                                  {
                                                      if (x.QuantityOrdered.HasValue && x.Price.HasValue)
                                                      {
                                                          sub += x.QuantityOrdered.Value*x.Price.Value;
                                                          tax += x.Tax.HasValue? x.Tax.Value:0;
                                                      }
                                                  });
            purchaseOrder.SubTotal = sub;
            purchaseOrder.Tax = tax;
            purchaseOrder.Total = sub + tax + (purchaseOrder.Fees.HasValue?purchaseOrder.Fees.Value:0);
        }
    }
}