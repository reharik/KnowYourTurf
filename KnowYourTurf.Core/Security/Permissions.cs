using System;
using CC.Security.Interfaces;
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

        public Permissions(IAuthorizationRepository authorizationRepository,
            IPermissionsBuilderService permissionsBuilderService,
            IPermissionsService permissionsService)
        {
            _authorizationRepository = authorizationRepository;
            _permissionsBuilderService = permissionsBuilderService;
//            _permissionsService = permissionsService;
        }

        

        public void CreateControllerPermission(string controllerName, UserType ut, int level = 1)
        {
            _permissionsBuilderService.Allow("/" + controllerName).For(ut.Key).OnEverything().Level(level).Save();
        }

        public void CreateMenuPermission(UserType ut, string token, int level = 1)
        {
            _permissionsBuilderService.Allow("/MenuItem/" + token).For(ut.Key).OnEverything().Level(level).Save();
        }

        public void CreatePermission(UserType ut, string operation, int level = 1)
        {
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