using CC.Security.Model;
using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class UsersGroupMap : ClassMap<UsersGroup>
    {
        public UsersGroupMap()
        {
            Cache.ReadWrite().Region("cc-security");
            Table("UsersGroups");
            Id(x => x.EntityId);

            Map(x => x.Name).Not.Nullable().Length(255).Unique();
            Map(x => x.Description).Length(1000);
            References(x => x.Parent).Column("ParentId");
            HasMany(x => x.DirectChildren).AsSet()
                .KeyColumn("Parent")
                .LazyLoad()
                .Inverse()
                .Cache.ReadWrite()
                .Region("cc-security");
            HasManyToMany(x => x.Users)
                .AsSet()
                .Table("UsersToUsersGroups")
                .ParentKeyColumn("GroupId")
                .ChildKeyColumn("UserId")
                .LazyLoad()
                .Cache.ReadWrite()
                .Region("cc-security");
            HasManyToMany(x => x.AllChildren).AsSet()
                .Table("UsersGroupsHierarchy")
                .ParentKeyColumn("ParentGroup")
                .ChildKeyColumn("ChildGroup")
                .LazyLoad()
                .Cache.ReadWrite()
                .Region("cc-security");
            HasManyToMany(x => x.AllParents).AsSet()
                .Table("UsersGroupsHierarchy")
                .ParentKeyColumn("ChildGroup")
                .ChildKeyColumn("ParentGroup")
                .LazyLoad()
                .Cache.ReadWrite()
                .Region("cc-security");
        }

    }
}