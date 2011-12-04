using System.Collections.Generic;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;
using System.Linq;
using Rhino.Security.Interfaces;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class EmployeeController : AdminControllerBase
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IUploadedFileHandlerService _uploadedFileHandlerService;
        private readonly ISessionContext _sessionContext;
        private readonly IAuthorizationRepository _authorizationRepository;

        public EmployeeController(IRepository repository,
            ISaveEntityService saveEntityService,
            IUploadedFileHandlerService uploadedFileHandlerService,
            ISessionContext sessionContext,
            IAuthorizationRepository authorizationRepository)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _uploadedFileHandlerService = uploadedFileHandlerService;
            _sessionContext = sessionContext;
            _authorizationRepository = authorizationRepository;
        }

        public ActionResult AddEdit(ViewModel input)
        {
            var employee = input.EntityId > 0 ? _repository.Find<User>(input.EntityId) : new User();
            var availableUserRoles = Enumeration.GetAll<UserRole>(true).Select(x => new TokenInputDto { id = x.Key, name = x.Key});
            IEnumerable<TokenInputDto> selectedUserRoles;
            if (input.EntityId > 0 && employee.UserLoginInfo.UserRoles.IsNotEmpty())
                selectedUserRoles =
                    employee.UserLoginInfo.UserRoles.Split(',').Select(x => new TokenInputDto {id = x, name = x});
            else selectedUserRoles = null;

            var model = new UserViewModel
            {
                User = employee,
                AvailableItems = availableUserRoles,
                SelectedItems = selectedUserRoles,
                Title = WebLocalizationKeys.EMPLOYEE_INFORMATION.ToString()
            };
            return PartialView("EmployeeAddUpdate", model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var employee = _repository.Find<User>(input.EntityId);
            var model = new UserViewModel
                            {
                                User = employee,
                                AddEditUrl = UrlContext.GetUrlForAction<EmployeeController>(x => x.AddEdit(null)) + "/" + employee.EntityId,
                                Title = WebLocalizationKeys.EMPLOYEE_INFORMATION.ToString()
                            };
            return PartialView("EmployeeView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var employee = _repository.Find<User>(input.EntityId);
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeleteEmployeeRules");
            var rulesResult = rulesEngineBase.ExecuteRules(employee);
            if(!rulesResult.Success)
            {
                Notification notification = new Notification(rulesResult);
                return Json(notification);
            }
            _repository.SoftDelete(employee);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(UserViewModel input)
        {
            User employee;
            if (input.User.EntityId > 0)
            {
                employee = _repository.Find<User>(input.User.EntityId);
            }
            else
            {
                employee = new User();
                var companyId = _sessionContext.GetCompanyId();
                var company = _repository.Find<Company>(companyId);
                employee.Company = company;
            }
            employee = mapToDomain(input, employee);
            mapRolesToGroups(employee);
            if (input.DeleteImage)
            {
                _uploadedFileHandlerService.DeleteFile(employee.ImageUrl);
                employee.ImageUrl = string.Empty;
            }

            var serverDirectory = "/CustomerPhotos/" + _sessionContext.GetCompanyId() + "/Employees";
            employee.ImageUrl = _uploadedFileHandlerService.GetUploadedFileUrl(serverDirectory, employee.FirstName+"_"+employee.LastName);
            var crudManager = _saveEntityService.ProcessSave(employee);

            crudManager = _uploadedFileHandlerService.SaveUploadedFile(serverDirectory, employee.FirstName + "_" + employee.LastName, crudManager);
            var notification = crudManager.Finish();
            return Json(notification,"text/plain");
        }

        private void mapRolesToGroups(User employee)
        {
            _authorizationRepository.DetachUserFromGroup(employee, UserRole.Administrator.Key);
            _authorizationRepository.DetachUserFromGroup(employee, UserRole.Employee.Key);
            _authorizationRepository.DetachUserFromGroup(employee, UserRole.Facilities.Key);
            _authorizationRepository.DetachUserFromGroup(employee, UserRole.KYTAdmin.Key);
            _authorizationRepository.DetachUserFromGroup(employee, UserRole.MultiTenant.Key);

            employee.UserLoginInfo.UserRoles.Split(',').Each(x =>
            {
                if (x == UserRole.Administrator.Key)
                {
                    _authorizationRepository.AssociateUserWith(employee, UserRole.Administrator.Key);
                }
                if (x == UserRole.Employee.Key)
                {
                    _authorizationRepository.AssociateUserWith(employee, UserRole.Employee.Key);
                }
                if (x == UserRole.Facilities.Key)
                {
                    _authorizationRepository.AssociateUserWith(employee, UserRole.Facilities.Key);
                }
                if (x == UserRole.KYTAdmin.Key)
                {
                    _authorizationRepository.AssociateUserWith(employee, UserRole.KYTAdmin.Key);
                }
                if (x == UserRole.MultiTenant.Key)
                {
                    _authorizationRepository.AssociateUserWith(employee, UserRole.MultiTenant.Key);
                }
            });
        }

        private User mapToDomain(UserViewModel model, User employee)
        {
            var employeeModel = model.User;
            employee.EmployeeId = employeeModel.EmployeeId;
            employee.Address1 = employeeModel.Address1;
            employee.Address2= employeeModel.Address2;
            employee.FirstName= employeeModel.FirstName;
            employee.LastName = employeeModel.LastName;
            employee.EmergencyContact = employeeModel.EmergencyContact;
            employee.EmergencyContactPhone= employeeModel.EmergencyContactPhone;
            employee.Email = employeeModel.Email;
            employee.PhoneMobile = employeeModel.PhoneMobile;
            employee.City = employeeModel.City;
            employee.State = employeeModel.State;
            employee.ZipCode = employeeModel.ZipCode;
            employee.Notes = employeeModel.Notes;
            employee.UserLoginInfo = new UserLoginInfo()
            {
                Password = employeeModel.UserLoginInfo.Password,
                LoginName = employeeModel.Email,
                Status = employeeModel.UserLoginInfo.Status,
                UserRoles = model.RolesInput.IsNotEmpty() ? model.RolesInput : UserRole.Employee.ToString(),
                UserType = UserRole.Employee.ToString(),
            }; return employee;
        }
    }
}