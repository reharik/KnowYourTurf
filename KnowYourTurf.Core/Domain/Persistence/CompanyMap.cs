using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class CompanyMap : DomainEntityMap<Company>
    {
        public CompanyMap()
        {
            Map(x => x.Name);
            Map(x => x.Latitude);
            Map(x => x.Longitude);
            Map(x => x.TaxRate);
        }
    }
}