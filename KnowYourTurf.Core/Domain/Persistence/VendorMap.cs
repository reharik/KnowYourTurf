using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class VendorBaseMap : DomainEntityMap<VendorBase>
    {
        public VendorBaseMap()
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
            HasMany(x => x.Contacts).Access.CamelCaseField(Prefix.Underscore).KeyColumn("Vendor_id").LazyLoad().Cascade.AllDeleteOrphan();
            DiscriminateSubClassesOnColumn<string>("VendorType");
        }
    }

    public class EquipmentVendorMap : SubclassMap<EquipmentVendor>
    {
        public EquipmentVendorMap()
        {
            DiscriminatorValue("EquipmentVendor");
            HasManyToMany(x => x.EquipmentTypes).Access.CamelCaseField(Prefix.Underscore).ParentKeyColumn("Vendor_id").LazyLoad();
        }
    }

    public class FieldVendorMap : SubclassMap<FieldVendor>
    {
        public FieldVendorMap()
        {
            DiscriminatorValue("FieldVendor");
            HasManyToMany(x => x.Products).Access.CamelCaseField(Prefix.Underscore).ParentKeyColumn("Vendor_id").LazyLoad();
            HasMany(x => x.PurchaseOrders).Access.CamelCaseField(Prefix.Underscore).KeyColumn("Vendor_id").LazyLoad().Cascade.AllDeleteOrphan();
        }
    }
}