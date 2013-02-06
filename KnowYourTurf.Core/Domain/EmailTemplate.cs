using CC.Core.Domain;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using CC.Core;

namespace KnowYourTurf.Core.Domain
{
    public class EmailTemplate:DomainEntity, IPersistableObject
    {
        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        public virtual string FriendlyName { get { return Name.ToSeperateWordsFromPascalCase(); } }
        public virtual string Description { get; set; }
        [TextArea]
        public virtual string Template { get; set; }
    }
}