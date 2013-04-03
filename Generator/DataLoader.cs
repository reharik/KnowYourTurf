using System;
using CC.Core.DomainTools;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;
using StructureMap;

namespace Generator
{
    public class DataLoader
    {
        private IRepository _repository;
        private Field _field1;
        private Field _field2;
        private Task _task1;
        private Task _task2;
        private FieldVendor _vendor2;
        private FieldVendor _vendor1;
        private Material _materials1;
        private Material _materials2;
        private Fertilizer _fertilizer2;
        private Fertilizer _fertilizer1;
        private Chemical _chemical1;
        private Chemical _chemical2;
        private InventoryProduct _inventoryChemical1;
        private InventoryProduct _inventoryChemical2;
        private InventoryProduct _inventoryFertilizer1;
        private InventoryProduct _inventoryFertilizer2;
        private InventoryProduct _invenotyMaterial1;
        private InventoryProduct _inventoryMaterial2;
        private Equipment _equip1;
        private Equipment _equip2;
        private User _employee1;
        private User _employee2;
        private static VendorContact _contact1v1;
        private static VendorContact _contact2v1;
        private static VendorContact _contact1v2;
        private static VendorContact _contact2v2;
        private Client _client;
        private IPurchaseOrderLineItemService _purchaseOrderLineItemService;
        private User _defaultUser;
        private User _employeeAdmin1;
        private User _employeeAdmin2;
        private UserRole _userRoleAdmin;
        private UserRole _userRoleEmployee;
        private UserRole _userRoleFac;
        private Site _category1;
        private Site _category2;
        private Field _field3;
        private Field _field4;
        private Task _task3;
        private Task _task4;
        private EquipmentType _equipmentType1;
        private ISecurityDataService _securityDataService;

        public void Load(IRepository repository, ISecurityDataService securityDataService)
        {
            _repository = repository;
            _securityDataService = securityDataService;
            //genregistry has no clientfilter and special getclientidservice
//            _repository = ObjectFactory.Container.GetInstance<IRepository>();
//            _repository.UnitOfWork.Initialize();
            _purchaseOrderLineItemService = new PurchaseOrderLineItemService(null);

            CreateClient();
            LoadForClient(_client.EntityId);
//            LoadForClient(_client.EntityId);
            
            _repository.UnitOfWork.Commit();

        }

