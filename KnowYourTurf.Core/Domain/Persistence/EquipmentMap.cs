using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class EquipmentMap : DomainEntityMap<Equipment>
    {
        public EquipmentMap()
        {
            Map(x => x.Name);
            Map(x => x.TotalHours);
            Map(x => x.Threshold);
            Map(x => x.Description);
            Map(x => x.Make);
            Map(x => x.Model);
            Map(x => x.SerialNumber);
            Map(x => x.WarrentyInfo);
            Map(x => x.WebSite);
            Map(x => x.ID);
            References(x => x.EquipmentVendor).Column("Vendor_id");
            References(x => x.EquipmentType);
            HasMany(x => x.Tasks).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
            HasManyToMany(x => x.Documents).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
            HasManyToMany(x => x.Photos).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
        }
    }
}