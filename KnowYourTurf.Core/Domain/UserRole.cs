using FluentNHibernate.Data;

namespace KnowYourTurf.Core.Domain
{
    public class UserRole:DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}