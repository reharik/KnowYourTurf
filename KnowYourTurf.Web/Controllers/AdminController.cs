using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;
using System.Linq;
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
            var admin = input.EntityId > 0 ? _repository.Find<Administrator>(input.EntityId) : new Administrator();
            
            var model = new AdminViewModel
            {
                Administrator = admin,
            };
            return PartialView("AdminAddUpdate", model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var admin = _repository.Find<Administrator>(input.EntityId);
            var model = new AdminViewModel
                            {
                                Administrator = admin,
                                AddEditUrl = UrlContext.GetUrlForAction<AdminController>(x => x.Admin(null)) + "/" + admin.EntityId
                            };
            return PartialView("AdminView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var admin = _repository.Find<Administrator>(input.EntityId);
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

        public ActionResult Save(AdminViewModel input)
        {
            Administrator administrator;
            if (input.Administrator.EntityId > 0)
            {
                administrator = _repository.Find<Administrator>(input.Administrator.EntityId);
            }
            else
            {
                administrator = new Administrator();
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
            if(administrator.IsAnEmployee)
            {
                var user = _repository.Find<User>(administrator.EntityId);
                var employee = user as Employee;
                _saveEntityService.ProcessSave(employee,crudManager);
            }
            crudManager = _uploadedFileHandlerService.SaveUploadedFile(serverDirectory, administrator.FirstName + "_" + administrator.LastName, crudManager);
            var notification = crudManager.Finish();
            return Json(notification,"text/plain");
        }

        private Administrator mapToDomain(AdminViewModel model, Administrator administrator)
        {
            var adminModel = model.Administrator;
            administrator.AdminId = adminModel.AdminId;
            administrator.Address1 = adminModel.Address1;
            administrator.Address2= adminModel.Address2;
            administrator.FirstName= adminModel.FirstName;
            administrator.LastName = adminModel.LastName;
            administrator.Password = adminModel.Password;
            administrator.Email = adminModel.Email;
            administrator.LoginName = adminModel.Email;
            administrator.PhoneMobile = adminModel.PhoneMobile;
            administrator.City = adminModel.City;
            administrator.State = adminModel.State;
            administrator.ZipCode = adminModel.ZipCode;
            administrator.Status= adminModel.Status;
            administrator.Notes = adminModel.Notes;
            administrator.IsAnEmployee = adminModel.IsAnEmployee;
            administrator.UserRoles = UserRole.Administrator.ToString();
            return administrator;
        }
    }
}