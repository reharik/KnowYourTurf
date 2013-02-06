using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class EmailTemplateMap : DomainEntityMap<EmailTemplate>
    {
        public EmailTemplateMap()
        {
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Template);
        }
    }
}