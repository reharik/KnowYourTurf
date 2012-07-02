using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class EmailJobMap : DomainEntityMap<EmailJob>
    {
        public EmailJobMap()
        {
            Map(x => x.Name);
            Map(x => x.Subject);
            Map(x => x.Sender);
            Map(x => x.Description);
            Map(x => x.Frequency);
            Map(x => x.Status);
            References(x => x.ReadOnlyEmailTemplate).Access.CamelCaseField(Prefix.Underscore).LazyLoad();
            References(x => x.EmailJobType);
            HasManyToMany(x => x.Subscribers).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
        }
    }
}