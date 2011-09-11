using FluentNHibernate.Mapping;
using KnowYourTurf.Core.Config;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class DomainEntityMap<ENTITY> : ClassMap<ENTITY>
        where ENTITY : DomainEntity
    {   
        public DomainEntityMap()
        {
            Id(x => x.EntityId);
            Map(x => x.LastModified);
            Map(x => x.DateCreated);
            Map(x => x.CompanyId);
            Map(x => x.IsDeleted);
            References(x => x.CreatedBy);
            References(x => x.ModifiedBy);
            ApplyFilter<CompanyConditionFilter>("CompanyId= :CompanyId");
        }

    }
}