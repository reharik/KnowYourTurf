using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Areas.Schedule.Controllers;
using StructureMap;

namespace Generator
{
    public class DataLoader
    {
        private IRepository _repository;
        private ISecurityDataService _securityDataService;
        private IDynamicExpressionQuery _dynamicExpressionQuery;
        private User _defaultUser;
        private UserRole _userRoleTrainer;
        private UserRole _userRoleAdmin;


        public void Load()
        {
            _dynamicExpressionQuery = ObjectFactory.GetInstance<IDynamicExpressionQuery>();
            _repository = ObjectFactory.GetNamedInstance<IRepository>("NoFiltersOrInterceptor");

            _securityDataService = ObjectFactory.GetInstance<ISecurityDataService>();

            _repository.Initialize();
            createCompany();
            createLocations();
            createUserRoles();
            createUser();
            _repository.Commit();
        }

        private void createCompany()
        {
            var company = new Company{Name = "Method Fitness"};
            _repository.Save(company);
        }

        private void createLocations()
        {
            var location1 = new Location { Name = "West", CompanyId = 1};
            var location2 = new Location { Name = "East", CompanyId = 1 };
            _repository.Save(location1);
            _repository.Save(location2);
        }

        private void createUserRoles()
        {
            _userRoleTrainer = new UserRole
                                   {
                                       Name = "Trainer"
                                   };
            _userRoleAdmin = new UserRole
                                 {
                                     Name = "Administrator"
                                 };
            _repository.Save(_userRoleAdmin);
            _repository.Save(_userRoleTrainer);
        }

        private void createUser()
        {
            var salt = _securityDataService.CreateSalt();
            var passwordHash = _securityDataService.CreatePasswordHash("123", salt);
            _defaultUser = new User
                               {
                                   FirstName = "Raif",
                                   LastName = "Harik",
                                   CompanyId = 1
                               };
            _defaultUser.AddUserRole(_userRoleTrainer);
            _defaultUser.AddUserRole(_userRoleAdmin);
            _defaultUser.UserLoginInfo = new UserLoginInfo
                                             {
                                                 LoginName = "Admin",
                                                 Password = passwordHash,
                                                 Salt = salt,
                                                 CompanyId = 1
                                             };
            _repository.Save(_defaultUser);
        }
    }
}
