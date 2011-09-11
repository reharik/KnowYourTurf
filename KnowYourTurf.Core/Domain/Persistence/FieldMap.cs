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
            HasMany(x => x.GetPendingTasks()).Where(x => x.Status == TemporalStatus.Pending.Key).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
            HasMany(x => x.GetCompletedTasks()).Where(x => x.Status == TemporalStatus.Complete.Key).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
            HasMany(x => x.GetEvents()).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
            HasMany(x => x.GetDocuments()).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
            HasMany(x => x.GetPhotos()).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.AllDeleteOrphan();
        }
    }
}