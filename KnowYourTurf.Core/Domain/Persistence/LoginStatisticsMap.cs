namespace KnowYourTurf.Core.Domain.Persistence
{
    public class LoginStatisticsMap : DomainEntityMap<LoginStatistics>
    {
        public LoginStatisticsMap()
        {
            Map(x => x.BrowserType);
            Map(x => x.BrowserVersion);
            Map(x => x.UserAgent);
            Map(x => x.UserHostName);
            Map(x => x.UserHostAddress);
            References(x => x.User);
        }
    }
}