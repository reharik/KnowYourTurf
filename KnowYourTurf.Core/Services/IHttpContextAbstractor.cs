using System;
using System.Web;
using KnowYourTurf.Core.Config;

namespace KnowYourTurf.Core.Services
{
    public interface IHttpContextAbstractor
    {
        long GetUserIdFromIdentity();
        long GetCompanyIdFromIdentity();
    }

    public class HttpContextAbstractor : IHttpContextAbstractor
    {
        public long GetUserIdFromIdentity()
        {
            var httpContext = HttpContext.Current;
            var customPrincipal = httpContext != null ? httpContext.User as CustomPrincipal : null;
            return customPrincipal != null ? customPrincipal.UserId : 0;
        }

        public long GetCompanyIdFromIdentity()
        {
            var httpContext = HttpContext.Current;
            var customPrincipal = httpContext != null ? httpContext.User as CustomPrincipal : null;
            return customPrincipal != null ? customPrincipal.CompanyId : 0;
        }
    }
}