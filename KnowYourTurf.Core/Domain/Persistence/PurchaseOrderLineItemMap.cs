namespace KnowYourTurf.Core.Domain.Persistence
{
    public class PurchaseOrderLineItemMap : DomainEntityMap<PurchaseOrderLineItem>
    {
        public PurchaseOrderLineItemMap()
        {
            Map(x => x.Price);
            Map(x => x.QuantityOrdered);
            Map(x => x.TotalReceived);
            Map(x => x.SubTotal);
            Map(x => x.Tax);
            Map(x => x.Completed);
            Map(x => x.DateRecieved);
            Map(x => x.UnitType);
            References(x => x.Product).Not.LazyLoad();
            References(x => x.PurchaseOrder);
        }

    }
}