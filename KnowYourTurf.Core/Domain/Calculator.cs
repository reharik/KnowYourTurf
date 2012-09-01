namespace KnowYourTurf.Core.Domain
{
    public class Calculator : DomainEntity, IPersistableObject
    {
        public virtual string Name { get; set; }
    }
}