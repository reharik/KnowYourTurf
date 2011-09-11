using Castle.Components.Validator;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;

namespace KnowYourTurf.Core.Domain
{
    public class BaseProduct : DomainEntity
    {
        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        [ValidateNonEmpty]
        public virtual string InstantiatingType { get; set; }
        [TextArea(3,20)]
        public virtual string Notes { get; set; }
    }
}