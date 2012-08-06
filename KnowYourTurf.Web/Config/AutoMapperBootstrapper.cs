using AutoMapper;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Controllers;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Config
{
    public class AutoMapperBootstrapper
    {
        public virtual void BootstrapAutoMapper()
        {
            Mapper.CreateMap<User, UserViewModel>().ForMember(d => d.UserRoles, o => o.Ignore());
            Mapper.CreateMap<Field, FieldViewModel>();
            Mapper.CreateMap<Photo, PhotoDto>();
            Mapper.CreateMap<Task, TaskViewModel>().ForMember(d => d.Equipment, o => o.Ignore()).ForMember(d => d.Employees, o => o.Ignore());
            Mapper.CreateMap<Equipment, EquipmentViewModel>();
        }

        public static void Bootstrap()
        {
            new AutoMapperBootstrapper().BootstrapAutoMapper();
        }
    }
}