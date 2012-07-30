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
            Mapper.CreateMap<Field, FieldViewModel>();
            Mapper.CreateMap<Task, TaskViewModel>().ForMember(d => d.Equipment, o => o.Ignore()).ForMember(d => d.Employees, o => o.Ignore());

        }

        public static void Bootstrap()
        {
            new AutoMapperBootstrapper().BootstrapAutoMapper();
        }
    }
}