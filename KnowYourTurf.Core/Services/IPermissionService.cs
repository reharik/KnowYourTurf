using KnowYourTurf.Core.Domain;
using Rhino.Security.Interfaces;
using StructureMap;

namespace KnowYourTurf.Core.Services
{
    public interface IUserPermissionService
    {
        bool IsAllowed(string operationName);
    }

    public class UserPermissionService : IUserPermissionService
    {
        private readonly ISessionContext _sessionContext;
        private readonly IRepository _repository;
        private readonly IAuthorizationService _authorizationService;

        public UserPermissionService(ISessionContext sessionContext, IRepository repository, IAuthorizationService authorizationService)
        {
            _sessionContext = sessionContext;
            _repository = repository;
            _authorizationService = authorizationService;
        }

        public bool IsAllowed(string operationName)
        {
            var userEntityId = _sessionContext.GetUserEntityId();
            var user = _repository.Find<User>(userEntityId);
            return _authorizationService.IsAllowed(user, operationName);
        }

        public static bool Allow(string operationName)
        {
            var sessionContext = ObjectFactory.Container.GetInstance<SessionContext>();
            var userEntityId = sessionContext.GetUserEntityId();
            var repository = ObjectFactory.Container.GetInstance<IRepository>();
            var user = repository.Find<User>(userEntityId);
            var authorizationService = ObjectFactory.Container.GetInstance<IAuthorizationService>();
            return authorizationService.IsAllowed(user, operationName);
        }
    }
}