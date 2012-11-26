using System;
using System.Linq;
using CC.Security.Interfaces;
using CC.Security.Model;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Security
{
    public interface IPermissions
    {
        void CreateControllerPermission(string controllerName, UserType ut, int level = 1);
        void CreateMenuPermission(UserType ut, string token, int level = 1);
        void CreatePermission(UserType ut, string operation, int level = 1);
        void RemovePermissionForController(UserType ut, string operation);
    }
    /// <summary>
    /// 
    /// 
    /// DO NOT MAKE THIS STRONGLY TYPED!
    /// NEED TO PREVENT REFACTORING!
    /// 
    /// 
    /// </summary>
    public class Permissions : IPermissions
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IPermissionsBuilderService _permissionsBuilderService;
        private readonly IPermissionsService _permissionsService;
        private UsersGroup _adminUG;
        private Permission[] _adminPerms;
        private UsersGroup _employeeUG;
        private Permission[] _employeePerms;
        private UsersGroup _facUG;
        private Permission[] _facPerms;

        public Permissions(IAuthorizationRepository authorizationRepository,
            IPermissionsBuilderService permissionsBuilderService,
            IPermissionsService permissionsService)
        {
            _authorizationRepository = authorizationRepository;
            _permissionsBuilderService = permissionsBuilderService;
            _permissionsService = permissionsService;
        }

        
        public bool CheckIfExists(UserType ut, string operation)
        {
            switch (ut.Key)
            {
                case "Administrator":
                    if (_adminPerms == null)
                    {
                        if (_adminUG == null)
                        {
                            _adminUG = _authorizationRepository.GetUsersGroupByName(ut.Key);
                        }
                        _adminPerms = _permissionsService.GetPermissionsFor(_adminUG);
                    }
                    if (_adminPerms.Any(x => x.Operation.Name == operation)) return true;
                    break;
                case "Employee":
                    if (_employeePerms == null)
                    {
                        if (_employeeUG == null)
                        {
                            _employeeUG = _authorizationRepository.GetUsersGroupByName(ut.Key);
                        }
                        _employeePerms = _permissionsService.GetPermissionsFor(_employeeUG);
                    }
                    if (_employeePerms.Any(x => x.Operation.Name == operation)) return true;
                    break;
                case "Facilities":
                    if (_facPerms == null)
                    {
                        if (_facUG == null)
                        {
                            _facUG = _authorizationRepository.GetUsersGroupByName(ut.Key);
                        }
                        _facPerms = _permissionsService.GetPermissionsFor(_facUG);
                    }
                    if (_facPerms.Any(x => x.Operation.Name == operation)) return true;
                    break;
            }
            return false;
        }

        public void CreateControllerPermission(string controllerName, UserType ut, int level = 1)
        {
            if (!CheckIfExists(ut, "/" + controllerName))
                _permissionsBuilderService.Allow("/" + controllerName).For(ut.Key).OnEverything().Level(level).Save();
        }

        public void CreateMenuPermission(UserType ut, string token, int level = 1)
        {
            if (!CheckIfExists(ut, "/MenuItem/" + token))
                _permissionsBuilderService.Allow("/MenuItem/" + token).For(ut.Key).OnEverything().Level(level).Save();
        }

        public void CreatePermission(UserType ut, string operation, int level = 1)
        {
            if (!CheckIfExists(ut, operation))
                _permissionsBuilderService.Allow(operation).For(ut.Key).OnEverything().Level(level).Save();
        }

        public void RemovePermissionForController(UserType ut, string operation)
        {
            //TODO must create method on permissionService that gets permission by somthing other than Id
            throw new NotImplementedException();
//            _permissionsService.GetPermission()
//            _authorizationRepository.RemovePermission();
        }
    }
}