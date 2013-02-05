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
        private Client _client;
        private User _defaultUser;
        private UserRole _userRoleAdmin;
        private UserRole _userRoleEmployee;
        private UserRole _userRoleFac;
        private Site _category1;

        public void Load(IRepository repository)
        {
            _repository = repository;
            CreateClient();
            LoadForClient(_client.EntityId);
            _repository.UnitOfWork.Commit();

        }

        private void LoadForClient(int clientId)
        {
            CreateUserRoles(clientId);
            CreateUser(clientId);

            CreateField(clientId);
            CreateEmailTemplate(clientId);
        }

        private void CreateUserRoles(int clientId)
        {
            _userRoleAdmin = new UserRole
            {
                Name = UserType.Administrator.ToString(),
                ClientId = clientId
            };
            _userRoleEmployee = new UserRole
            {
                Name = UserType.Employee.ToString(),
                ClientId = clientId
            };
            _userRoleFac = new UserRole
            {
                Name = UserType.Facilities.ToString(),
                ClientId = clientId
            };
            _repository.Save(_userRoleAdmin);
            _repository.Save(_userRoleEmployee);
            _repository.Save(_userRoleFac);
        }

        private void CreateEmailTemplate(int clientId)
        {
            var template = new EmailTemplate
                                    {
                                        Name = "EmployeeDailyTask",
                                        Template =
                                            "<p>Hi {%=name%},</p><p>Here are your tasks for {%=data%}:</p><p>{%=tasks%}</p><p>Thank you,</p><p>Management</p>",
                                        ClientId = clientId
                                    };
            _repository.Save(template);

            var template2 = new EmailTemplate
            {
                Name = "EquipmentMaintenanceNotification",
                Template =
                    "<p>Hi {%=name%},</p><p>Your {%=equipmentName%} has passed the Total Hours limit you identified.</p><p>Please create a Task and update the threshold as needed.</p><p>Thank you,</p><p>Management</p>",
                ClientId = clientId
            };
            _repository.Save(template2);
        }

        private void CreateClient()
        {
            _client = new Client { Name = "KYT", ZipCode = "78702", TaxRate = 8.25,NumberOfSites = 1};
            _category1 = new Site { Name = "Site 1" };
            _client.AddSite(_category1);
            _repository.Save(_client);
        }

        private void CreateUser(int clientId)
        {
            _defaultUser = new User()
            {
                FirstName = "KYT",
                LastName = "User",
                Email = "support@KYTSoftware.com",
                Client = _client,
                ClientId = clientId,
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
                                                 ClientId = clientId
                                             };
            _defaultUser.AddUserRole(_userRoleAdmin);
            _defaultUser.AddUserRole(_userRoleEmployee);
            _repository.Save(_defaultUser);

        }

        private void CreateField(int clientId)
        {
            _field1 = new Field
            {
                Name = "field1",
                Description = "field1",
                Size = 22000,
                Abbreviation = "f1",
                ClientId = clientId
            };

            _category1.AddField(_field1);
            _repository.Save(_category1);
        }
    }
}
