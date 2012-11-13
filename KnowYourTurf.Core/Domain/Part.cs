using CC.Core.CustomAttributes;
using Castle.Components.Validator;

namespace KnowYourTurf.Core.Domain
{
    public class Part:DomainEntity
    {
        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        [TextArea]
        public virtual string Description { get; set; }
    }
}