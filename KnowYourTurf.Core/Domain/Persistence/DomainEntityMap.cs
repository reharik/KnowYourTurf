using CC.Core.Domain;
using FluentNHibernate.Mapping;
using KnowYourTurf.Core.Config;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class EntityMap<ENTITY> : ClassMap<ENTITY>
        where ENTITY : Entity
    {
        public EntityMap()
        {
            Id(x => x.EntityId);
            Map(x => x.ChangeDate);
            Map(x => x.CreatedDate);
            Map(x => x.IsDeleted);
//            References(x => x.CreatedBy);
//            References(x => x.ModifiedBy);
        }

    }
    public class DomainEntityMap<DOMAINENTITY> : EntityMap<DOMAINENTITY> where DOMAINENTITY : DomainEntity
    {   
        public DomainEntityMap()
        {
            Map(x => x.CompanyId);
            ApplyFilter<CompanyConditionFilter>("(CompanyId= :CompanyId)");
        }

    }
}