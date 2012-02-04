using KnowYourTurf.Core.Domain;
using Rhino.Security.Interfaces;
using StructureMap;

namespace KnowYourTurf.Web.Config
{
    public class SecurityBootstrapper 
    {
        public static void Bootstrap()
        {
            var repository = ObjectFactory.GetInstance<IRepository>();
            var authRepo = ObjectFactory.GetInstance<IAuthorizationRepository>();
            authRepo.CreateUsersGroup("Manager");
            authRepo.CreateUsersGroup("Admin");
            authRepo.CreateUsersGroup("User");
            authRepo.CreateUsersGroup("546_SpecialAdmin");
            authRepo.CreateUsersGroup("546_SpecialManager");
            authRepo.CreateUsersGroup("546_SpecialUser");
            authRepo.CreateChildUserGroupOf("546_SpecialManager", "SuperSpecial");
            
            
            authRepo.CreateOperation("/Course/CreateCourse");
            authRepo.CreateOperation("/Course/CreateTest");
            authRepo.CreateOperation("/CreateOrg");
            authRepo.CreateOperation("/ViewDashboard");
            authRepo.CreateOperation("/Course/CreateCourseThatIsSpecial");
            authRepo.CreateOperation("/Course/CreateTestThatIsSpecial");
            authRepo.CreateOperation("/CreateOrgThatIsSpecial");
            authRepo.CreateOperation("/ViewDashboardThatIsSpecial");
            authRepo.CreateOperation("/DoSomethingThatOnlyAFewMangersCanDo");


            var user1 = repository.Find<User>(663225);
            var user2 = repository.Find<User>(663226);
            var user3 = repository.Find<User>(663228);
            var user4 = repository.Find<User>(663229);
            var user5 = repository.Find<User>(663230);
            var user6 = repository.Find<User>(663236);
            var user7 = repository.Find<User>(663237);
            var user8 = repository.Find<User>(647842);
            authRepo.AssociateUserWith(user1, "Admin");
            authRepo.AssociateUserWith(user2, "Manager");
            authRepo.AssociateUserWith(user3, "User");
            authRepo.AssociateUserWith(user4, "Manager");
            authRepo.AssociateUserWith(user4, "User");
            authRepo.AssociateUserWith(user5, "546_SpecialManager");
            authRepo.AssociateUserWith(user6, "546_SpecialUser");
            authRepo.AssociateUserWith(user7, "546_SpecialAdmin");
            authRepo.AssociateUserWith(user8, "SuperSpecial");

            var permBuilder = ObjectFactory.GetInstance<IPermissionsBuilderService>();
            permBuilder.Allow("/Course/CreateCourse").For("Manager").OnEverything().DefaultLevel().Save();
            permBuilder.Allow("/Course/CreateTest").For("Manager").OnEverything().DefaultLevel().Save();
            permBuilder.Allow("/CreateOrg").For("Admin").OnEverything().DefaultLevel().Save();
            permBuilder.Allow("/ViewDashboard").For("User").OnEverything().DefaultLevel().Save();
            permBuilder.Allow("/Course/CreateCourseThatIsSpecial").For("546_SpecialManager").OnEverything().DefaultLevel().Save();
            permBuilder.Allow("/ViewDashboardThatIsSpecial").For("546_SpecialUser").OnEverything().DefaultLevel().Save();
            permBuilder.Allow("/CreateOrgThatIsSpecial").For("546_SpecialAdmin").OnEverything().DefaultLevel().Save();
            permBuilder.Allow("/DoSomethingThatOnlyAFewMangersCanDo").For("SuperSpecial").OnEverything().DefaultLevel().Save();
            
            repository.Commit();
        }
    }
}