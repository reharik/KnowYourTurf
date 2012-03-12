using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core.Domain
{
    public class EmailJobType:DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        [ValueOf(typeof(Status))]
        public virtual string Status { get; set; }
    }
}