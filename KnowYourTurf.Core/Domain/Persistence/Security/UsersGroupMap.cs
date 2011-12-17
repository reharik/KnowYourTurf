using FluentNHibernate.Mapping;
using Rhino.Security.Model;

namespace DecisionCritical.Core.Domain.Persistence
{
    public class UsersGroupMap : ClassMap<UsersGroup>
    {
        public UsersGroupMap()
        {
            Table("security_UsersGroups");
            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue("00000000-0000-0000-0000-000000000000");
            Map(x => x.Name,"[Name]").Not.Nullable().Length(255).Unique();
            References(x => x.Parent).Column("Parent");
            HasMany(x => x.DirectChildren).Table("security_UsersGroups").LazyLoad().Inverse().KeyColumn("Parent");
            HasManyToMany(x => x.AllChildren).LazyLoad().Table("security_UsersGroupsHierarchy").Inverse().ParentKeyColumn("ParentGroup").ChildKeyColumn("ChildGroup").Cache.ReadWrite().Region("rhino-security");
            HasManyToMany(x => x.AllParents).LazyLoad().Table("security_UsersGroupsHierarchy").Inverse().ParentKeyColumn("ChildGroup").ChildKeyColumn("ParentGroup").Cache.ReadWrite().Region("rhino-security");
            HasManyToMany(x => x.Users)
                .CollectionType<User>()
                .LazyLoad()
                .Table("security_UsersToUsersGroups")
                .ParentKeyColumn("GroupId")
                .ChildKeyColumn("UserId")
                .Cache.ReadWrite().Region("rhino-security");
        }

    }
}