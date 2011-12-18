using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Services;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class AdminController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IUploadedFileHandlerService _uploadedFileHandlerService;
        private readonly ISessionContext _sessionContext;

        public AdminController(IRepository repository,
            ISaveEntityService saveEntityService,
            IUploadedFileHandlerService uploadedFileHandlerService,
            ISessionContext sessionContext)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _uploadedFileHandlerService = uploadedFileHandlerService;
            _sessionContext = sessionContext;
        }

        public ActionResult Admin(ViewModel input)
        {
            var admin = input.EntityId > 0 ? _repository.Find<User>(input.EntityId) : new User();
            
            var model = new UserViewModel
            {
                User = admin,
                Title = WebLocalizationKeys.ADMINISTRATOR_INFORMATION.ToString()
            };
            return PartialView("AdminAddUpdate", model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var admin = _repository.Find<User>(input.EntityId);
            var model = new UserViewModel
                            {
                                User= admin,
                                AddUpdateUrl = UrlContext.GetUrlForAction<AdminController>(x => x.Admin(null)) + "/" + admin.EntityId,
                                Title = WebLocalizationKeys.ADMINISTRATOR_INFORMATION.ToString()
                            };
            return PartialView("AdminView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var admin = _repository.Find<User>(input.EntityId);
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeleteAdminRules");
            var rulesResult = rulesEngineBase.ExecuteRules(admin);
            if(!rulesResult.Success)
            {
                Notification notification = new Notification(rulesResult);
                return Json(notification);
            }
            _repository.SoftDelete(admin);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(UserViewModel input)
        {
            User administrator;
            if (input.User.EntityId > 0)
            {
                administrator = _repository.Find<User>(input.User.EntityId);
            }
            else
            {
                administrator = new User();
                var company = _sessionContext.GetCurrentCompany();
                administrator.Company = company;
            }
            administrator = mapToDomain(input, administrator);

            if (input.DeleteImage)
            {
                _uploadedFileHandlerService.DeleteFile(administrator.ImageUrl);
                administrator.ImageUrl = string.Empty;
            }

            var serverDirectory = "/CustomerPhotos/" + administrator.CompanyId + "/Admins";
            administrator.ImageUrl = _uploadedFileHandlerService.GetUploadedFileUrl(serverDirectory, administrator.FirstName+"_"+administrator.LastName);
            var crudManager = _saveEntityService.ProcessSave(administrator);
                var user = _repository.Find<User>(administrator.EntityId);
                _saveEntityService.ProcessSave(user,crudManager);
            crudManager = _uploadedFileHandlerService.SaveUploadedFile(serverDirectory, administrator.FirstName + "_" + administrator.LastName, crudManager);
            var notification = crudManager.Finish();
            return Json(notification,"text/plain");
        }

        private User mapToDomain(UserViewModel model, User administrator)
        {
            var adminModel = model.User;
            administrator.Address1 = adminModel.Address1;
            administrator.Address2= adminModel.Address2;
            administrator.FirstName= adminModel.FirstName;
            administrator.LastName = adminModel.LastName;
            administrator.Email = adminModel.Email;
            administrator.PhoneMobile = adminModel.PhoneMobile;
            administrator.City = adminModel.City;
            administrator.State = adminModel.State;
            administrator.ZipCode = adminModel.ZipCode;
            administrator.Notes = adminModel.Notes;
            administrator.UserLoginInfo = new UserLoginInfo()
                                              {
                                                  Password = adminModel.UserLoginInfo.Password,
                                                  LoginName = adminModel.Email,
                                                  Status = adminModel.UserLoginInfo.Status,
                                                  UserType = UserType.Administrator.ToString(),
                                              };
            administrator.AddUserRole(new UserRole { Name = UserType.Administrator.ToString() });
            administrator.AddUserRole(new UserRole { Name = UserType.Employee.ToString() });
            return administrator;
        }
    }
}