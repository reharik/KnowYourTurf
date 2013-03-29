using CC.Core.CustomAttributes;
using CC.Core.Domain;
using Castle.Components.Validator;

namespace KnowYourTurf.Core.Domain
{
    public class BaseProduct : DomainEntity, IPersistableObject
    {
        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        [ValidateNonEmpty]
        public virtual string InstantiatingType { get; set; }
        [TextArea]
        public virtual string Notes { get; set; }
    }
}