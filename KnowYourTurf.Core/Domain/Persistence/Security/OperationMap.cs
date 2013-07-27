using FluentNHibernate.Mapping;
using Rhino.Security.Model;

namespace DecisionCritical.Core.Domain.Persistence
{
    public class OperationMap : ClassMap<Operation>
    {
        public OperationMap()
        {
            Table("security_Operations");
            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue("00000000-0000-0000-0000-000000000000");
            Map(x => x.Name, "[Name]").Not.Nullable().Length(255).Unique();
            Map(x => x.Comment).Length(255);
            References(x => x.Parent).Column("Parent");
            HasMany(x => x.Children).LazyLoad().Inverse().KeyColumn("Parent").Cache.ReadWrite().Region("rhino-security");
        }

    }
}