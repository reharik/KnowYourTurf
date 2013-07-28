using FluentNHibernate.Mapping;
using Rhino.Security.Model;

namespace DecisionCritical.Core.Domain.Persistence
{
    public class EntityTypeMap : ClassMap<EntityType>
    {
        public EntityTypeMap()
        {
            Table("security_EntityTypes");
            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue("00000000-0000-0000-0000-000000000000");
            Map(x => x.Name,"[Name]").Not.Nullable().Length(255).Unique();
        }

    }
}