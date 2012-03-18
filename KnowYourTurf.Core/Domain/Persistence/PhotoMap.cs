using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class PhotoMap : DomainEntityMap<Photo>
    {
        public PhotoMap()
        {
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.FileUrl);
            References(x => x.PhotoCategory);
        }
    }
}