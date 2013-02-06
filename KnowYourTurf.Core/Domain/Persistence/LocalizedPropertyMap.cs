
namespace KnowYourTurf.Core.Domain.Persistence
{
    public class LocalizedPropertyMap : DomainEntityMap<LocalizedProperty>
    {
        public LocalizedPropertyMap()
        {
            Map(x => x.Culture);
            Map(x => x.Name);
            Map(x => x.ParentType);
            Map(x => x.Text);
            Map(x => x.Tooltip);
        }
    }
}