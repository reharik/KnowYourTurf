using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;
using System.Linq;
using Rhino.Security.Interfaces;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class FacilitiesController : AdminControllerBase
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly ISessionContext _sessionContext;
        private readonly IAuthorizationRepository _authorizationRepository;

        public FacilitiesController(IRepository repository,
            ISaveEntityService saveEntityService,
            IFileHandlerService fileHandlerService,
            ISessionContext sessionContext, IAuthorizationRepository authorizationRepository)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _fileHandlerService = fileHandlerService;
            _sessionContext = sessionContext;
            _authorizationRepository = authorizationRepository;
        }

        public ActionResult Facilities(ViewModel input)
        {
            var facilities = input.EntityId > 0 ? _repository.Find<User>(input.EntityId) : new User();
            
            var model = new UserViewModel
            {
                Item = facilities,
                Title = WebLocalizationKeys.FACILITIES.ToString()
            };
            return PartialView("FacilitiesAddUpdate", model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var facilities = _repository.Find<User>(input.EntityId);
            var model = new UserViewModel
                            {
                                Item = facilities,
                                AddUpdateUrl = UrlContext.GetUrlForAction<FacilitiesController>(x => x.Facilities(null)) + "/" + facilities.EntityId,
                                Title = WebLocalizationKeys.FACILITIES.ToString()
                            };
            return PartialView("FacilitiesView", model);
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
            if (input.Item.EntityId > 0)
            {
                facilities = _repository.Find<User>(input.Item.EntityId);
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
                _fileHandlerService.DeleteFile(facilities.ImageUrl);
                facilities.ImageUrl = string.Empty;
            }

            facilities.ImageUrl = _fileHandlerService.SaveAndReturnUrlForFile("CustomerPhotos/Facilitiess");
            facilities.ImageFriendlyName = facilities.FirstName + "_" + facilities.LastName;
            var crudManager = _saveEntityService.ProcessSave(facilities);

            var notification = crudManager.Finish();
            return Json(notification,"text/plain");
        }

        private User mapToDomain(UserViewModel model, User facilities)
        {
            var facilitiesModel = model.Item;
            facilities.Address1 = facilitiesModel.Address1;
            facilities.Address2= facilitiesModel.Address2;
            facilities.FirstName= facilitiesModel.FirstName;
            facilities.LastName = facilitiesModel.LastName;
            facilities.Email = facilitiesModel.Email;
            facilities.PhoneMobile = facilitiesModel.PhoneMobile;
            facilities.City = facilitiesModel.City;
            facilities.State = facilitiesModel.State;
            facilities.ZipCode = facilitiesModel.ZipCode;
            facilities.Notes = facilitiesModel.Notes;
            facilities.UserLoginInfo = new UserLoginInfo()
            {
                Password = facilitiesModel.UserLoginInfo.Password,
                LoginName = facilitiesModel.Email,
                Status = facilitiesModel.UserLoginInfo.Status,
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