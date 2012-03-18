
namespace KnowYourTurf.Core.Domain.Persistence
{
    public class LocalizedTextMap : DomainEntityMap<LocalizedText>
    {
        public LocalizedTextMap()
        {
            Map(x => x.Culture);
            Map(x => x.Name);
            Map(x => x.Text).Length(1000);
        }
    }
}