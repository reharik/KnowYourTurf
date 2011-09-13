using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public sealed class UserPersistenceMap : DomainEntityMap<User>
    {
        public UserPersistenceMap()
        {
            Map(u => u.LoginName);
            Map(u => u.Password);
            Map(u => u.Status);

            Map(u => u.FirstName);
            Map(u => u.LastName);
            Map(u => u.Email);
            Map(u => u.PhoneMobile);
            Map(u => u.PhoneHome);
            Map(u => u.Address1);
            Map(u => u.Address2);
            Map(u => u.City);
            Map(u => u.State);
            Map(u => u.ZipCode);
            Map(c => c.BirthDate);
            Map(c => c.Notes);
            Map(c => c.UserRoles);
            Map(c => c.LanguageDefault);
            Map(x => x.ImageUrl);
            References(x => x.Company);
            HasManyToMany(x => x.GetEmailTemplates()).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
        }


        public class EmployeeMap : SubclassMap<Employee>
        {
            public EmployeeMap()
            {
                Map(u => u.EmployeeId);
                Map(x => x.EmployeeType);
                Map(x => x.EmergencyContact);
                Map(x => x.EmergencyContactPhone);
                HasManyToMany(x => x.GetTasks()).Table("EmployeeToTask").Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
            }
        }

        public class FacilitiesMap : SubclassMap<Facilities>
        {
            public FacilitiesMap()
            {
                Map(u => u.FacilitiesId);
            }
        }

        public class MultiTenantUserMap : SubclassMap<MultiTenantUser>
        {
            public MultiTenantUserMap()
            {
                Map(u => u.MultiTenantUserId);
            }
        }

        public class KYTAdministratorMap : SubclassMap<KYTAdministrator>
        {
            public KYTAdministratorMap()
            {
                Map(u => u.KYTAdministratorId);
            }
        }
    }
}