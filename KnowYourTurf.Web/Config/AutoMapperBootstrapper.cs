using AutoMapper;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Config
{
    public class AutoMapperBootstrapper
    {
        public virtual void BootstrapAutoMapper()
        {
            Mapper.CreateMap<User, EmployeeDashboardViewModel>();
             
        }

        public static void Bootstrap()
        {
            new AutoMapperBootstrapper().BootstrapAutoMapper();
        }
    }
}