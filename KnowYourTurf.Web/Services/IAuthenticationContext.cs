using System;
using System.Web;
using System.Web.Security;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Web.Controllers
{
    public interface IAuthenticationContext
    {
        string ThisUserHasBeenAuthenticated(User username, bool rememberMe);
        void SignOut();
    }

    public class WebAuthenticationContext : IAuthenticationContext
    {
        public string ThisUserHasBeenAuthenticated(User user, bool rememberMe)
        {
            string userData = String.Empty;
            userData = userData + "UserId=" + user.EntityId + "|CompanyId=" + user.Company.EntityId;
            var ticket = new FormsAuthenticationTicket(1, user.FullName, DateTime.Now, DateTime.Now.AddMinutes(30), rememberMe, userData);
            string encTicket = FormsAuthentication.Encrypt(ticket);
            var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            HttpContext.Current.Response.Cookies.Add(faCookie);
            return FormsAuthentication.GetRedirectUrl(user.FullName, false);

        }

        public void SignOut()
        {
            SignOutFunc();
        }

        public Action<string, bool> SetAuthCookieFunc = FormsAuthentication.SetAuthCookie;
        public Action SignOutFunc = FormsAuthentication.SignOut;
    }
}