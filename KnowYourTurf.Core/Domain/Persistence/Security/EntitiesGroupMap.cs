using FluentNHibernate.Mapping;
using Rhino.Security.Model;

namespace DecisionCritical.Core.Domain.Persistence
{
    public class EntitiesGroupMap : ClassMap<EntitiesGroup>
    {
        public EntitiesGroupMap()
        {
            Table("security_EntitiesGroups");
            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue("00000000-0000-0000-0000-000000000000");
            Map(x => x.Name,"[Name]").Not.Nullable().Length(255).Unique();
            References(x => x.Parent).Column("Parent");
            HasMany(x => x.DirectChildren).LazyLoad().Inverse().KeyColumn("Parent").Cache.ReadWrite().Region("rhino-security");
            HasManyToMany(x => x.AllChildren).LazyLoad().Table("security_EntityGroupsHierarchy").Inverse().ChildKeyColumn("ParentGroup").Cache.ReadWrite().Region("rhino-security");
            HasManyToMany(x => x.AllParents).LazyLoad().Table("security_EntityGroupsHierarchy").Inverse().ChildKeyColumn("ChildGroup").Cache.ReadWrite().Region("rhino-security");
            HasManyToMany(x => x.Entities)
                .LazyLoad()
                .Table("security_EntityReferencesToEntitysGroups")
                .Inverse()
                .ParentKeyColumn("GroupId")
                .ChildKeyColumn("EntityReferenceId")
                .Cache
                .ReadWrite()
                .Region("rhino-security");

        }

    }
}