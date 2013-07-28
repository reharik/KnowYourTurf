using FluentNHibernate.Mapping;
using Rhino.Security.Model;

namespace DecisionCritical.Core.Domain.Persistence
{
    public class EntityReferencepMap : ClassMap<EntityReference>
    {
        public EntityReferencepMap()
        {
            Table("security_EntityReferences");
            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue("00000000-0000-0000-0000-000000000000");
            Map(x => x.EntitySecurityKey).Not.Nullable().Unique();
            References(x => x.Type).Column("Type").Not.Nullable();
        }

    }
}