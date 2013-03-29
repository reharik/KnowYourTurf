using System;
using System.Web;
using KnowYourTurf.Core.Config;

namespace KnowYourTurf.Core.Services
{
    public interface IGetClientIdService
    {
        int Execute();
        int ClientId { get; set; }
    }

    public class GetClientIdService : IGetClientIdService
    {
        public int ClientId { get; set; }
        public int Execute()
        {
            var httpContext = HttpContext.Current;
            var customPrincipal = httpContext!=null ? httpContext.User as CustomPrincipal:null;
            return customPrincipal !=null ? customPrincipal.ClientId:0;
        }
    }

    // this of course is stupid and should be on the sessioncontext
    // so if you're reading this refactor it.
    public class DataLoaderGetClientIdService:IGetClientIdService
    {
        public int ClientId { get; set; }
        public int Execute()
        {
            return ClientId;
        }
    }
}