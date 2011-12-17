namespace KnowYourTurf.Core.Domain
{
    public class Company:DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}