using CC.Core.Domain;
using Castle.Components.Validator;

namespace KnowYourTurf.Core.Domain
{
    public class Photo : DomainEntity, IPersistableObject
    {
        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual PhotoCategory PhotoCategory { get; set; }
        [ValidateNonEmpty]
        public virtual string FileUrl { get; set; }
    }
}