using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Config
{

    public class CompanyConditionFilter : FilterDefinition
    {
        public CompanyConditionFilter()
        {
            WithName("CompanyConditionFilter")
                .AddParameter("CompanyId",NHibernate.NHibernateUtil.Int64);
        }
    }

    public class IsDeletedConditionFilter : FilterDefinition
    {
        public IsDeletedConditionFilter()
        {
            WithName("IsDeletedConditionFilter")
                .AddParameter("IsDeleted", NHibernate.NHibernateUtil.Boolean);
        }
    }
}

