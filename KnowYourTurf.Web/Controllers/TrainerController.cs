using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModelAndDTOs;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Domain.Tools;
using KnowYourTurf.Core.Enumerations;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Rules;
using KnowYourTurf.Core.Services;
using Rhino.Security.Interfaces;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class TrainerController : AdminController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IFileHandlerService _uploadedFileHandlerService;
        private readonly ISessionContext _sessionContext;
        private readonly ISecurityDataService _securityDataService;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IUpdateCollectionService _updateCollectionService;

        public TrainerController(IRepository repository,
            ISaveEntityService saveEntityService,
            IFileHandlerService uploadedFileHandlerService,
            ISessionContext sessionContext,
            ISecurityDataService securityDataService,
            IAuthorizationRepository authorizationRepository,
            IUpdateCollectionService updateCollectionService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _uploadedFileHandlerService = uploadedFileHandlerService;
            _sessionContext = sessionContext;
            _securityDataService = securityDataService;
            _authorizationRepository = authorizationRepository;
            _updateCollectionService = updateCollectionService;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var trainer = input.EntityId > 0 ? _repository.Find<User>(input.EntityId) : new User();
            var userRoles = _repository.FindAll<UserRole>();
            var availableUserRoles = userRoles.Select(x => new TokenInputDto { id = x.EntityId, name = x.Name});
            var selectedUserRoles = trainer.UserRoles.Any()
                ? trainer.UserRoles.Select(x => new TokenInputDto { id = x.EntityId, name = x.Name })
                : null;

            var model = new UserViewModel
            {
                User = trainer,
                AvailableItems = availableUserRoles,
                SelectedItems = selectedUserRoles,
                Title = WebLocalizationKeys.TRAINER_INFORMATION.ToString()
            };
            return PartialView("TrainerAddUpdate", model);
        }

        public ActionResult Display(ViewModel input)
        {
            var trainer = _repository.Find<User>(input.EntityId);
            var model = new UserViewModel
            {
                User = trainer,
                AddUpdateUrl = UrlContext.GetUrlForAction<TrainerController>(x => x.AddUpdate(null)) + "/" + trainer.EntityId,
                Title = WebLocalizationKeys.TRAINER_INFORMATION.ToString()
            };
            return PartialView("TrainerView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var trainer = _repository.Find<User>(input.EntityId);
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeleteTrainerRules");
            var rulesResult = rulesEngineBase.ExecuteRules(trainer);
            if (!rulesResult.Success)
            {
                Notification notification = new Notification(rulesResult);
                return Json(notification);
            }
            _repository.Delete(trainer);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(UserViewModel input)
        {
            User trainer;
            trainer = input.EntityId > 0 ? _repository.Find<User>(input.EntityId) : new User();
            trainer = mapToDomain(input, trainer);
            handlePassword(input, trainer);
            addSecurityUserGroups(trainer);
//            if (input.DeleteImage)
//            {
////                _uploadedFileHandlerService.DeleteFile(trainer.ImageUrl);
//                trainer.ImageUrl = string.Empty;
//            }
//            
//            var file = _uploadedFileHandlerService.RetrieveUploadedFile();
////            var serverDirectory = "/CustomerPhotos/" + _sessionContext.GetCompanyId() + "/Trainers";
//            trainer.ImageUrl = _uploadedFileHandlerService.GetUrlForFile(file, trainer.FirstName + "_" + trainer.LastName);
            var crudManager = _saveEntityService.ProcessSave(trainer);

//            _uploadedFileHandlerService.SaveUploadedFile(file, trainer.FirstName + "_" + trainer.LastName);
            var notification = crudManager.Finish();
            return Json(notification, "text/plain");
        }

        private void addSecurityUserGroups(User trainer)
        {
            _authorizationRepository.AssociateUserWith(trainer,SecurityUserGroups.Trainer.ToString());
            if(trainer.UserRoles.Any(x=>x.Name==SecurityUserGroups.Administrator.ToString()))
            {
                _authorizationRepository.AssociateUserWith(trainer, SecurityUserGroups.Administrator.ToString());
            }
        }

        private void handlePassword(UserViewModel input, User origional)
        {
            if (input.Password.IsNotEmpty())
            {
                origional.UserLoginInfo.Salt = _securityDataService.CreateSalt();
                origional.UserLoginInfo.Password = _securityDataService.CreatePasswordHash(input.Password,
                                                            origional.UserLoginInfo.Salt);
            }
        }

        private User mapToDomain(UserViewModel model, User trainer)
        {
            var trainerModel = model.User;
            trainer.Address1 = trainerModel.Address1;
            trainer.Address2 = trainerModel.Address2;
            trainer.FirstName = trainerModel.FirstName;
            trainer.LastName = trainerModel.LastName;
            trainer.Email = trainerModel.Email;
            trainer.PhoneMobile = trainerModel.PhoneMobile;
            trainer.City = trainerModel.City;
            trainer.State = trainerModel.State;
            trainer.ZipCode = trainerModel.ZipCode;
            trainer.Notes = trainerModel.Notes;
            trainer.Status = trainerModel.Status;
            trainer.BirthDate = trainerModel.BirthDate;
            trainer.Color = trainerModel.Color;
            
            trainer.UserLoginInfo = new UserLoginInfo()
            {
                LoginName = trainer.Email
            };

            _updateCollectionService.UpdateFromCSV(trainer.UserRoles,model.UserRolesInput,trainer.AddUserRole,trainer.RemoveUserRole);
            return trainer;
        }
    }

    public class UserViewModel:ViewModel
    {
        public User User { get; set; }
        public IEnumerable<TokenInputDto> AvailableItems { get; set; }
        public IEnumerable<TokenInputDto> SelectedItems { get; set; }
        public bool DeleteImage { get; set; }
        public string Password { get; set; }
        [ValidateSameAs("Password")]
        public string PasswordConfirmation { get; set; }
        public string UserRolesInput { get; set; }
    }
}