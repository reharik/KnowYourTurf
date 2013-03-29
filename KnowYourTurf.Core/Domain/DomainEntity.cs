using CC.Core.Domain;

namespace KnowYourTurf.Core.Domain
{
    public abstract class DomainEntity : Entity
    {
        public virtual int ClientId { get; set; }
    }

}