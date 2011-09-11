using System;
using System.Security.Principal;
using System.Web;
using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Core.Services
{
    public interface ISessionContext
    {
        User GetCurrentUser();
        object RetrieveSessionObject(Guid sessionKey);
        object RetrieveSessionObject(string sessionKey);
        void AddUpdateSessionItem(SessionItem item);
        void RemoveSessionItem(Guid sessionKey);
        void RemoveSessionItem(string sessionKey);
        string MapPath(string url);
        HttpPostedFile RetrieveUploadedFile();
        SessionItem RetrieveSessionItem(string sessionKey);
        long GetCompanyId();
        long GetUserId();
    }

    public class SessionContext : ISessionContext
    {
        private readonly IRepository _repository;

        public SessionContext(IRepository repository)
        {
            _repository = repository;
        }

        public User GetCurrentUser()
        {
            if(HttpContext.Current == null)
            {
                return null;
            }
            IIdentity identity = HttpContext.Current.User.Identity;
            if (!identity.IsAuthenticated)
            {
                return null;
            }
            var customPrincipal = (CustomPrincipal) HttpContext.Current.User;
            User currentUser = _repository.Find<User>(customPrincipal.UserId);
            return currentUser;
        }

        public object RetrieveSessionObject(Guid sessionKey)
        {
            SessionItem item = (SessionItem)HttpContext.Current.Session[sessionKey.ToString()];
            return item.SessionObject;
        }

        public object RetrieveSessionObject(string sessionKey)
        {
            SessionItem item = (SessionItem)HttpContext.Current.Session[sessionKey];
            return item != null ?item.SessionObject:null;
        }

        public SessionItem RetrieveSessionItem(string sessionKey)
        {
            return (SessionItem)HttpContext.Current.Session[sessionKey];
        }


        public void AddUpdateSessionItem(SessionItem item)
        {
            HttpContext.Current.Session[item.SessionKey] = item;
        }

        public void RemoveSessionItem(Guid sessionKey)
        {
            HttpContext.Current.Session.Remove(sessionKey.ToString());
        }

        public void RemoveSessionItem(string sessionKey)
        {
            HttpContext.Current.Session.Remove(sessionKey);
        }

        public string MapPath(string url)
        {
            return HttpContext.Current.Server.MapPath(url);
        }

        public HttpPostedFile RetrieveUploadedFile()
        {
            return HttpContext.Current.Request.Files.AllKeys.Length > 0 ? HttpContext.Current.Request.Files[0] : null;
        }

        public long GetCompanyId()
        {
            var httpContext = HttpContext.Current;
            var customPrincipal = httpContext != null ? httpContext.User as CustomPrincipal : null;
            return customPrincipal != null ? customPrincipal.CompanyId : 0;
        }
        public long GetUserId()
        {
            var httpContext = HttpContext.Current;
            var customPrincipal = httpContext != null ? httpContext.User as CustomPrincipal : null;
            return customPrincipal != null ? customPrincipal.UserId : 0;
        }
    }

    public class SessionItem
    {
        public DateTime TimeStamp { get; set; }
        public string SessionKey { get; set; }
        public object SessionObject { get; set; }
    }

    public class UserSessionFake : ISessionContext
    {
        private User _currentUser;
        public UserSessionFake(User currentUser)
        {
            _currentUser = currentUser;
        }

        public virtual User GetCurrentUser()
        {
            return _currentUser;
        }

        public bool CheckIfSessionItemIsStale<ENTITY>(Guid sessionKey) where ENTITY : DomainEntity
        {
            throw new NotImplementedException();
        }

        public object RetrieveSessionObject(Guid sessionKey)
        {
            throw new NotImplementedException();
        }

        public object RetrieveSessionObject(string sessionKey)
        {
            throw new NotImplementedException();
        }

        public void AddUpdateSessionItem(SessionItem item)
        {
            throw new NotImplementedException();
        }

        public void RemoveSessionItem(Guid sessionKey)
        {
            throw new NotImplementedException();
        }

        public void RemoveSessionItem(string sessionKey)
        {
            throw new NotImplementedException();
        }

        public string MapPath(string url)
        {
            throw new NotImplementedException();
        }

        public HttpPostedFile RetrieveUploadedFile()
        {
            throw new NotImplementedException();
        }

        public SessionItem RetrieveSessionItem(string sessionKey)
        {
            throw new NotImplementedException();
        }

        public long GetCompanyId()
        {
            throw new NotImplementedException();
        }

        public long GetUserId()
        {
            throw new NotImplementedException();
        }
    }
}