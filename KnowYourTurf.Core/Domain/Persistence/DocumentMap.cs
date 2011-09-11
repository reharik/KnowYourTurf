using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class DocumentMap : DomainEntityMap<Document>
    {
        public DocumentMap()
        {
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.FileUrl);
            References(x => x.DocumentCategory);
        }
    }
}