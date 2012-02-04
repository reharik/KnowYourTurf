using KnowYourTurf.Core.Domain;
using Rhino.Security.Interfaces;

namespace KnowYourTurf.Core.Services
{
    public interface IKYTPermissionsService
    {
        void GrantDefaultAdminPermissions(string type);
        void GrantDefaultTrainersPermissions();
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


        public void GrantDefaultAdminPermissions(string type)
        {
            var operations = ((CustomAuthorizationRepository)_authorizationRepository).GetAllOperations();
            foreach (var operation in operations)
            {
                if (operation.Name != "/KYTAdmin")
                    _permissionsBuilderService
                        .Allow(operation)
                        .For(type)
                        .OnEverything()
                        .Level(10)
                        .Save();
            }
        }

        public void GrantDefaultTrainersPermissions()
        {
           // _permissionsBuilderService.Allow("/HomeController").For("Trainer").OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/EmployeeDashboardController").For("Trainer").OnEverything().Level(1).Save();
            //_permissionsBuilderService.Allow("/EmployeeController").For("Trainer").OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/AppointmentCalendarController").For("Trainer").OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/AppointmentController").For("Trainer").OnEverything().Level(1).Save();

         //   _permissionsBuilderService.Allow("/MenuItem/Home").For("Trainer").OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/EmployeeDashboard").For("Trainer").OnEverything().Level(1).Save();
            _permissionsBuilderService.Allow("/MenuItem/Calendar").For("Trainer").OnEverything().Level(1).Save();
        }

    }
}