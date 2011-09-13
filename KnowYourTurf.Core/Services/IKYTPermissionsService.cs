using KnowYourTurf.Core.Enums;
using Rhino.Security.Interfaces;

namespace KnowYourTurf.Core.Services
{
    public interface IKYTPermissionsService
    {
        void GrantDefaultKYTAdminPermissions();
        void GrantDefaultAdminPermissions(string type);
        void GrantDefaultEmployeePermissions();
        void GrantDefaultFacilitiesPermissions();
    }

    public class KYTPermissionsService : IKYTPermissionsService
    {
        private readonly IPermissionsBuilderService _permissionsBuilderService;
        private readonly IAuthorizationRepository _authorizationRepository;

        public KYTPermissionsService(IPermissionsBuilderService permissionsBuilderService, IAuthorizationRepository authorizationRepository)
        {
            _permissionsBuilderService = permissionsBuilderService;
            _authorizationRepository = authorizationRepository;
        }

        public void GrantDefaultKYTAdminPermissions()
        {
            _permissionsBuilderService
                    .Allow("/KYTAdmin")
                    .For(UserRole.KYTAdmin.Key)
                    .OnEverything()
                    .Level(10)
                    .Save();
            GrantDefaultAdminPermissions(UserRole.KYTAdmin.Key);
        }

        public void GrantDefaultAdminPermissions(string type)
        {
            var operations = ((CustomAuthorizationRepository)_authorizationRepository).GetAllOperations();
            foreach (var operation in operations)
            {
                if(operation.Name != "/KYTAdmin")
                _permissionsBuilderService
                    .Allow(operation)
                    .For(type)
                    .OnEverything()
                    .Level(10)
                    .Save();
            }
        }

        public void GrantDefaultEmployeePermissions()
        {
            _permissionsBuilderService.Allow("/HomeController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/CalculatorController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/CalculatorListController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/EmployeeDashboardController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/EmployeeController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/EquipmentController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/EquipmentListController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/EventController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/EventCalendarController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/FieldController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/FieldDashboardController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/FieldListController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/ForumController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/InventoryListController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/PurchaseOrderCommitController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/PurchaseOrderController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/PurchaseOrderLineItemController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/PurchaseOrderListController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/TaskController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/TaskCalendarController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/TaskListController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/VendorContactController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/VendorContactListController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/VendorController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/VendorListController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/WeatherController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/WeatherListController").For(UserRole.Employee.Key).OnEverything().Level(1).Save();

            _permissionsBuilderService.Allow("/MenuItem/Home").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Vendors").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Inventory").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Fields").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Equipment").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Tasks").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/PurchaseOrders").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Events").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Calculators").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Weather").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Forum").For(UserRole.Employee.Key).OnEverything().Level(1).Save();
        }

        public void GrantDefaultFacilitiesPermissions()
        {
            _permissionsBuilderService
                .Allow("/EventController")
                .For(UserRole.Facilities.Key)
                .OnEverything()
                .Level(1)
                .Save();
            _permissionsBuilderService
                .Allow("/EventCalendarController")
                .For(UserRole.Facilities.Key)
                .OnEverything()
                .Level(1)
                .Save();
            _permissionsBuilderService.Allow("/MenuItem/Events").For(UserRole.Employee.Key).OnEverything().Level(1).Save();

        }
    }
}