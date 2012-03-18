using System.Security.Principal;
using System.Web.Security;

namespace KnowYourTurf.Core.Config
{
    public class CustomIdentitiy : IIdentity
    {
        private readonly FormsAuthenticationTicket _authTicket;

        public CustomIdentitiy(FormsAuthenticationTicket authTicket)
        {
            _authTicket = authTicket;
        }

        public string Name
        {
            get { return _authTicket.Name; }
        }

        public string AuthenticationType
        {
            get { return "Forms Authentication"; }
        }

        public bool IsAuthenticated
        {
            get { return !_authTicket.Expired; }
        }
    }
}