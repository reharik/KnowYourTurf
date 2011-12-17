using KnowYourTurf.Core.Config;
using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class DomainEntityMap<DOMAINENTITY> : EntityMap<DOMAINENTITY> where DOMAINENTITY : DomainEntity
    {
        public DomainEntityMap()
        {
            Map(x => x.CompanyId);
            ApplyFilter<CompanyConditionFilter>("(CompanyId= :CompanyId)");
        }
    }



    public class EntityMap<ENTITY> : ClassMap<ENTITY> where ENTITY : Entity
    {
        public EntityMap()
        {
            Id(x => x.EntityId);
            Map(x => x.CreateDate)
                .Default("(getdate())");
            Map(x => x.ChangeDate)
                //.Not.Nullable()
                .Default("(getdate())");
            Map(x => x.ChangedBy);
            Map(x => x.Archived);
            ApplyFilter<DeletedConditionFilter>("Archived= :Archived");
        }
    }
}
