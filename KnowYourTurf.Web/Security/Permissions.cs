using System;
using CC.Security.Interfaces;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Security
{
    public interface IPermissions
    {
        void GrantAdminPermissions();
        void GrantFacilitiesPermissions();
        void GrantEmployeePermissions();
        void CreateControllerPermission(string controllerName, UserType ut);
        void CreateMenuPermission(UserType ut, string token);
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
            _permissionsService = permissionsService;
        }

        public void GrantAdminPermissions()
        {
            var operations = ((CustomAuthorizationRepository)_authorizationRepository).GetAllOperations();
            foreach (var operation in operations)
            {
                CreatePermission(UserType.Administrator, operation.Name, 10);
            }
        }

        public void GrantFacilitiesPermissions()
        {
            CreateControllerPermission("EventController",UserType.Facilities);
            CreateControllerPermission("EventCalendarController",UserType.Facilities);
            CreateControllerPermission("KnowYourTurfController",UserType.Facilities);
            CreateControllerPermission("OrthogonalController",UserType.Facilities);

            CreateMenuPermission(UserType.Employee, "Events");
        }

        public void GrantEmployeePermissions()
        {
            CreateControllerPermission("KnowYourTurfController",UserType.Employee);
            CreateControllerPermission("OrthogonalController",UserType.Employee);
            CreateControllerPermission("CalculatorController",UserType.Employee);
            CreateControllerPermission("CalculatorListController",UserType.Employee);
            CreateControllerPermission("EmployeeDashboardController",UserType.Employee);
            CreateControllerPermission("EmployeeController",UserType.Employee);
            CreateControllerPermission("EquipmentController",UserType.Employee);
            CreateControllerPermission("EquipmentListController",UserType.Employee);
            CreateControllerPermission("EventController",UserType.Employee);
            CreateControllerPermission("EventCalendarController",UserType.Employee);
            CreateControllerPermission("FieldController",UserType.Employee);
            CreateControllerPermission("FieldDashboardController",UserType.Employee);
            CreateControllerPermission("FieldListController",UserType.Employee);
            CreateControllerPermission("ForumController",UserType.Employee);
            CreateControllerPermission("InventoryListController",UserType.Employee);
            CreateControllerPermission("PhotoController",UserType.Employee);
            CreateControllerPermission("DocumentController",UserType.Employee);
            CreateControllerPermission("PurchaseOrderCommitController",UserType.Employee);
            CreateControllerPermission("PurchaseOrderController",UserType.Employee);
            CreateControllerPermission("PurchaseOrderLineItemController",UserType.Employee);
            CreateControllerPermission("PurchaseOrderListController",UserType.Employee);
            CreateControllerPermission("TaskController",UserType.Employee);
            CreateControllerPermission("TaskCalendarController",UserType.Employee);
            CreateControllerPermission("TaskListController",UserType.Employee);
            CreateControllerPermission("VendorContactController",UserType.Employee);
            CreateControllerPermission("VendorContactListController",UserType.Employee);
            CreateControllerPermission("VendorController",UserType.Employee);
            CreateControllerPermission("VendorListController",UserType.Employee);
            CreateControllerPermission("WeatherController",UserType.Employee);
            CreateControllerPermission("WeatherListController",UserType.Employee);

            CreateMenuPermission(UserType.Employee, "Home");
            CreateMenuPermission(UserType.Employee, "Fields");
            CreateMenuPermission(UserType.Employee, "Equipment");
            CreateMenuPermission(UserType.Employee, "Tasks");
            CreateMenuPermission(UserType.Employee, "Events");
            CreateMenuPermission(UserType.Employee, "Calculators");
            CreateMenuPermission(UserType.Employee, "Weather");
            CreateMenuPermission(UserType.Employee, "Forum");
        }

        public void CreateControllerPermission(string controllerName, UserType ut)
        {
            _permissionsBuilderService.Allow("/" + controllerName).For(ut.Key).OnEverything().Level(1).Save();
        }

        public void CreateMenuPermission(UserType ut, string token)
        {
            _permissionsBuilderService.Allow("/MenuItem/" + token).For(ut.Key).OnEverything().Level(1).Save();
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