using System;
using CC.Core.DomainTools;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;
using StructureMap;

namespace Generator
{
    public class QA_Base_Objects_DataLoader
    {
        private IRepository _repository;
        private Field _field1;
        private FieldVendor _vendor1;
        private Material _materials1;
        private Fertilizer _fertilizer1;
        private Chemical _chemical1;
        private InventoryProduct _inventoryChemical1;
        private InventoryProduct _inventoryFertilizer1;
        private InventoryProduct _invenotyMaterial1;
        private Equipment _equip1;
        private Equipment _equip2;
        private static VendorContact _contact1v1;
        private static VendorContact _contact2v1;
        private Company _company;
        private User _defaultUser;
        private UserRole _userRoleAdmin;
        private UserRole _userRoleEmployee;
        private UserRole _userRoleFac;
        private Site _category1;
        private EquipmentType _equipmentType1;

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
            CreateEquipmentType(companyId);
            CreateEquipment(companyId);
            CreateChemical(companyId);
            CreateMaterials(companyId);
            CreateFertilizer(companyId);
            CreateInventory(companyId);
            CreateEventType(companyId);
//
            CreateVendor(companyId);
            CreateVendorContact(companyId);
            CreateCalculator(companyId);
            CreateDocumentCategory(companyId);
            CreatePhotoCategory(companyId);
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

        private void CreateDocumentCategory(int companyId)
        {
            var category = new DocumentCategory
            {
                Name = "Field",
                Description = "pictures of fields",
                Status = Status.Active.ToString(),
                CompanyId = companyId
            };
            var category2 = new DocumentCategory
            {
                Name = "People",
                Description = "pictures of people",
                Status = Status.Active.ToString(),
                CompanyId = companyId
            };
            _repository.Save(category);
            _repository.Save(category2);

        }

        private void CreatePhotoCategory(int companyId)
        {
            var category = new PhotoCategory
            {
                Name = "Field",
                Description = "pictures of fields",
                Status = Status.Active.ToString(),
                CompanyId = companyId
            };
            var category2 = new PhotoCategory
            {
                Name = "People",
                Description = "pictures of people",
                Status = Status.Active.ToString(),
                CompanyId = companyId
            };
            _repository.Save(category);
            _repository.Save(category2);

        }


        private void CreateCalculator(int companyId)
        {
            var fertilizerNeeded = new Calculator
            {
                Name = "FertilizerNeeded",
                CompanyId = companyId
            };
            var materials = new Calculator
            {
                Name = "Materials",
                CompanyId = companyId
            };
            var sand = new Calculator
            {
                Name = "Sand",
                CompanyId = companyId
            };
            var overseedBagsNeeded = new Calculator
            {
                Name = "OverseedBagsNeeded",
                CompanyId = companyId
            };
            var overseedRateNeeded = new Calculator
            {
                Name = "OverseedRateNeeded",
                CompanyId = companyId
            };
            var fertilizerUsed = new Calculator
            {
                Name = "FertilizerUsed",
                CompanyId = companyId
            };
            _repository.Save(fertilizerNeeded);
            _repository.Save(fertilizerUsed);
            _repository.Save(materials);
            _repository.Save(sand);
            _repository.Save(overseedBagsNeeded );
            _repository.Save(overseedRateNeeded);
        }

        private void CreateCompany()
        {
            _company = new Company { Name = "KYT", ZipCode = "78702", TaxRate = 8.25,NumberOfSites = 2};
            _category1 = new Site { Name = "Field 1" };
//            _category2 = new Site { Name = "Field 2" };
            _company.AddSite(_category1);
//            _company.AddCategory(_category2);

            _repository.Save(_company);
        }

        private void CreateEquipmentType(int companyId)
        {
            _equipmentType1 = new EquipmentType
                                  {
                                      Name = "type 1",
                                      CompanyId = companyId,
                                      Status = "Active"
                                  };
            _repository.Save(_equipmentType1);
        }


        private void CreateEquipment(int companyId)
        {
            _equip1 = new Equipment
                             {
                                 Name = "Truck",
                                 EquipmentType = _equipmentType1,
                                 CompanyId = companyId
                             };
            _equip2 = new Equipment
                             {
                                 Name = "Plane",
                                 EquipmentType = _equipmentType1,
                                 CompanyId = companyId
                             };
            _repository.Save(_equip1);
            _repository.Save(_equip2);
        }

        private void CreateUser(int companyId)
        {
            _defaultUser = new User()
            {
                EmployeeId = "123",
                Address1 = "123 street",
                Address2 = "apt a",
                BirthDate = DateTime.Parse("1/5/1972"),
                City = "Austin",
                Email = "support@kytsoftware.com",
                FirstName = "KYT",
                LastName = "User",
                PhoneHome = "123.123.1234",
                PhoneMobile = "123.123.1234",
                State = "Tx",
                ZipCode = "12345",
                Company = _company,
                CompanyId = companyId
            };
            _defaultUser.UserLoginInfo = new UserLoginInfo
                                             {
                                                 LoginName = "Admin",
                                                 Password = "123",
                                                 CompanyId = companyId
                                             };
            _defaultUser.AddUserRole(_userRoleAdmin);
            _defaultUser.AddUserRole(_userRoleEmployee);
            
            var employeeUser = new User()
            {
                EmployeeId = "1234",
                Address1 = "123 street",
                Address2 = "apt a",
                BirthDate = DateTime.Parse("1/5/1972"),
                City = "Austin",
                Email = "support@kytsoftware.com",
                FirstName = "KYT",
                LastName = "Employee",
                PhoneHome = "123.123.1234",
                PhoneMobile = "123.123.1234",
                State = "Tx",
                ZipCode = "12345",
                Company = _company,
                CompanyId = companyId
            };
            employeeUser.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "Employee",
                Password = "123",
                CompanyId = companyId
            };
            employeeUser.AddUserRole(_userRoleAdmin);
            employeeUser.AddUserRole(_userRoleEmployee);

