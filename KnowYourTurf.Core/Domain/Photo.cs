using CC.Core.Domain;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Web.Controllers
{
    public class Photo : DomainEntity, IPersistableObject
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual PhotoCategory PhotoCategory { get; set; }
        public virtual string FileUrl { get; set; }
    }
}