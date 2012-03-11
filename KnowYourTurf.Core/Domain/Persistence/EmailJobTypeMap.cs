using KnowYourTurf.Core.Config;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class EmailJobTypeMap : DomainEntityMap<EmailJobType>
    {
        public EmailJobTypeMap()
        {
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Status);
            ApplyFilter<StatusConditionFilter>("(Status = :condition)");
        }
    }
}