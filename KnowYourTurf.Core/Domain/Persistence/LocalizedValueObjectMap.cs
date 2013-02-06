
namespace KnowYourTurf.Core.Domain.Persistence
{
    public class LocalizedEnumerationMap : DomainEntityMap<LocalizedEnumeration>
    {
        public LocalizedEnumerationMap()
        {
            Map(x => x.Culture);
            Map(x => x.Name);
            Map(x => x.ValueType);
            Map(x => x.Text);
            Map(x => x.Tooltip);
        }
    }
}