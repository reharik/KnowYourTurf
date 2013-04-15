using System;
using System.Web;
using System.Web.Security;
using CC.Core.DomainTools;
using KnowYourTurf.Core.Domain;
using NHibernate;
using StructureMap;

namespace KnowYourTurf.Web.Services
{
    public interface IAuthenticationContext
    {
        string ThisUserHasBeenAuthenticated(User username, bool rememberMe, User impersonator = null);
        void SignOut();
    }

    public class WebAuthenticationContext : IAuthenticationContext
    {
        public string ThisUserHasBeenAuthenticated(User user, bool rememberMe, User impersonator = null)
        {
            string userData = String.Empty;
            userData = userData + "UserId=" + user.EntityId + "|ClientId=" + user.ClientId;
            var ticket = new FormsAuthenticationTicket(1, user.FullNameLNF, DateTime.Now, DateTime.Now.AddMinutes(30),
                                                       rememberMe, userData);
            string encTicket = FormsAuthentication.Encrypt(ticket);
            var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            HttpContext.Current.Response.Cookies.Add(faCookie);
            logUserStats(user, impersonator);
            return FormsAuthentication.GetRedirectUrl(user.FullNameLNF, false);
        }

        private void logUserStats(User user, User impersonator = null)
        {
            var sf = ObjectFactory.GetInstance<ISessionFactory>();
            var session = sf.OpenSession();
            var unitOfWork = new UnitOfWork(session);
            var repository = new Repository(unitOfWork, null);
            var request = HttpContext.Current.Request;
            var loginStatistics = new LoginStatistics
            {
                User = user,
                BrowserType = request.Browser.Browser,
                BrowserVersion = request.Browser.Version,
                UserAgent = request.UserAgent,
                UserHostAddress = request.UserHostAddress,
                UserHostName = request.UserHostName,
                CreatedDate = DateTime.Now,
                ChangedDate = DateTime.Now,
                CreatedBy = impersonator ?? user,
                ChangedBy = impersonator ?? user,
                ClientId = user.ClientId
            };
            repository.Save(loginStatistics);
            repository.Commit();
        }


        public void SignOut()
        {
            SignOutFunc();
        }

        public Action<string, bool> SetAuthCookieFunc = FormsAuthentication.SetAuthCookie;
        public Action SignOutFunc = FormsAuthentication.SignOut;
    }
}