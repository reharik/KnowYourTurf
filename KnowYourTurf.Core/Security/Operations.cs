using System.Collections.Generic;
using CC.Security.Interfaces;
using CC.Security.Model;
using System.Linq;

namespace KnowYourTurf.Web.Security
{
    public interface IOperations
    {
        void CreateOperationForControllerType(string controllerName);
        void CreateOperationForMenuItem(string menuItemName);
        void CreateOperation(string operation);
        void RemoveOperationForControllerType(string controllerName);
        void RemoveOperationForMenuItem(string menuItemName);
        void RemoveOperation(string operation);
    }
    /// <summary>
    /// 
    /// 
    /// DO NOT MAKE THIS STRONGLY TYPED!
    /// NEED TO PREVENT REFACTORING!
    /// 
    /// 
    /// </summary>
    public class Operations : IOperations
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        private IList<Operation> _allOperations;

        public Operations(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        public void CreateOperationForControllerType(string controllerName) 
        {
            if(_allOperations==null) _allOperations = _authorizationRepository.GetAllOperations();

            if (!_allOperations.Any(x => x.Name == "/" + controllerName))
            {
                _authorizationRepository.CreateOperation("/" + controllerName);
            }
        }

        public void CreateOperationForMenuItem(string menuItemName)
        {
            if (_allOperations == null) _allOperations = _authorizationRepository.GetAllOperations();
            if (!_allOperations.Any(x => x.Name == "/MenuItem/" + menuItemName))
            {
                _authorizationRepository.CreateOperation("/MenuItem/" + menuItemName);
            }
        }

        public void CreateOperation(string operation)
        {
            if (_allOperations == null) _allOperations = _authorizationRepository.GetAllOperations();
            if (!_allOperations.Any(x => x.Name == operation))
            {
                _authorizationRepository.CreateOperation(operation);
            }
        }

        public void RemoveOperationForControllerType(string controllerName) 
        {
            _authorizationRepository.RemoveOperation("/" + controllerName);
        }

        public void RemoveOperationForMenuItem(string menuItemName)
        {
            _authorizationRepository.RemoveOperation("/MenuItem/" + menuItemName);
        }

        public void RemoveOperation(string operation)
        {
            _authorizationRepository.RemoveOperation(operation);
        }
    }
}