            var facilities = new User()
            {
                EmployeeId = "1234",
                Address1 = "123 street",
                Address2 = "apt a",
                BirthDate = DateTime.Parse("1/5/1972"),
                City = "Austin",
                Email = "support@kytsoftware.com",
                FirstName = "KYT",
                LastName = "Faclilities",
                PhoneHome = "123.123.1234",
                PhoneMobile = "123.123.1234",
                State = "Tx",
                ZipCode = "12345",
                Company = _company,
                CompanyId = companyId
            };
            facilities.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "fac",
                Password = "123",
                CompanyId = companyId
            };
            facilities.AddUserRole(_userRoleFac);


            _repository.Save(_defaultUser);
            _repository.Save(employeeUser);
            _repository.Save(facilities);

        }

        private void CreateField(int companyId)
        {
            _field1 = new Field
            {
                Name = "field 1",
                Description = "Field 1",
                Size = 22000,
                Abbreviation = "F 1",
                CompanyId = companyId
            };

            _category1.AddField(_field1);
            _repository.Save(_category1);
        }

        private void CreateEventType(int companyId)
        {
            var eventType1 = new EventType
            {
                Name = "some event",
                Status = Status.Active.ToString(),
                CompanyId = companyId
            };
            var eventType2 = new EventType
            {
                Name = "some other event",
                Status = Status.Active.ToString(),
                CompanyId = companyId
            };
            _repository.Save(eventType1);
            _repository.Save(eventType2);
        }

        private void CreateVendor(int companyId)
        {
            _vendor1 = new FieldVendor
            {
                Company = "KYT Vendor",
                Phone = "555.123.4567",
                Fax = "123.456.7891",
                Website = "www.kytvendor.com",
                LogoUrl = "kytvendor",
                Notes = "notes 1",
                CompanyId = companyId
            };

            _vendor1.AddProduct(_fertilizer1);

            _vendor1.AddProduct(_chemical1);

            _vendor1.AddProduct(_materials1);

            _repository.Save(_vendor1);
        }

        private void CreateVendorContact(int companyId)
        {
            _contact1v1 = new VendorContact
                                 {
                                     Address2 = "4600 Guadalupe St",
                                     Address1 = "B113",
                                     City = "Austin",
                                     Email = "support@kytsoftware.com",
                                     FirstName = "Contact",
                                     LastName = "One",
                                     Phone = "123.456.7890",
                                     Fax = "123.456.7890",
                                     State = "RI",
                                     Status = "Active",
                                     CompanyId = companyId
                                 };
          
            _contact2v1 = new VendorContact
            {
                Address2 = "4600 Guadalupe St",
                Address1 = "B113",
                City = "Austin",
                Email = "support@kytsoftware.com",
                FirstName = "Contact",
                LastName = "Two",
                Phone = "123.456.7890",
                Fax = "123.456.7890",
                State = "Tx",
                Status = "Active",
                CompanyId = companyId
            };
          
            _vendor1.AddContact(_contact1v1);
            _vendor1.AddContact(_contact2v1);
           
            _repository.Save(_vendor1);
        }

        public void CreateInventory(int companyId)
        {
            _inventoryChemical1 = new InventoryProduct()
            {
                Quantity = 10,
                UnitType = UnitType.Tons.Key,
                SizeOfUnit = 10,
                CompanyId = companyId
            };
         
            _inventoryFertilizer1 = new InventoryProduct()
            {
                Quantity = 10,
                UnitType = UnitType.Tons.Key,
                SizeOfUnit = 10,
                CompanyId = companyId
            };
          
            _invenotyMaterial1 = new InventoryProduct()
            {
                Quantity = 10,
                UnitType = UnitType.Tons.Key,
                SizeOfUnit = 10,
                CompanyId = companyId
            };
            _inventoryChemical1.Product = _chemical1;
            _inventoryFertilizer1.Product = _fertilizer1;
            _invenotyMaterial1.Product = _materials1;
            
            _repository.Save(_inventoryChemical1);
            _repository.Save(_inventoryFertilizer1);
            _repository.Save(_invenotyMaterial1);
        }

        private void CreateMaterials(int companyId)
        {
            _materials1 = new Material
            {
                Name = "Kryptonite",
                CompanyId = companyId
            };

            _repository.Save(_materials1);
        }

        private void CreateFertilizer(int companyId)
        {
            _fertilizer1 = new Fertilizer
            {
                Name = "Manure",
                N = 10,
                P = 10,
                K = 10,
                CompanyId = companyId
            };

            _repository.Save(_fertilizer1);
        }

        private void CreateChemical(int companyId)
        {
            _chemical1 = new Chemical()
            {
                Name = "HCL",
                CompanyId = companyId
            };

            _repository.Save(_chemical1);
        }
    }
}
