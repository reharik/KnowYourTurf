using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class VendorMap : DomainEntityMap<Vendor>
    {
        public VendorMap()
        {
            Map(x => x.Company);
            Map(x => x.Address1);
            Map(x => x.Address2);
            Map(x => x.City);
            Map(x => x.State);
            Map(x => x.ZipCode);
            Map(x => x.Country);
            Map(x => x.Fax);
            Map(x => x.LogoUrl);
            Map(x => x.Notes);
            Map(x => x.Phone);
            Map(x => x.Status);
            Map(x => x.Website);
            HasMany(x => x.GetContacts()).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
            HasManyToMany(x => x.GetProducts()).Access.CamelCaseField(Prefix.Underscore).LazyLoad();
            HasMany(x => x.GetPurchaseOrders()).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
        }
    }
}