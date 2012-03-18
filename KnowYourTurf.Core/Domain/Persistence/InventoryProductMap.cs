using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class InventoryProductMap : DomainEntityMap<InventoryProduct>
    {
        public InventoryProductMap()
        {
            Map(x => x.Quantity);
            Map(x => x.Description);
            Map(x => x.DatePurchased);
            Map(x => x.LastUsed);
            Map(x => x.Cost);
            Map(x => x.UnitType);
            Map(x => x.SizeOfUnit);
            References(x => x.LastVendor);
            References(x => x.Product).Not.LazyLoad();
            DiscriminateSubClassesOnColumn<string>("Discriminator").AlwaysSelectWithValue();

        }
    }
}