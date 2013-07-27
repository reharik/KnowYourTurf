using FluentNHibernate.Mapping;
using Rhino.Security.Model;

namespace DecisionCritical.Core.Domain.Persistence
{
    public class PermissionMap : ClassMap<Permission>
    {
        public PermissionMap()
        {
            Table("security_Permissions");
            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue("00000000-0000-0000-0000-000000000000");
            Map(x => x.EntitySecurityKey);
            Map(x => x.Allow).Not.Nullable();
            Map(x => x.Level);
            Map(x => x.EntityTypeName);
            References(x => x.Operation).Column("Operation").Not.Nullable();
            References(x => x.User).Column("[User]").Class<User>();
            References(x => x.UsersGroup).Column("UsersGroup");
            References(x => x.EntitiesGroup).Column("EntitiesGroup");
        }

    }
}