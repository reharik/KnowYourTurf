namespace KnowYourTurf.Core.Domain
{
    public class Company : DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual string Longitude { get; set; }
        public virtual string Latitude { get; set; }
        public virtual double TaxRate { get; set; }
    }
}