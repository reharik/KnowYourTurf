using System;
using CC.Core.DomainTools;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;

namespace Generator
{
    public class MinDataLoader
    {
        private IRepository _repository;
        private Field _field1;
        private Company _company;
        private User _defaultUser;
        private UserRole _userRoleAdmin;
        private UserRole _userRoleEmployee;
        private UserRole _userRoleFac;
        private Site _category1;

        public void Load(IRepository repository)
        {
            _repository = repository;
            CreateCompany();
            LoadForCompany(_company.EntityId);
            _repository.UnitOfWork.Commit();

        }

        private void LoadForCompany(int companyId)
        {
            CreateUserRoles(companyId);
            CreateUser(companyId);

            CreateField(companyId);
            CreateEmailTemplate(companyId);
        }

        private void CreateUserRoles(int companyId)
        {
            _userRoleAdmin = new UserRole
            {
                Name = UserType.Administrator.ToString(),
                CompanyId = companyId
            };
            _userRoleEmployee = new UserRole
            {
                Name = UserType.Employee.ToString(),
                CompanyId = companyId
            };
            _userRoleFac = new UserRole
            {
                Name = UserType.Facilities.ToString(),
                CompanyId = companyId
            };
            _repository.Save(_userRoleAdmin);
            _repository.Save(_userRoleEmployee);
            _repository.Save(_userRoleFac);
        }

        private void CreateEmailTemplate(int companyId)
        {
            var template = new EmailTemplate
                                    {
                                        Name = "EmployeeDailyTask",
                                        Template =
                                            "<p>Hi {%=name%},</p><p>Here are your tasks for {%=data%}:</p><p>{%=tasks%}</p><p>Thank you,</p><p>Management</p>",
                                        CompanyId = companyId
                                    };
            _repository.Save(template);

            var template2 = new EmailTemplate
            {
                Name = "EquipmentMaintenanceNotification",
                Template =
                    "<p>Hi {%=name%},</p><p>Your {%=equipmentName%} has passed the Total Hours limit you identified.</p><p>Please create a Task and update the threshold as needed.</p><p>Thank you,</p><p>Management</p>",
                CompanyId = companyId
            };
            _repository.Save(template2);
        }

        private void CreateCompany()
        {
            _company = new Company { Name = "KYT", ZipCode = "78702", TaxRate = 8.25,NumberOfSites = 1};
            _category1 = new Site { Name = "Site 1" };
            _company.AddSite(_category1);
            _repository.Save(_company);
        }

        private void CreateUser(int companyId)
        {
            _defaultUser = new User()
            {
                FirstName = "KYT",
                LastName = "User",
                Email = "support@KYTSoftware.com",
                Company = _company,
                CompanyId = companyId,
                EmployeeId = "123",
                Address1 = "123 street",
                Address2 = "apt a",
                BirthDate = DateTime.Parse("1/5/1972"),
                City = "Austin",
                PhoneHome = "123.123.1234",
                PhoneMobile = "123.123.1234",
                State = "Tx",
                ZipCode = "12345",
            };
            _defaultUser.UserLoginInfo = new UserLoginInfo
                                             {
                                                 LoginName = "Admin",
                                                 Password = "123",
                                                 Status = "Active",
                                                 CompanyId = companyId
                                             };
            _defaultUser.AddUserRole(_userRoleAdmin);
            _defaultUser.AddUserRole(_userRoleEmployee);
            _repository.Save(_defaultUser);

        }

        private void CreateField(int companyId)
        {
            _field1 = new Field
            {
                Name = "field1",
                Description = "field1",
                Size = 22000,
                Abbreviation = "f1",
                CompanyId = companyId
            };

            _category1.AddField(_field1);
            _repository.Save(_category1);
        }
    }
}