        private void LoadForClient(int clientId)
        {
            CreateUserRoles(clientId);
            CreateUser(clientId);

            CreateEmployee(clientId);
            CreateField(clientId);
            CreateEquipmentType(clientId);
            CreateEquipment(clientId);
            CreateChemical(clientId);
            CreateMaterials(clientId);
            CreateFertilizer(clientId);
            CreateInventory(clientId);
            CreateTask(clientId);
            CreateEventType(clientId);
//
            CreateVendor(clientId);
            CreateVendorContact(clientId);
            CreateCalculator(clientId);
            CreateDocumentCategory(clientId);
            CreatePhotoCategory(clientId);
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


        private void CreateDocumentCategory(int clientId)
        {
            var category = new DocumentCategory
            {
                Name = "Field",
                Description = "pictures of fields",
                Status = Status.Active.ToString(),
                ClientId = clientId
            };
            var category2 = new DocumentCategory
            {
                Name = "People",
                Description = "pictures of people",
                Status = Status.Active.ToString(),
                ClientId = clientId
            };
            _repository.Save(category);
            _repository.Save(category2);

        }

        private void CreatePhotoCategory(int clientId)
        {
            var category = new PhotoCategory
            {
                Name = "Field",
                Description = "pictures of fields",
                Status = Status.Active.ToString(),
                ClientId = clientId
            };
            var category2 = new PhotoCategory
            {
                Name = "People",
                Description = "pictures of people",
                Status = Status.Active.ToString(),
                ClientId = clientId
            };
            _repository.Save(category);
            _repository.Save(category2);

        }


        private void CreateCalculator(int clientId)
        {
            var fertilizerNeeded = new Calculator
            {
                Name = "FertilizerNeeded",
                ClientId = clientId
            };
            var materials = new Calculator
            {
                Name = "Materials",
                ClientId = clientId
            };
            var sand = new Calculator
            {
                Name = "Sand",
                ClientId = clientId
            };
            var overseedBagsNeeded = new Calculator
            {
                Name = "OverseedBagsNeeded",
                ClientId = clientId
            };
            var overseedRateNeeded = new Calculator
            {
                Name = "OverseedRateNeeded",
                ClientId = clientId
            };
            var fertilizerUsed = new Calculator
            {
                Name = "FertilizerUsed",
                ClientId = clientId
            };
            _repository.Save(fertilizerNeeded);
            _repository.Save(fertilizerUsed);
            _repository.Save(materials);
            _repository.Save(sand);
            _repository.Save(overseedBagsNeeded );
            _repository.Save(overseedRateNeeded);
        }

        private void CreateClient()
        {
            _client = new Client { Name = "KYT", ZipCode = "78702", TaxRate = 8.25,NumberOfSites = 2};
            _category1 = new Site { Name = "Field 1" };
//            _category2 = new Site { Name = "Field 2" };
            _client.AddSite(_category1);
//            _client.AddCategory(_category2);

            _repository.Save(_client);
        }

        private void CreateEquipmentType(int clientId)
        {
            _equipmentType1 = new EquipmentType
                                  {
                                      Name = "type 1",
                                      ClientId = clientId,
                                      Status = "Active"
                                  };
            _repository.Save(_equipmentType1);
        }


        private void CreateEquipment(int clientId)
        {
            _equip1 = new Equipment
                             {
                                 Name = "Truck",
                                 EquipmentType = _equipmentType1,
                                 ClientId = clientId
                             };
            _equip2 = new Equipment
                             {
                                 Name = "Plane",
                                 EquipmentType = _equipmentType1,
                                 ClientId = clientId
                             };
            _repository.Save(_equip1);
            _repository.Save(_equip2);
        }

        private void CreateUser(int clientId)
        {
            _defaultUser = new User()
            {
                
                FirstName = "Raif",
                LastName = "Harik",
                Client = _client,
                ClientId = clientId
            };
            _defaultUser.UserLoginInfo = new UserLoginInfo
                                             {
                                                 LoginName = "Admin",
                                                 ClientId = clientId
                                             };
            var salt1 = _securityDataService.CreateSalt();
            _defaultUser.UserLoginInfo.Salt = salt1;
            _defaultUser.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("123", salt1);
            _defaultUser.AddUserRole(_userRoleAdmin);
            _defaultUser.AddUserRole(_userRoleEmployee);
            var altUser = new User()
            {
                FirstName = "Amahl",
                LastName = "Harik",
                Client = _client,
                ClientId = clientId
            };
            altUser.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "alt",
                ClientId = clientId
            };
            var salt2 = _securityDataService.CreateSalt();
            altUser.UserLoginInfo.Salt = salt2;
            altUser.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("alt", salt2);
            altUser.AddUserRole(_userRoleAdmin);
            altUser.AddUserRole(_userRoleEmployee);

            var facilities = new User()
            {
                FirstName = "Amahl",
                LastName = "Harik",
                ClientId = clientId
            };
            facilities.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "facilities",
                ClientId = clientId
            };
            var salt3 = _securityDataService.CreateSalt();
            facilities.UserLoginInfo.Salt = salt3;
            facilities.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("facilities", salt3);
            facilities.AddUserRole(_userRoleFac);


            _repository.Save(_defaultUser);
            _repository.Save(altUser);
            _repository.Save(facilities);

        }

        private void CreateEmployee(int clientId)
        {
            _employee1 = new User()
            {
                EmployeeId = "123",
                Address1 = "123 street",
                Address2 = "apt a",
                BirthDate = DateTime.Parse("1/5/1972"),
                City = "Austin",
                Email = "reharik@gmail.com",
                FirstName = "Raif",
                LastName = "Harik",
                PhoneHome = "123.123.1234",
                PhoneMobile = "123.123.1234",
                State = "Tx",
                ZipCode = "12345",
                Client = _client,
                ClientId = clientId
                };
            _employee1.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "reharik@gmail.com",
                ClientId = clientId
            };
            var salt1 = _securityDataService.CreateSalt();
            _employee1.UserLoginInfo.Salt = salt1;
            _employee1.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("123", salt1);

