using System.Linq;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Core.Services
{
    public interface ISecurityDataService
    {
        User AuthenticateForUserId(string username, string password);
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
            var unitOfWork = _repository.UnitOfWork;
            unitOfWork.CurrentSession.DisableFilter("CompanyConditionFilter");
            var user = _repository.Query<User>(u => u.UserLoginInfo.LoginName.ToLowerInvariant() == username && u.UserLoginInfo.Password == password).FirstOrDefault();
            return user;
        }
    }
}