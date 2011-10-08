using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Config;
using KnowYourTurf.Web.Controllers;
using Rhino.Security.Interfaces;
using StructureMap;

namespace KnowYourTurf.Web.Services
{
    public interface ISecuritySetupService
    {
        void ExecuteAll();
        void CreateOperationsForAllMenuItems();
        void AssociateAllUsersWithThierTypeGroup();
        void CreateUserGroups();
        void CreateAdminPermissions();
    }

    public class DefaultSecuritySetupService:ISecuritySetupService
    {
        private static readonly List<string> Operations = new List<string>();
        private readonly IContainer _container;
        private readonly IRepository _repository;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IPermissionsBuilderService _permissionsBuilderService;
        private readonly IKYTPermissionsService _permissionsService;

        public DefaultSecuritySetupService(IContainer container,
            IAuthorizationRepository authorizationRepository,
            IPermissionsBuilderService permissionsBuilderService,
            IKYTPermissionsService permissionsService)
        {
            _container = container;
            _repository = new Repository();
            _authorizationRepository = authorizationRepository;
            _permissionsBuilderService = permissionsBuilderService;
            _permissionsService = permissionsService;
        }

        public void ExecuteAll()
        {
            CreateUserGroups();
            AssociateAllUsersWithThierTypeGroup();
            CreateKYTAdminOperation();
            CreateOperationsForAllControllers();
            CreateOperationsForAllMenuItems();
            CreateAdminPermissions();
            _permissionsService.GrantDefaultAdminPermissions(UserRole.Administrator.Key);
            _permissionsService.GrantDefaultFacilitiesPermissions();
            _permissionsService.GrantDefaultEmployeePermissions();
            _permissionsService.GrantDefaultKYTAdminPermissions();
            CreateFacilitiesPermissions();
            _repository.UnitOfWork.Commit();
        }

        private void CreateKYTAdminOperation()
        {
            _authorizationRepository.CreateOperation("/AdminOrGreater");
            _authorizationRepository.CreateOperation("/KYTAdmin");
        }

        private void CreateOperationsForAllControllers()
        {
            foreach (Type controllerType in typeof (KYTController)
                .Assembly
                .GetTypes()
                .Where(x => (typeof (Controller).IsAssignableFrom(x)) && !x.IsAbstract))
            {
                //foreach ( var method in controllerType
                //    .GetMethods()
                //    .Where(x => (typeof (ActionResult).IsAssignableFrom(x.ReturnType)) && !x.IsDefined(typeof (NonActionAttribute), true)))
                //{
                    var operation = string.Format("/{0}", controllerType.Name);
                    if (!Operations.Contains(operation))
                    {
                        Operations.Add(operation);
                        if (_authorizationRepository.GetOperationByName(operation) == null)
                            _authorizationRepository.CreateOperation(operation);
                    }
                //}
            }
        }

        public void CreateOperationsForAllMenuItems()
        {


            var menuConfig = _container.GetAllInstances<IMenuConfig>();
            menuConfig.Each(x =>
            {
                var menuItems = x.Build(true);
                menuItems.Each(m =>
                {
                    var operation = "/MenuItem/" + m.Text.RemoveWhiteSpace();
                    if (!Operations.Contains(operation))
                    {
                        Operations.Add(operation);
                        if (_authorizationRepository    .GetOperationByName(operation) == null)
                            _authorizationRepository.CreateOperation(operation);
                    }
                });
            });
        }

        public void AssociateAllUsersWithThierTypeGroup()
        {
            var admins = _repository.Query<Employee>(x => x.UserRoles.Contains(UserRole.Administrator.ToString()));
            admins.Each(x => _authorizationRepository.AssociateUserWith(x, UserRole.Administrator.Key));
            var employees = _repository.FindAll<Employee>();
            employees.Each(x => _authorizationRepository.AssociateUserWith(x, UserRole.Employee.Key));
            
            var facilities = _repository.FindAll<Facilities>();
            facilities.Each(x => _authorizationRepository.AssociateUserWith(x, UserRole.Facilities.Key));
            var multiTenantUsers = _repository.FindAll<MultiTenantUser>();
            multiTenantUsers.Each(x => _authorizationRepository.AssociateUserWith(x, UserRole.MultiTenant.Key));
            var kytAdministrators = _repository.FindAll<KYTAdministrator>();
            kytAdministrators.Each(x =>
                                       {
                                           _authorizationRepository.AssociateUserWith(x, UserRole.KYTAdmin.Key);
                                           _authorizationRepository.AssociateUserWith(x, UserRole.Administrator.Key);
                                       });
        }

        public void CreateUserGroups()
        {
            if (_authorizationRepository.GetUsersGroupByName(UserRole.Administrator.Key) == null)
            {
                _authorizationRepository.CreateUsersGroup(UserRole.Administrator.Key);
            }
            if (_authorizationRepository.GetUsersGroupByName(UserRole.Employee.Key) == null)
            {
                _authorizationRepository.CreateUsersGroup(UserRole.Employee.Key);
            }
            if (_authorizationRepository.GetUsersGroupByName(UserRole.Facilities.Key) == null)
            {
                _authorizationRepository.CreateUsersGroup(UserRole.Facilities.Key);
            }
            if (_authorizationRepository.GetUsersGroupByName(UserRole.MultiTenant.Key) == null)
            {
                _authorizationRepository.CreateUsersGroup(UserRole.MultiTenant.Key);
            }
            if (_authorizationRepository.GetUsersGroupByName(UserRole.KYTAdmin.Key) == null)
            {
                _authorizationRepository.CreateUsersGroup(UserRole.KYTAdmin.Key);
            }
        }

        public void CreateAdminPermissions()
        {
           
        }

        public void CreateFacilitiesPermissions()
        {
            

        }
    }
}