using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class PurchaseOrderMap : DomainEntityMap<PurchaseOrder>
    {
        public PurchaseOrderMap()
        {
            Map(x => x.SubTotal);
            Map(x => x.Completed);
            Map(x => x.Fees);
            Map(x => x.Tax);
            Map(x => x.Total);
            Map(x => x.DateReceived);
            References(x => x.Vendor);
            HasMany(x => x.LineItems).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
        }
    }
}