using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class EventMap : DomainEntityMap<Event>
    {
        public EventMap()
        {
            References(x => x.EventType);
            References(x => x.ReadOnlyField).Access.CamelCaseField(Prefix.Underscore).LazyLoad();
            Map(x => x.ScheduledDate);
            Map(x => x.StartTime);
            Map(x => x.EndTime);
            Map(x => x.Notes);
        }
    }
}