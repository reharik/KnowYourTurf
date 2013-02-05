using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Config
{

    public class ClientConditionFilter : FilterDefinition
    {
        public ClientConditionFilter()
        {
            WithName("ClientConditionFilter")
                .AddParameter("ClientId",NHibernate.NHibernateUtil.Int32);
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

