using Castle.Components.Validator;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;

namespace KnowYourTurf.Core.Domain
{
    public class EmailTemplate:DomainEntity
    {
        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        [TextArea]
        public virtual string Template { get; set; }
    }
}