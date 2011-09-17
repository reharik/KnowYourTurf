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
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class EmployeeController : AdminControllerBase
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IUploadedFileHandlerService _uploadedFileHandlerService;
        private readonly ISessionContext _sessionContext;

        public EmployeeController(IRepository repository,
            ISaveEntityService saveEntityService,
            IUploadedFileHandlerService uploadedFileHandlerService,
            ISessionContext sessionContext)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _uploadedFileHandlerService = uploadedFileHandlerService;
            _sessionContext = sessionContext;
        }

        public ActionResult AddEdit(ViewModel input)
        {
            var employee = input.EntityId > 0 ? _repository.Find<Employee>(input.EntityId) : new Employee();
            var availableUserRoles = Enumeration.GetAll<UserRole>(true).Select(x => new TokenInputDto { id = x.Key, name = x.Key});
            var selectedUserRoles = employee.UserRoles.IsNotEmpty() 
                ? employee.UserRoles.Split(',').Select(x => new TokenInputDto { id = x, name = x })
                :null;
            
            var model = new EmployeeViewModel
            {
                Employee = employee,
                AvailableRoles = availableUserRoles,
                SelectedRoles = selectedUserRoles
            };
            return PartialView("EmployeeAddUpdate", model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var employee = _repository.Find<Employee>(input.EntityId);
            var model = new EmployeeViewModel
                            {
                                Employee = employee,
                                AddEditUrl = UrlContext.GetUrlForAction<EmployeeController>(x => x.AddEdit(null)) + "/" + employee.EntityId
                            };
            return PartialView("EmployeeView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var employee = _repository.Find<Employee>(input.EntityId);
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

        public ActionResult Save(EmployeeViewModel input)
        {
            Employee employee;
            if (input.Employee.EntityId > 0)
            {
                employee = _repository.Find<Employee>(input.Employee.EntityId);
            }
            else
            {
                employee = new Employee();
                var companyId = _sessionContext.GetCompanyId();
                var company = _repository.Find<Company>(companyId);
                employee.Company = company;
            }
            employee = mapToDomain(input, employee);

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

        private Employee mapToDomain(EmployeeViewModel model, Employee employee)
        {
            var employeeModel = model.Employee;
            employee.EmployeeId = employeeModel.EmployeeId;
            employee.Address1 = employeeModel.Address1;
            employee.Address2= employeeModel.Address2;
            employee.FirstName= employeeModel.FirstName;
            employee.LastName = employeeModel.LastName;
            employee.EmployeeType= employeeModel.EmployeeType;
            employee.EmergencyContact = employeeModel.EmergencyContact;
            employee.EmergencyContactPhone= employeeModel.EmergencyContactPhone;
            employee.Password = employeeModel.Password;
            employee.Email = employeeModel.Email;
            employee.LoginName = employeeModel.Email;
            employee.PhoneMobile = employeeModel.PhoneMobile;
            employee.City = employeeModel.City;
            employee.State = employeeModel.State;
            employee.ZipCode = employeeModel.ZipCode;
            employee.Status= employeeModel.Status;
            employee.Notes = employeeModel.Notes;
            employee.UserRoles = model.RolesInput;//model.UserRoleSelectBoxPickerDto.Selected.Aggregate((i, j) => i + "," + j);
            return employee;
        }
    }
}