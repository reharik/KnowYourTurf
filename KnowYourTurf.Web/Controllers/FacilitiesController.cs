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
    public class FacilitiesController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IUploadedFileHandlerService _uploadedFileHandlerService;
        private readonly ISessionContext _sessionContext;

        public FacilitiesController(IRepository repository,
            ISaveEntityService saveEntityService,
            IUploadedFileHandlerService uploadedFileHandlerService,
            ISessionContext sessionContext)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _uploadedFileHandlerService = uploadedFileHandlerService;
            _sessionContext = sessionContext;
        }

        public ActionResult Facilities(ViewModel input)
        {
            var facilities = input.EntityId > 0 ? _repository.Find<Facilities>(input.EntityId) : new Facilities();
            
            var model = new FacilitiesViewModel
            {
                Facilities = facilities,
            };
            return PartialView("FacilitiesAddUpdate", model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var facilities = _repository.Find<Facilities>(input.EntityId);
            var model = new FacilitiesViewModel
                            {
                                Facilities = facilities,
                                AddEditUrl = UrlContext.GetUrlForAction<FacilitiesController>(x => x.Facilities(null)) + "/" + facilities.EntityId
                            };
            return PartialView("FacilitiesView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var facilities = _repository.Find<Facilities>(input.EntityId);
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

        public ActionResult Save(FacilitiesViewModel input)
        {
            Facilities facilities;
            if (input.Facilities.EntityId > 0)
            {
                facilities = _repository.Find<Facilities>(input.Facilities.EntityId);
            }
            else
            {
                facilities = new Facilities();
                var companyId = _sessionContext.GetCompanyId();
                var company = _repository.Find<Company>(companyId);
                facilities.Company = company;
            }
            facilities = mapToDomain(input, facilities);

            if (input.DeleteImage)
            {
                _uploadedFileHandlerService.DeleteFile(facilities.ImageUrl);
                facilities.ImageUrl = string.Empty;
            }

            var serverDirectory = "/CustomerPhotos/" + facilities.CompanyId + "/Facilitiess";
            facilities.ImageUrl = _uploadedFileHandlerService.GetUploadedFileUrl(serverDirectory, facilities.FirstName+"_"+facilities.LastName);
            var crudManager = _saveEntityService.ProcessSave(facilities);

            crudManager = _uploadedFileHandlerService.SaveUploadedFile(serverDirectory, facilities.FirstName + "_" + facilities.LastName, crudManager);
            var notification = crudManager.Finish();
            return Json(notification,"text/plain");
        }

        private Facilities mapToDomain(FacilitiesViewModel model, Facilities facilities)
        {
            var facilitiesModel = model.Facilities;
            facilities.FacilitiesId = facilitiesModel.FacilitiesId;
            facilities.Address1 = facilitiesModel.Address1;
            facilities.Address2= facilitiesModel.Address2;
            facilities.FirstName= facilitiesModel.FirstName;
            facilities.LastName = facilitiesModel.LastName;
            facilities.Password = facilitiesModel.Password;
            facilities.Email = facilitiesModel.Email;
            facilities.LoginName = facilitiesModel.Email;
            facilities.PhoneMobile = facilitiesModel.PhoneMobile;
            facilities.City = facilitiesModel.City;
            facilities.State = facilitiesModel.State;
            facilities.ZipCode = facilitiesModel.ZipCode;
            facilities.Status= facilitiesModel.Status;
            facilities.Notes = facilitiesModel.Notes;
            facilities.UserRoles = UserRole.Facilities.ToString();
            return facilities;
        }
    }
}