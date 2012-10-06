using CC.Core.Domain;

namespace KnowYourTurf.Core.Domain
{

    public abstract class DomainEntity : Entity
    {
        public virtual long CompanyId { get; set; }
    }

}