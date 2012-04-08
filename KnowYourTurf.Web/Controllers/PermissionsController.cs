using System.Collections.Generic;
using System.Web.Mvc;
using FubuMVC.Core;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using NHibernate.Collection.Generic;
using Rhino.Security.Interfaces;

namespace KnowYourTurf.Web.Controllers
{
    public class PermissionsController : KYTController
    {
        private readonly IRepository _repository;
        private readonly IPermissionsService _permissionsService;
        private readonly IAuthorizationRepository _authorizationRepository;

        public PermissionsController(IRepository repository,IPermissionsService permissionsService,IAuthorizationRepository authorizationRepository)
        {
            _repository = repository;
            _permissionsService = permissionsService;
            _authorizationRepository = authorizationRepository;
        }

        public ActionResult ShowPermissions()
        {
            var model = new showPermissions {Permissions = new List<Permissions>()};
            var user1 =_repository.Find<User>(663225);
            var user2 =_repository.Find<User>(663226);
            var user3 =_repository.Find<User>(663228);
            var user4 =_repository.Find<User>(663229);
            var user5 =_repository.Find<User>(663230);
            var user6 =_repository.Find<User>(663236);
            var user7 =_repository.Find<User>(663237);
            var user8 =_repository.Find<User>(647842);
            var permissions1 = new Permissions{UserName = user1.FullNameLNF, PermList = new List<string>()};
            _permissionsService.GetPermissionsFor(user1).Each(x => permissions1.PermList.Add(x.Operation.Name));
            model.Permissions.Add(permissions1);
            _authorizationRepository.GetAssociatedUsersGroupFor(user1).Each(x => permissions1.userGroupList.Add(x.Name));
            
            var permissions2= new Permissions{UserName = user2.FullNameLNF, PermList = new List<string>()};
            _permissionsService.GetPermissionsFor(user2).Each(x => permissions2.PermList.Add(x.Operation.Name));
            model.Permissions.Add(permissions2);
            _authorizationRepository.GetAssociatedUsersGroupFor(user2).Each(x => permissions2.userGroupList.Add(x.Name));
            
            var permissions3 = new Permissions { UserName = user3.FullNameLNF, PermList = new List<string>() };
            _permissionsService.GetPermissionsFor(user3).Each(x => permissions3.PermList.Add(x.Operation.Name));
            model.Permissions.Add(permissions3);
            _authorizationRepository.GetAssociatedUsersGroupFor(user3).Each(x => permissions3.userGroupList.Add(x.Name));
            
            var permissions4 = new Permissions { UserName = user4.FullNameLNF, PermList = new List<string>() };
            _permissionsService.GetPermissionsFor(user4).Each(x => permissions4.PermList.Add(x.Operation.Name));
            model.Permissions.Add(permissions4);
            _authorizationRepository.GetAssociatedUsersGroupFor(user4).Each(x => permissions4.userGroupList.Add(x.Name));
            
            var permissions5 = new Permissions { UserName = user5.FullNameLNF, PermList = new List<string>() };
            _permissionsService.GetPermissionsFor(user5).Each(x => permissions5.PermList.Add(x.Operation.Name));
            model.Permissions.Add(permissions5);
            _authorizationRepository.GetAssociatedUsersGroupFor(user5).Each(x => permissions5.userGroupList.Add(x.Name));
            
            var permissions6 = new Permissions { UserName = user6.FullNameLNF, PermList = new List<string>() };
            _permissionsService.GetPermissionsFor(user6).Each(x => permissions6.PermList.Add(x.Operation.Name));
            model.Permissions.Add(permissions6);
            _authorizationRepository.GetAssociatedUsersGroupFor(user6).Each(x => permissions6.userGroupList.Add(x.Name));
            
            var permissions7 = new Permissions { UserName = user7.FullNameLNF, PermList = new List<string>() };
            _permissionsService.GetPermissionsFor(user7).Each(x => permissions7.PermList.Add(x.Operation.Name));
            model.Permissions.Add(permissions7);
            _authorizationRepository.GetAssociatedUsersGroupFor(user7).Each(x => permissions7.userGroupList.Add(x.Name));
            
            var permissions8 = new Permissions { UserName = user8.FullNameLNF, PermList = new List<string>() };
            _permissionsService.GetPermissionsFor(user8).Each(x => permissions8.PermList.Add(x.Operation.Name));
            model.Permissions.Add(permissions8);
            _authorizationRepository.GetAssociatedUsersGroupFor(user8).Each(x => permissions8.userGroupList.Add(x.Name));
            return View(model);


        }
    }

    public class showPermissions:ViewModel
    {
        public IList<Permissions> Permissions { get; set; }
    }

    public class Permissions
    {
        public Permissions()
        {
            PermList = new List<string>();
            userGroupList = new List<string>();
        }

        public string UserName { get; set; }
        public IList<string> PermList { get; set; }
        public IList<string> userGroupList { get; set; }
    }
}