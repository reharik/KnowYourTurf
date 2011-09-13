using Castle.Components.Validator;

namespace KnowYourTurf.Core.Domain
{
    public class MultiTenantUser : User
    {
        [ValidateNonEmpty]
        public virtual string MultiTenantUserId { get; set; }
    }
}