using AutoMapper;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Config
{
    public class AutoMapperBootstrapper
    {
        public virtual void BootstrapAutoMapper()
        {
            Mapper.CreateMap<User, UserViewModel>().ForMember(d => d.UserRoles, o => o.Ignore());
             
        }

        public static void Bootstrap()
        {
            new AutoMapperBootstrapper().BootstrapAutoMapper();
        }
    }
}