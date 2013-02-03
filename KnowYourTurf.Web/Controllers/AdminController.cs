using System.Web.Mvc;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Services;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class AdminController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly ISessionContext _sessionContext;
        private readonly ISecurityDataService _securityDataService;

        public AdminController(IRepository repository,
            ISaveEntityService saveEntityService,
            IFileHandlerService fileHandlerService,
            ISessionContext sessionContext,
            ISecurityDataService securityDataService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _fileHandlerService = fileHandlerService;
            _sessionContext = sessionContext;
            _securityDataService = securityDataService;
        }

        public ActionResult Admin(ViewModel input)
        {
//            var admin = input.EntityId > 0 ? _repository.Find<User>(input.EntityId) : new User();
//            
//            var model = new UserViewModel
//            {
//                Item = admin,
//                _Title = WebLocalizationKeys.ADMINISTRATOR_INFORMATION.ToString()
//            };
//            return PartialView("AdminAddUpdate", model);
            return null;
        }
      
        public ActionResult Display(ViewModel input)
        {
//            var admin = _repository.Find<User>(input.EntityId);
//            var model = new UserViewModel
//                            {
//                                Item= admin,
//                                AddUpdateUrl = UrlContext.GetUrlForAction<AdminController>(x => x.Admin(null)) + "/" + admin.EntityId,
//                                _Title = WebLocalizationKeys.ADMINISTRATOR_INFORMATION.ToString()
//                            };
//            return PartialView("AdminView", model);
            return null;
        }

        public ActionResult Delete(ViewModel input)
        {
            var admin = _repository.Find<User>(input.EntityId);
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeleteAdminRules");
            var rulesResult = rulesEngineBase.ExecuteRules(admin);
            if(!rulesResult.Success)
            {
                var notification = new RulesNotification(rulesResult);
                return new CustomJsonResult(notification);
            }
            _repository.Delete(admin);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(UserViewModel input)
        {
            User administrator;
            if (input.EntityId > 0)
            {
                administrator = _repository.Find<User>(input.EntityId);
            }
            else
            {
                administrator = new User();
                var client = _sessionContext.GetCurrentClient();
                administrator.Client = client;
            }
            administrator = mapToDomain(input, administrator);
            handlePassword(input,administrator);
            if (input.DeleteImage)
            {
                _fileHandlerService.DeleteFile(administrator.FileUrl);
                administrator.FileUrl = string.Empty;
            }

            administrator.FileUrl = _fileHandlerService.SaveAndReturnUrlForFile("CustomerPhotos/Admins");
            administrator.ImageFriendlyName = administrator.FirstName + "_" + administrator.LastName;
            var crudManager = _saveEntityService.ProcessSave(administrator);
                var user = _repository.Find<User>(administrator.EntityId);
                _saveEntityService.ProcessSave(user,crudManager);
            var notification = crudManager.Finish();
            return new CustomJsonResult(notification, "text/plain");
        }

        private User mapToDomain(UserViewModel model, User administrator)
        {
            administrator.Address1 = model.Address1;
            administrator.Address2 = model.Address2;
            administrator.FirstName = model.FirstName;
            administrator.LastName = model.LastName;
            administrator.Email = model.Email;
            administrator.PhoneMobile = model.PhoneMobile;
            administrator.City = model.City;
            administrator.State = model.State;
            administrator.ZipCode = model.ZipCode;
            administrator.Notes = model.Notes;
            administrator.UserLoginInfo = new UserLoginInfo()
                                              {
                                                  Password = model.UserLoginInfoPassword,
                                                  LoginName = model.Email
                                              };
            administrator.AddUserRole(new UserRole { Name = UserType.Administrator.ToString() });
            administrator.AddUserRole(new UserRole { Name = UserType.Employee.ToString() });
            return administrator;
        }

        private void handlePassword(UserViewModel input, User origional)
        {
            if (input.UserLoginInfoPassword.IsNotEmpty())
            {
                origional.UserLoginInfo.Salt = _securityDataService.CreateSalt();
                origional.UserLoginInfo.Password = _securityDataService.CreatePasswordHash(input.UserLoginInfoPassword,
                                                            origional.UserLoginInfo.Salt);
            }
        }
    }
}