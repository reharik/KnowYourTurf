using System.Web.Mvc;
using AutoMapper;
using KnowYourTurf.Security.Interfaces;
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
    public class FacilitiesController : AdminControllerBase
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly ISessionContext _sessionContext;
        private readonly ISelectListItemService _selectListItemService;
        private readonly IAuthorizationRepository _authorizationRepository;

        public FacilitiesController(IRepository repository,
            ISaveEntityService saveEntityService,
            IFileHandlerService fileHandlerService,
            ISessionContext sessionContext,
            ISelectListItemService selectListItemService,
            IAuthorizationRepository authorizationRepository)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _fileHandlerService = fileHandlerService;
            _sessionContext = sessionContext;
            _selectListItemService = selectListItemService;
            _authorizationRepository = authorizationRepository;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("FacilitiesAddUpdate", new UserViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var facilities = input.EntityId > 0 ? _repository.Find<User>(input.EntityId) : new User();
            var model = Mapper.Map<User, UserViewModel>(facilities);
            model.FileUrl = model.FileUrl.IsNotEmpty() ? BasicExtentions.AddImageSizeToName(model.FileUrl, "thumb") : "";
            model._StateList = _selectListItemService.CreateList<State>();
            model._Title = WebLocalizationKeys.FACILITIES.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<FacilitiesController>(x => x.Save(null));
            model._UserLoginInfoStatusList = _selectListItemService.CreateList<Status>(true);
            
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(ViewModel input)
        {
            var facilities = _repository.Find<User>(input.EntityId);
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeleteFacilitiesRules");
            var rulesResult = rulesEngineBase.ExecuteRules(facilities);
            if(!rulesResult.Success)
            {
                Notification notification = new Notification(rulesResult);
                return Json(notification);
            }
            _repository.SoftDelete(facilities);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(UserViewModel input)
        {
            User facilities;
            if (input.EntityId > 0)
            {
                facilities = _repository.Find<User>(input.EntityId);
            }
            else
            {
                facilities = new User();
                var companyId = _sessionContext.GetCompanyId();
                var company = _repository.Find<Company>(companyId);
                facilities.Company = company;
            }
            facilities = mapToDomain(input, facilities);
            mapRolesToGroups(facilities);
            if (input.DeleteImage)
            {
                _fileHandlerService.DeleteFile(facilities.FileUrl);
                facilities.FileUrl = string.Empty;
            }

            facilities.FileUrl = _fileHandlerService.SaveAndReturnUrlForFile("CustomerPhotos/Facilitiess");
            facilities.ImageFriendlyName = facilities.FirstName + "_" + facilities.LastName;
            var crudManager = _saveEntityService.ProcessSave(facilities);

            var notification = crudManager.Finish();
            return Json(notification,"text/plain");
        }

        private User mapToDomain(UserViewModel model, User facilities)
        {
            facilities.Address1 = model.Address1;
            facilities.Address2 = model.Address2;
            facilities.FirstName = model.FirstName;
            facilities.LastName = model.LastName;
            facilities.Email = model.Email;
            facilities.PhoneMobile = model.PhoneMobile;
            facilities.City = model.City;
            facilities.State = model.State;
            facilities.ZipCode = model.ZipCode;
            facilities.Notes = model.Notes;
            facilities.UserLoginInfo = new UserLoginInfo()
            {
                Password = model.UserLoginInfoPassword,
                LoginName = model.Email,
                Status = model.UserLoginInfoStatus,
            };
            var role = _repository.Query<UserRole>(x => x.Name == UserType.Facilities.ToString()).FirstOrDefault();
            facilities.AddUserRole(role);
            return facilities;
        }

        private void mapRolesToGroups(User facilities)
        {
            _authorizationRepository.DetachUserFromGroup(facilities, UserType.Administrator.Key);
            _authorizationRepository.DetachUserFromGroup(facilities, UserType.Employee.Key);
            _authorizationRepository.DetachUserFromGroup(facilities, UserType.Facilities.Key);
            _authorizationRepository.DetachUserFromGroup(facilities, UserType.KYTAdmin.Key);
            _authorizationRepository.DetachUserFromGroup(facilities, UserType.MultiTenant.Key);

            _authorizationRepository.AssociateUserWith(facilities, UserType.Facilities.Key);
        }
    }
}