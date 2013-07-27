using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Config
{
    public class CompanyConditionFilter : FilterDefinition
    {
        public CompanyConditionFilter()
        {
            WithName("CompanyConditionFilter")
                .AddParameter("CompanyId", NHibernate.NHibernateUtil.Int32);
        }
    }

    public class DeletedConditionFilter : FilterDefinition
    {
        public DeletedConditionFilter()
        {
            WithName("DeletedConditionFilter")
                .AddParameter("Archived", NHibernate.NHibernateUtil.Boolean);
        }
    }
}