using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Config
{

    public class CompanyConditionFilter : FilterDefinition
    {
        public CompanyConditionFilter()
        {
            WithName("CompanyConditionFilter")
                .AddParameter("CompanyId",NHibernate.NHibernateUtil.Int32);
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
    public class StatusConditionFilter : FilterDefinition
    {
        public StatusConditionFilter()
        {
            WithName("StatusConditionFilter")
                .AddParameter("condition", NHibernate.NHibernateUtil.String);
        }
    }
}

