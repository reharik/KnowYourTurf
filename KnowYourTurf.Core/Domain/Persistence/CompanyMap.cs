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
            Map(x => x.NumberOfSites);
            HasMany(x => x.Sites).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
        }
    }

    public class SiteMap : DomainEntityMap<Site>
    {
        public SiteMap()
        {
            Map(x => x.Name);
            Map(x => x.SiteOperation);
            Map(x => x.Description);
            HasMany(x => x.Fields).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
        }
    }
}