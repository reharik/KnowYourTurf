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
            Mapper.CreateMap<EmailJob, EmailJobViewModel>().ForMember(d => d.Subscribers, o => o.Ignore());
            Mapper.CreateMap<EventType, EventTypeViewModel>();
            Mapper.CreateMap<TaskType, TaskTypeViewModel>();
            Mapper.CreateMap<PhotoCategory, ListTypeViewModel>();
            Mapper.CreateMap<DocumentCategory, ListTypeViewModel>();
        
        }

        public static void Bootstrap()
        {
            new AutoMapperBootstrapper().BootstrapAutoMapper();
        }
    }
}