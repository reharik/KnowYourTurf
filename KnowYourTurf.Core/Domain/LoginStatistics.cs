using CC.Core.Domain;

namespace KnowYourTurf.Core.Domain
{
    public class LoginStatistics : DomainEntity, IPersistableObject
    {
        public virtual User User { get; set; }
        public virtual string BrowserType { get; set; }
        public virtual string BrowserVersion { get; set; }
        public virtual string UserAgent { get; set; }
        public virtual string UserHostName { get; set; }
        public virtual string UserHostAddress { get; set; }
    }
}