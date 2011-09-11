using Castle.Components.Validator;

namespace KnowYourTurf.Core.Domain
{
    public class Equipment : DomainEntity
    {
        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Vendor { get; set; }
        [ValidateNonEmpty]
        [ValidateDecimal]
        public virtual double TotalHours { get; set; }
        public virtual string ImageUrl { get; set; }
    }
}