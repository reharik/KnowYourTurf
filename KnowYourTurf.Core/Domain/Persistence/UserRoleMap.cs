namespace KnowYourTurf.Core.Domain.Persistence
{
    public class UserRoleMap : DomainEntityMap<UserRole>
    {
        public UserRoleMap()
        {
            Map(x => x.Name);
            Map(x => x.Description);
        }
    }
}