            _employee2 = new User()
            {
                EmployeeId = "1234",
                Address1 = "123 street",
                Address2 = "apt a",
                BirthDate = DateTime.Parse("1/5/1972"),
                City = "Austin",
                Email = "amahl@gmail.com",
                FirstName = "Amahl",
                LastName = "Harik",
                PhoneHome = "123.123.1234",
                PhoneMobile = "123.123.1234",
                State = "Tx",
                ZipCode = "12345",
                Client = _client,
                ClientId = clientId
                };
            _employee2.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "amahl@gmail.com",
                Password = "123",
                ClientId = clientId
            };
            var salt2 = _securityDataService.CreateSalt();
            _employee2.UserLoginInfo.Salt = salt2;
            _employee2.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("123", salt2);

            _employeeAdmin1 = new User()
            {
                EmployeeId = "1234",
                Address1 = "123 street",
                Address2 = "apt a",
                BirthDate = DateTime.Parse("1/5/1972"),
                City = "Austin",
                Email = "mark@gmail.com",
                FirstName = "mark",
                LastName = "lara",
                PhoneHome = "123.123.1234",
                PhoneMobile = "123.123.1234",
                State = "Tx",
                ZipCode = "12345",
                Client = _client,
                ClientId = clientId
            };
            _employeeAdmin1.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "mark@gmail.com",
                Password = "123",
                ClientId = clientId
            };
            var salt3 = _securityDataService.CreateSalt();
            _employeeAdmin1.UserLoginInfo.Salt = salt3;
            _employeeAdmin1.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("123", salt3);

            _employeeAdmin2 = new User()
            {
                EmployeeId = "1234",
                Address1 = "123 street",
                Address2 = "apt a",
                BirthDate = DateTime.Parse("1/5/1972"),
                City = "Austin",
                Email = "chris@gmail.com",
                FirstName = "chris",
                LastName = "chris",
                PhoneHome = "123.123.1234",
                PhoneMobile = "123.123.1234",
                State = "Tx",
                ZipCode = "12345",
                Client = _client,
                ClientId = clientId
            };
            _employeeAdmin2.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "chris@gmail.com",
                Password = "123",
                ClientId = clientId
            };
            var salt4 = _securityDataService.CreateSalt();
            _employeeAdmin2.UserLoginInfo.Salt = salt4;
            _employeeAdmin2.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("123", salt4);

            
            
            _employee1.AddUserRole(_userRoleEmployee);
            _employee2.AddUserRole(_userRoleEmployee);
            _employeeAdmin1.AddUserRole(_userRoleEmployee);
            _employeeAdmin2.AddUserRole(_userRoleEmployee);
            _employeeAdmin1.AddUserRole(_userRoleAdmin);
            _employeeAdmin2.AddUserRole(_userRoleAdmin);

            _repository.Save(_employee1);
            _repository.Save(_employee2);
            _repository.Save(_employeeAdmin1);
            _repository.Save(_employeeAdmin2);
        }

        private void CreateField(int clientId)
        {
            _field1 = new Field
            {
                Name = "field1",
                Description = "SomeField1",
                Size = 22000,
                Abbreviation = "SFF",
                ClientId = clientId
            };

            _field2 = new Field
            {
                Name = "field2",
                Description = "SomeField2",
                Size = 120000,
                Abbreviation = "SOFF",
                ClientId = clientId
            };
            _field3 = new Field
            {
                Name = "field3",
                Description = "SomeField1",
                Size = 22000,
                Abbreviation = "SFF",
                ClientId = clientId
            };

            _field4 = new Field
            {
                Name = "field3",
                Description = "SomeField2",
                Size = 120000,
                Abbreviation = "SOFF",
                ClientId = clientId
            };


            _category1.AddField(_field1);
            _category1.AddField(_field2);
//            _category2.AddField(_field3);
//            _category2.AddField(_field4);
            _repository.Save(_category1);
//            _repository.Save(_category2);
        }

        private void CreateEventType(int clientId)
        {
            var eventType1 = new EventType
            {
                Name = "some event",
                Status = Status.Active.ToString(),
                ClientId = clientId
            };
            var eventType2 = new EventType
            {
                Name = "some other event",
                Status = Status.Active.ToString(),
                ClientId = clientId
            };
            _repository.Save(eventType1);
            _repository.Save(eventType2);
        }

       

        private void CreateTask(int clientId)
        {
            var taskType1 = new TaskType
            {
                Name = "Mow",
                Status = Status.Active.ToString(),
                ClientId = clientId
            };
            var taskType2 = new TaskType
            {
                Name = "Water",
                Status = Status.Active.ToString(),
                ClientId = clientId
            };
            _repository.Save(taskType1);
            _repository.Save(taskType2);
            _task1 = new Task
            {
                TaskType = taskType1,
                ScheduledDate = DateTime.Parse("3/3/2011"),
                StartTime = DateTime.Parse("3/3/2011 5:30 AM"),
                EndTime = DateTime.Parse("3/3/2011 6:30 AM"),
                Notes = "Notes1",
                QuantityNeeded = 4,
                UnitType = UnitType.Tons.ToString(),
                ClientId = clientId
            };
            _task1.Field =_field1;
            _task1.InventoryProduct = _inventoryMaterial2;

            _task2 = new Task
            {
                TaskType = taskType2,
                ScheduledDate = DateTime.Parse("3/3/2011"),
                StartTime = DateTime.Parse("3/3/2011 6:30 AM"),
                EndTime = DateTime.Parse("3/3/2011 7:30 AM"),
                Notes = "Notes2",
                QuantityNeeded = 4,
                UnitType = UnitType.Tons.ToString(),
                ClientId = clientId
            };
            _task2.Field = _field2;
            _task2.InventoryProduct = _inventoryChemical2;

            _task1.AddEmployee(_employee1);
            _task1.AddEmployee(_employee2);
            _task1.AddEquipment(_equip1);
            _task1.AddEquipment(_equip2);
            _task2.AddEmployee(_employee1);
            _task2.AddEmployee(_employee2);
            _task2.AddEquipment(_equip1);
            _task2.AddEquipment(_equip2);


            _task3 = new Task
            {
                TaskType = taskType1,
                ScheduledDate = DateTime.Parse("3/3/2011"),
                StartTime = DateTime.Parse("3/3/2011 5:30 AM"),
                EndTime = DateTime.Parse("3/3/2011 6:30 AM"),
                Notes = "Notes1",
                QuantityNeeded = 4,
                UnitType = UnitType.Tons.ToString(),
                ClientId = clientId
            };
            _task3.Field = _field3;
            _task3.InventoryProduct = _invenotyMaterial1;

            _task4 = new Task
            {
                TaskType = taskType2,
                ScheduledDate = DateTime.Parse("3/3/2011"),
                StartTime = DateTime.Parse("3/3/2011 6:30 AM"),
                EndTime = DateTime.Parse("3/3/2011 7:30 AM"),
                Notes = "Notes2",
                QuantityNeeded = 4,
                UnitType = UnitType.Tons.ToString(),
                ClientId = clientId
            };
            _task4.Field = _field4;
            _task4.InventoryProduct = _inventoryChemical2;

            _task3.AddEmployee(_employee1);
            _task3.AddEmployee(_employee2);
            _task3.AddEquipment(_equip1);
            _task3.AddEquipment(_equip2);
            _task4.AddEmployee(_employee1);
            _task4.AddEmployee(_employee2);
            _task4.AddEquipment(_equip1);
            _task4.AddEquipment(_equip2);

            _repository.Save(_task1);
            _repository.Save(_task2);
//            _repository.Save(_task3);
//            _repository.Save(_task4);

        }

        private void CreateVendor(int clientId)
        {
            _vendor1 = new FieldVendor
            {
                Company = "Some Client1",
                Phone = "555.123.4567",
                Fax = "123.456.7891",
                Website = "www.somewebsite1.com",
                LogoUrl = "someurl1",
                Notes = "notes1",
                ClientId = clientId
            };

            _vendor2 = new FieldVendor
            {
                Company = "Some Client2",
                Phone = "555.123.4567",
                Fax = "123.456.7891",
                Website = "www.somewebsite2.com",
                LogoUrl = "someurl2",
                Notes = "notes2",
                ClientId = clientId
            };
//
            _vendor1.AddProduct(_fertilizer1);
            _vendor1.AddProduct(_fertilizer2);
            _vendor2.AddProduct(_fertilizer1);
            _vendor2.AddProduct(_fertilizer2);

            _vendor1.AddProduct(_chemical1);
            _vendor1.AddProduct(_chemical2);
            _vendor2.AddProduct(_chemical1);
            _vendor2.AddProduct(_chemical2);

            _vendor1.AddProduct(_materials1);
            _vendor1.AddProduct(_materials2);
            _vendor2.AddProduct(_materials1);
            _vendor2.AddProduct(_materials2);


            _repository.Save(_vendor1);
            _repository.Save(_vendor2);

            var purchaseOrder1 = new PurchaseOrder
            {
                CreatedDate = DateTime.Parse("1/5/2009"),
                ClientId = clientId
            };
            purchaseOrder1.Vendor = _vendor1;
            var poli1 = new PurchaseOrderLineItem()
            {
                Price = 10,
                QuantityOrdered = 5,
                UnitType = UnitType.Bags.ToString(),
                SizeOfUnit = 5,
                Taxable = false,
                ClientId = clientId
            };
            poli1.Product = _fertilizer1;

            var poli2 = new PurchaseOrderLineItem()
            {
                Price = 10,
                UnitType = UnitType.Bags.ToString(),
                QuantityOrdered = 5,
                SizeOfUnit = 5,
                Taxable = false,
                ClientId = clientId
            };
            poli2.Product = _fertilizer1;

            var poli3 = new PurchaseOrderLineItem()
            {
                Price = 10,
                QuantityOrdered = 5,
                UnitType = UnitType.Bags.ToString(),
                SizeOfUnit = 5,
                Taxable = false,
                ClientId = clientId
            };
            poli3.Product = _materials1;

            var poli4 = new PurchaseOrderLineItem()
            {
                Price = 10,
                QuantityOrdered = 5,
                UnitType = UnitType.Bags.ToString(),
                SizeOfUnit = 5,
                Taxable = false,
                ClientId = clientId
            };
            poli4.Product = _materials2;

            var poli5 = new PurchaseOrderLineItem()
            {
                Price = 10,
                QuantityOrdered = 5,
                UnitType = UnitType.Bags.ToString(),
                SizeOfUnit = 5,
                Taxable = false,
                ClientId = clientId
            };
            poli5.Product =_chemical2;

            var poli6 = new PurchaseOrderLineItem()
            {
                Price = 10,
                UnitType = UnitType.Bags.ToString(),
                QuantityOrdered = 5,
                SizeOfUnit = 5,
                Taxable = false,
                ClientId = clientId
            };
            poli6.Product = _chemical1;

            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder1, poli1);
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder1, poli2);
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder1, poli3);
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder1, poli4);
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder1, poli5);
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder1, poli6);
            _vendor1.AddPurchaseOrder(purchaseOrder1);
            _repository.Save(_vendor1);

        }

        private void CreateVendorContact(int clientId)
        {
            _contact1v1 = new VendorContact
                                 {
                                     Address2 = "4600 Guadalupe St",
                                     Address1 = "B113",
                                     City = "Austin",
                                     Email = "amahl@gmail.com",
                                     FirstName = "Amahl",
                                     LastName = "Harik",
                                     Phone = "512.228.6069",
                                     Fax = "512.228.60690",
                                     State = "RI",
                                     Status = "Active",
                                     ClientId = clientId
                                 };
            _contact1v2 = new VendorContact
                             {
                                 Address2 = "4600 Guadalupe St",
                                 Address1 = "B113",
                                 City = "Austin",
                                 Email = "amahl@gmail.com",
                                 FirstName = "Amahl",
                                 LastName = "Harik",
                                 Phone = "512.228.6069",
                                 Fax = "512.228.60690",
                                 State = "RI",
                                 Status = "Active",
                                 ClientId = clientId
                             };
            _contact2v1 = new VendorContact
            {
                Address2 = "4600 Guadalupe St",
                Address1 = "B113",
                City = "Austin",
                Email = "reharik@gmail.com",
                FirstName = "Raif",
                LastName = "Harik",
                Phone = "512.228.6069",
                Fax = "512.228.60690",
                State = "Tx",
                Status = "Active",
                ClientId = clientId
            };
            _contact2v2 = new VendorContact
            {
                Address2 = "4600 Guadalupe St",
                Address1 = "B113",
                City = "Austin",
                Email = "reharik@gmail.com",
                FirstName = "Raif",
                LastName = "Harik",
                Phone = "512.228.6069",
                Fax = "512.228.60690",
                State = "Tx",
                Status = "Active",
                ClientId = clientId
            };
            _vendor1.AddContact(_contact1v1);
            _vendor1.AddContact(_contact2v1);
            _vendor2.AddContact(_contact1v2);
            _vendor2.AddContact(_contact2v2);
            
            _repository.Save(_vendor1);
            _repository.Save(_vendor2);
        }

        public void CreateInventory(int clientId)
        {

            _inventoryChemical1 = new InventoryProduct()
            {
                Quantity = 10,
                UnitType = UnitType.Tons.Key,
                SizeOfUnit = 10,
                ClientId = clientId
            };
            _inventoryChemical2 = new InventoryProduct()
            {
                Quantity = 10,
                UnitType = UnitType.Tons.Key,
                SizeOfUnit = 10,
                ClientId = clientId
            };
            _inventoryFertilizer1 = new InventoryProduct()
            {
                Quantity = 10,
                UnitType = UnitType.Tons.Key,
                SizeOfUnit = 10,
                ClientId = clientId
            };
            _inventoryFertilizer2 = new InventoryProduct()
            {
                Quantity = 10,
                UnitType = UnitType.Tons.Key,
                SizeOfUnit = 10,
                ClientId = clientId
            };
            _invenotyMaterial1 = new InventoryProduct()
            {
                Quantity = 10,
                UnitType = UnitType.Tons.Key,
                SizeOfUnit = 10,
                ClientId = clientId
            };
            _inventoryMaterial2 = new InventoryProduct()
            {
                Quantity = 10,
                UnitType = UnitType.Tons.Key,
                SizeOfUnit = 10,
                ClientId = clientId
            };
            _inventoryChemical1.Product = _chemical1;
            _inventoryChemical2.Product = _chemical2;
            _inventoryFertilizer1.Product = _fertilizer1;
            _inventoryFertilizer2.Product = _fertilizer2;
            _invenotyMaterial1.Product = _materials1;
            _inventoryMaterial2.Product = _materials2;
            
            _repository.Save(_inventoryChemical1);
            _repository.Save(_inventoryChemical2);
            _repository.Save(_inventoryFertilizer1);
            _repository.Save(_inventoryFertilizer2);
            _repository.Save(_invenotyMaterial1);
            _repository.Save(_inventoryMaterial2);
        }

        private void CreateMaterials(int clientId)
        {
            _materials1 = new Material
            {
                Name = "Kryptonite",
                ClientId = clientId
            };

            _materials2 = new Material
            {
                Name = "FoolsGold",
                ClientId = clientId
            };

            _repository.Save(_materials1);
            _repository.Save(_materials2);
        }

        private void CreateFertilizer(int clientId)
        {
            _fertilizer1 = new Fertilizer
            {
                Name = "cow poop",
                N = 10,
                P = 10,
                K = 10,
                ClientId = clientId
            };

            _fertilizer2 = new Fertilizer
            {
                Name = "Chicken poop",
                N = 10,
                P = 10,
                K = 10,
                ClientId = clientId
            };

            _repository.Save(_fertilizer1);
            _repository.Save(_fertilizer2);
        }

        private void CreateChemical(int clientId)
        {
            _chemical1 = new Chemical()
            {
                Name = "Lsd",
                ClientId = clientId
            };

            _chemical2 = new Chemical()
            {
                Name = "PCP",
                ClientId = clientId
            };

            _repository.Save(_chemical1);
            _repository.Save(_chemical2);
        }



    }
}
