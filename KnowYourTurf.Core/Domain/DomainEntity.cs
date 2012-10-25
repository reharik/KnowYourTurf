using CC.Core.Domain;

namespace KnowYourTurf.Core.Domain
{
    public abstract class DomainEntity : Entity
    {
        public virtual int CompanyId { get; set; }
    }

}