using FluentNHibernate.Mapping;
using KnowYourTurf.Web.Areas.Schedule.Controllers;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class AppointmentMap : DomainEntityMap<Appointment>
    {
        public AppointmentMap()
        {
            Map(x => x.ScheduledDate);
            Map(x => x.ScheduledStartTime);
            Map(x => x.ScheduledEndTime);
            References(x => x.Location);
            References(x => x.Trainer);
            HasManyToMany(x => x.Clients).Access.CamelCaseField(Prefix.Underscore);

        } 
    }
}