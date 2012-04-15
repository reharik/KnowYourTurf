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
            Map(x => x.NumberOfCategories);
            HasMany(x => x.Categories).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
        }
    }

    public class CategoryMap : DomainEntityMap<Category>
    {
        public CategoryMap()
        {
            Map(x => x.Name);
            Map(x => x.Description);
            HasMany(x => x.Fields).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
            HasMany(x => x.Tasks).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
            HasMany(x => x.Events).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
        }
    }
}