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
            References(x=>x.ReadOnlyCategory).Column("Category_id").Access.CamelCaseField(Prefix.Underscore).LazyLoad();
            HasMany(x => x.Tasks).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
            HasMany(x => x.Events).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
            HasManyToMany(x => x.Documents).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
            HasManyToMany(x => x.Photos).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
        }
    }
}