using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Enumerations;
using CC.Core.Html;
using CC.Core.Services;
using CC.Security.Interfaces;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;
using System.Linq;
using StructureMap;
using xVal.ServerSide;
using Status = KnowYourTurf.Core.Enums.Status;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class EmployeeController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISessionContext _sessionContext;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IUpdateCollectionService _updateCollectionService;
        private readonly ISelectListItemService _selectListItemService;
        private readonly ISecurityDataService _securityDataService;

        public EmployeeController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISessionContext sessionContext,
            IFileHandlerService fileHandlerService,
            IAuthorizationRepository authorizationRepository,
            IUpdateCollectionService updateCollectionService,
            ISelectListItemService selectListItemService,
            ISecurityDataService securityDataService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _sessionContext = sessionContext;
            _fileHandlerService = fileHandlerService;
            _authorizationRepository = authorizationRepository;
            _updateCollectionService = updateCollectionService;
            _selectListItemService = selectListItemService;
            _securityDataService = securityDataService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("EmployeeAddUpdate", new UserViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var employee = input.EntityId > 0 ? _repository.Find<User>(input.EntityId) : new User();
            var availableUserRoles = _repository.FindAll<UserRole>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            IEnumerable<TokenInputDto> selectedUserRoles;
            if (input.EntityId > 0 && employee.UserRoles != null)
                selectedUserRoles = employee.UserRoles.Select(x => new TokenInputDto {id = x.EntityId.ToString(), name = x.Name});
            else selectedUserRoles = availableUserRoles.Where(x => x.name == "Employee");

            var model = Mapper.Map<User, UserViewModel>(employee);
            model.FileUrl = model.FileUrl.IsNotEmpty() ? model.FileUrl.AddImageSizeToName("thumb") : "";
            model._StateList = _selectListItemService.CreateList<State>();
            model._UserLoginInfoStatusList = _selectListItemService.CreateList<Status>();
            model._Title = WebLocalizationKeys.EMPLOYEE_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<EmployeeController>(x => x.Save(null));
            model.UserRoles = new TokenInputViewModel { _availableItems = availableUserRoles, selectedItems = selectedUserRoles };
            return new CustomJsonResult(model);
        }
      
        public ActionResult Display(ViewModel input)
        {
//            var employee = _repository.Find<User>(input.EntityId);
//            var model = new UserViewModel
//                            {
//                                Item = employee,
//                                AddUpdateUrl = UrlContext.GetUrlForAction<EmployeeController>(x => x.AddUpdate(null)) + "/" + employee.EntityId,
//                                _Title = WebLocalizationKeys.EMPLOYEE_INFORMATION.ToString()
//                            };
//            return PartialView("EmployeeView", model);
            return null;
        }

        public ActionResult Delete(ViewModel input)
        {
            var employee = _repository.Find<User>(input.EntityId);
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeleteEmployeeRules");
            var rulesResult = rulesEngineBase.ExecuteRules(employee);
            if(!rulesResult.Success)
            {
                var notification = new RulesNotification(rulesResult);
                return new CustomJsonResult(notification);
            }
            _repository.Delete(employee);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(UserViewModel input)
        {
            User employee;
            if (input.EntityId > 0)
            {
                employee = _repository.Find<User>(input.EntityId);
            }
            else
            {
                employee = new User();
                var clientId = _sessionContext.GetClientId();
                var client = _repository.Find<Client>(clientId);
                employee.Client = client;
            }
            employee = mapToDomain(input, employee);
            ActionResult json;
            if(validateUserRolesAndPasswords(employee, input, out json))
            {
                return json;
            }
            
            mapRolesToGroups(employee);
            handlePassword(input, employee);
            
            if (input.DeleteImage)
            {
                _fileHandlerService.DeleteFile(employee.FileUrl);
                employee.FileUrl = string.Empty;
            }
            if(_fileHandlerService.RequsetHasFile())
            {
                employee.FileUrl = _fileHandlerService.SaveAndReturnUrlForFile(SiteConfig.Config.CustomerPhotosEmployeePath);
            }

            var crudManager = _saveEntityService.ProcessSave(employee);
            var notification = crudManager.Finish();
            return new CustomJsonResult(notification, "text/plain");
        }

        private bool validateUserRolesAndPasswords(User employee, UserViewModel input, out ActionResult json)
        {
            var notification = new Notification { Success = true };
            if (!employee.UserRoles.Any())
            {
                notification.Success=false;
                notification.Errors = new List<ErrorInfo>
                                          {
                                              new ErrorInfo(WebLocalizationKeys.USER_ROLES.ToString(),
                                                            WebLocalizationKeys.SELECT_AT_LEAST_ONE_USER_ROLE.ToString())
                                          };
            }
            if (input.PasswordConfirmation != input.PasswordConfirmation)
            {
                notification.Success = false;
                var errorInfo = new ErrorInfo(WebLocalizationKeys.PASSWORD.ToString(), WebLocalizationKeys.PASSWORD_CONFIRMATION_MUST_MATCH.ToString());
                if (notification.Errors != null) { notification.Errors.Add(errorInfo); }
                else{notification.Errors = new List<ErrorInfo>{errorInfo};}
            }

            json = !notification.Success ? new CustomJsonResult(notification) : null;
            return notification.Success;
        }

        private void mapRolesToGroups(User employee)
        {
            if (employee.UserRoles.Any(x => x.Name == UserType.Administrator.Key))
            {
                _authorizationRepository.AssociateUserWith(employee, UserType.Administrator.Key);
            }
            else
            {
                _authorizationRepository.DetachUserFromGroup(employee, UserType.Administrator.Key);
            }
            if (employee.UserRoles.Any(x => x.Name == UserType.Employee.Key))
            {
                _authorizationRepository.AssociateUserWith(employee, UserType.Employee.Key);
            }
            else
            {
                _authorizationRepository.DetachUserFromGroup(employee, UserType.Employee.Key);
            }
            if (employee.UserRoles.Any(x => x.Name == UserType.Facilities.Key))
            {
                _authorizationRepository.AssociateUserWith(employee, UserType.Facilities.Key);
            }
            else
            {
                _authorizationRepository.DetachUserFromGroup(employee, UserType.Facilities.Key);
            }
            if (employee.UserRoles.Any(x => x.Name == UserType.KYTAdmin.Key))
            {
                _authorizationRepository.AssociateUserWith(employee, UserType.KYTAdmin.Key);
            }
            else
            {
                _authorizationRepository.DetachUserFromGroup(employee, UserType.KYTAdmin.Key);
            }
            if (employee.UserRoles.Any(x => x.Name == UserType.MultiTenant.Key))
            {
                _authorizationRepository.AssociateUserWith(employee, UserType.MultiTenant.Key);
            }
            else
            {
                _authorizationRepository.DetachUserFromGroup(employee, UserType.MultiTenant.Key);
            }
        }

        private User mapToDomain(UserViewModel model, User employee)
        {
            employee.EmployeeId = model.EmployeeId;
            employee.Address1 = model.Address1;
            employee.Address2 = model.Address2;
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.EmergencyContact = model.EmergencyContact;
            employee.EmergencyContactPhone = model.EmergencyContactPhone;
            employee.Email = model.Email;
            employee.PhoneMobile = model.PhoneMobile;
            employee.City = model.City;
            employee.State = model.State;
            employee.ZipCode = model.ZipCode;
            employee.Notes = model.Notes;
            employee.LicenseNumber = model.LicenseNumber;
            if(employee.UserLoginInfo == null)
            {
                employee.UserLoginInfo = new UserLoginInfo();
            }
            employee.UserLoginInfo.LoginName = model.Email;
            _updateCollectionService.Update(employee.UserRoles, model.UserRoles, employee.AddUserRole, employee.RemoveUserRole);
            if (!employee.UserRoles.Any())
            {
                var emp = _repository.Query<UserRole>(x => x.Name == UserType.Employee.ToString()).FirstOrDefault();
                employee.AddUserRole(emp);
            }
            return employee;
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
