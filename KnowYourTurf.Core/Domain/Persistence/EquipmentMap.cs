using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class EquipmentMap : DomainEntityMap<Equipment>
    {
        public EquipmentMap()
        {
            Map(x => x.Name);
            Map(x => x.TotalHours);
            Map(x => x.Description);
            Map(x => x.Make);
            Map(x => x.Model);
            Map(x => x.SerialNumber);
            Map(x => x.WarrentyInfo);
            References(x => x.Site);
            References(x => x.FieldVendor);
            HasMany(x => x.Tasks).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
            HasManyToMany(x => x.Documents).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
            HasManyToMany(x => x.Photos).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
        }
    }
}