using Castle.Components.Validator;

namespace KnowYourTurf.Core.Domain
{
    public class Facilities : User
    {
        [ValidateNonEmpty]
        public virtual string FacilitiesId { get; set; }
    }
}