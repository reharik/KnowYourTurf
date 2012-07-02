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
            References(x => x.ReadOnlyLastVendor).Access.CamelCaseField(Prefix.Underscore).LazyLoad();
            References(x => x.ReadOnlyProduct).Access.CamelCaseField(Prefix.Underscore).Not.LazyLoad();
            DiscriminateSubClassesOnColumn<string>("Discriminator").AlwaysSelectWithValue();

        }
    }
}