namespace KnowYourTurf.Core.Domain.Persistence
{
    public class EventMap : DomainEntityMap<Event>
    {
        public EventMap()
        {
            References(x => x.EventType);
            References(x => x.Field);
            Map(x => x.ScheduledDate);
            Map(x => x.StartTime);
            Map(x => x.EndTime);
            Map(x => x.Notes);
        }
    }
}