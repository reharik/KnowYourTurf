using System;
using System.Web;
using KnowYourTurf.Core.Config;

namespace KnowYourTurf.Core.Services
{
    public interface IGetCompanyIdService
    {
        int Execute();
        int CompanyId { get; set; }
    }

    public class GetCompanyIdService : IGetCompanyIdService
    {
        public int CompanyId { get; set; }
        public int Execute()
        {
            var httpContext = HttpContext.Current;
            var customPrincipal = httpContext!=null ? httpContext.User as CustomPrincipal:null;
            return customPrincipal !=null ? customPrincipal.CompanyId:0;
        }
    }

    // this of course is stupid and should be on the sessioncontext
    // so if you're reading this refactor it.
    public class DataLoaderGetCompanyIdService:IGetCompanyIdService
    {
        public int CompanyId { get; set; }
        public int Execute()
        {
            return CompanyId;
        }
    }
}