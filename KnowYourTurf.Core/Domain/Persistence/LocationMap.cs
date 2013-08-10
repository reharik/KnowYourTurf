using FluentNHibernate.Mapping;
using KnowYourTurf.Web.Areas.Schedule.Controllers;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class LocationMap : DomainEntityMap<Location>
    {
        public LocationMap()
        {
            Map(x => x.Name);
            Map(x => x.Address1);
            Map(x => x.Address2);
            Map(x => x.City);
            Map(x => x.State);
            Map(x => x.Zip);
        } 
    }
}