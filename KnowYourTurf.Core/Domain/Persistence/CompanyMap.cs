using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class CompanyMap : DomainEntityMap<Company>
    {
        public CompanyMap()
        {
            Map(x => x.Name);
            Map(x => x.ZipCode);
            Map(x => x.TaxRate);
        }
    }
}