using FluentNHibernate;
using FluentNHibernate.Mapping;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class FieldMap : DomainEntityMap<Field>
    {
        public FieldMap()
        {
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Abbreviation);
            Map(x => x.Size);
            Map(x => x.ImageUrl);
            Map(x => x.Status);
            Map(x => x.FieldColor);
            References(x=>x.ReadOnlyCategory).Access.CamelCaseField(Prefix.Underscore).LazyLoad();
            HasMany(x => x.Tasks).Inverse().Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
            HasMany(x => x.Events).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
            HasMany(x => x.Documents).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
            HasMany(x => x.Photos).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
        }
    }
}