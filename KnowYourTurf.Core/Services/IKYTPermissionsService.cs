using CC.Security.Interfaces;
using KnowYourTurf.Core.Enums;

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
                    .For(UserType.KYTAdmin.Key)
                    .OnEverything()
                    .Level(10)
                    .Save();
            GrantDefaultAdminPermissions(UserType.KYTAdmin.Key);
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
            _permissionsBuilderService.Allow("/SystemSupportController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/OrthogonalController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/CalculatorController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/CalculatorListController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/EmployeeDashboardController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/EmployeeController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/EquipmentController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/EquipmentListController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/EventController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/EventCalendarController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/FieldController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/FieldDashboardController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/FieldListController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/ForumController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/InventoryListController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/PhotoController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/DocumentController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/PurchaseOrderCommitController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/PurchaseOrderController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/PurchaseOrderLineItemController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/PurchaseOrderListController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/TaskController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/TaskCalendarController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/TaskListController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/VendorContactController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/VendorContactListController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/VendorController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/VendorListController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/WeatherController").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/WeatherListController").For(UserType.Employee.Key).OnEverything().Level(1).Save();

            _permissionsBuilderService.Allow("/MenuItem/Home").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Fields").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Equipment").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Tasks").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Events").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Calculators").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Weather").For(UserType.Employee.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Forum").For(UserType.Employee.Key).OnEverything().Level(1).Save();
        }

        public void GrantDefaultFacilitiesPermissions()
        {
            _permissionsBuilderService
                .Allow("/EventController")
                .For(UserType.Facilities.Key)
                .OnEverything()
                .Level(1)
                .Save();
            _permissionsBuilderService
                .Allow("/EventCalendarController")
                .For(UserType.Facilities.Key)
                .OnEverything()
                .Level(1)
                .Save();
            _permissionsBuilderService.Allow("/MenuItem/Events").For(UserType.Facilities.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/SystemSupportController").For(UserType.Facilities.Key).OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/OrthogonalController").For(UserType.Facilities.Key).OnEverything().Level(1).Save();

        }
    }
}