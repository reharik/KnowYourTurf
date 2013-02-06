using CC.Core.Domain;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;

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