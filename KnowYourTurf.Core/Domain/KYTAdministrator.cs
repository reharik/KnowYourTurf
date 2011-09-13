using Castle.Components.Validator;

namespace KnowYourTurf.Core.Domain
{
    public class KYTAdministrator : User
    {
        [ValidateNonEmpty]
        public virtual string KYTAdministratorId { get; set; }
    }
}