using Castle.Components.Validator;

namespace KnowYourTurf.Core.Domain
{
    public class Administrator : User
    {
        [ValidateNonEmpty]
        public virtual string AdminId { get; set; }
        public virtual bool IsAnEmployee { get; set; }

    }
}