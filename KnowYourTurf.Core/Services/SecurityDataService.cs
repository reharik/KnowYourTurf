using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Security;
using CC.Core;
using CC.Core.DomainTools;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Core.Services
{
    public interface ISecurityDataService
    {
        User AuthenticateForUserId(string username, string password);
        string CreateSalt();
        string CreatePasswordHash(string pwd, string salt);
    }

    public class SecurityDataService : ISecurityDataService
    {
        private readonly IRepository _repository;

        public SecurityDataService(IRepository repository)
        {
            _repository = repository;
        }

        public User AuthenticateForUserId(string username, string password)
        {
            _repository.CurrentSession().DisableFilter("ClientConditionFilter");
            var users = _repository.Query<User>(u => u.UserLoginInfo.LoginName.ToLowerInvariant() == username.ToLowerInvariant());// && u.UserLoginInfo.Password == password).FirstOrDefault();
            User ValidUser = null;
            users.ForEachItem(x =>
            {
                var passwordHash = CreatePasswordHash(password, x.UserLoginInfo.Salt);
                if (x.UserLoginInfo.Password == passwordHash)
                {
                    ValidUser = x;
                }
            });
            return ValidUser;
        }
        public string CreateSalt()
        {
            //Generate a cryptographic random number.
            var rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[16];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        public string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd =
                FormsAuthentication.HashPasswordForStoringInConfigFile(
                    saltAndPwd, "sha1");
            return hashedPwd;
        }
    }
}                            