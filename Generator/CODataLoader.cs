using System;
using CC.Core.DomainTools;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Domain.Persistence;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;
using StructureMap;

namespace Generator
{
    public interface ICODataLoader
    {
        void Load();
    }

    public class CODataLoader : ICODataLoader
    {
        private IRepository _repository;
        private Company _company;
        private IPurchaseOrderLineItemService _purchaseOrderLineItemService;
        private readonly IInventoryService _inventoryService;
        private readonly ISecurityDataService _securityDataService;
        private User _defaultUser;
        private UserRole _userRoleAdmin;
        private UserRole _userRoleEmployee;
        private UserRole _userRoleFac;
        private DocumentCategory _documentCategory1;
        private DocumentCategory _documentCategory2;
        private TaskType _taskType1;
        private TaskType _taskType2;
        private TaskType _taskType3;
        private TaskType _taskType4;
        private TaskType _taskType5;
        private TaskType _taskType6;
        private TaskType _taskType7;
        private TaskType _taskType8;
        private EquipmentTaskType _equipmentTaskType1;
        private EquipmentTaskType _equipmentTaskType2;
        private EquipmentTaskType _equipmentTaskType3;
        private EquipmentTaskType _equipmentTaskType4;
        private EquipmentType _equipmentType1;
        private EquipmentType _equipmentType2;
        private EquipmentType _equipmentType3;
        private EquipmentType _equipmentType4;
        private EquipmentType _equipmentType5;
        private EquipmentType _equipmentType6;
        private EquipmentType _equipmentType7;
        private EquipmentType _equipmentType8;
        private EquipmentType _equipmentType9;
        private EquipmentType _equipmentType10;
        private EquipmentType _equipmentType11;
        private EquipmentType _equipmentType12;
        private EquipmentType _equipmentType13;
        private EventType _eventType1;
        private EventType _eventType2;
        private PhotoCategory _photoCategory1;
        private PhotoCategory _photoCategory2;
        private Part _part1;
        private Part _part2;
        private IGetCompanyIdService _getCompanyIdService;

        public CODataLoader(IRepository repository,
            IPurchaseOrderLineItemService purchaseOrderLineItemService,
            IInventoryService inventoryService,
            ISecurityDataService securityDataService)
        {
            _repository = repository;
            _purchaseOrderLineItemService = purchaseOrderLineItemService;
            _inventoryService = inventoryService;
            _securityDataService = securityDataService;
        }

        public void Load()
        {
            _getCompanyIdService = ObjectFactory.Container.GetInstance<IGetCompanyIdService>();
            _getCompanyIdService.CompanyId = 4;
            ObjectFactory.Container.Inject(_getCompanyIdService);

            CreateCompany();
            CreateListTypes();
            createUser();
//            createFieldVendor();
            createEquipmentAndVendor();
            CreateEmailTemplate();
            _repository.Commit();
        }

        private void CreateCompany()
        {
            _company = new Company { Name = "TAMU", ZipCode = "77840", TaxRate = 8.25, NumberOfSites = 2 };
            var site1 = new Site { Name = "Chemicals and Fertilizers", Description = "Site 1" };
            var field1 = new Field { Name = "Baseball", Description = "Baseball", Size = 120000, Status = "Active", Abbreviation = "BB" };
            var field2 = new Field { Name = "Football", Description = "Football", Size = 120000, Status = "Active", Abbreviation = "FB" };
            var field3 = new Field { Name = "Football Practice", Description = "Football Practice", Size = 120000, Status = "Active", Abbreviation = "FBPR" };
            var field4 = new Field { Name = "Lacrosse", Description = "Lacrosse", Size = 120000, Status = "Active", Abbreviation = "LAC" };
            var field5 = new Field { Name = "Softball", Description = "Softball", Size = 120000, Status = "Active", Abbreviation = "SB" };
            var field6 = new Field { Name = "Men's Soccer", Description = "Men's Soccer", Size = 120000, Status = "Active", Abbreviation = "MSOC" };
            var field7 = new Field { Name = "Women's Soccer", Description = "Women's Soccer", Size = 120000, Status = "Active", Abbreviation = "WSOC" };
            var field8 = new Field { Name = "Field 8", Description = "Field", Size = 61000, Status = "Active", Abbreviation = "F8" };
            var field9 = new Field { Name = "Field 9", Description = "Field", Size = 61000, Status = "Active", Abbreviation = "F9" };
            var field10 = new Field { Name = "Field 10", Description = "Field", Size = 61000, Status = "Active", Abbreviation = "F10" };
            var field11 = new Field { Name = "Field 11", Description = "Field", Size = 61000, Status = "Active", Abbreviation = "F11" };
            var field12 = new Field { Name = "Field 12", Description = "Field", Size = 61000, Status = "Active", Abbreviation = "F12" };
            var field13 = new Field { Name = "Field 13", Description = "Field", Size = 61000, Status = "Active", Abbreviation = "F13" };
            var field14 = new Field { Name = "Field 14", Description = "Field", Size = 61000, Status = "Active", Abbreviation = "F14" };
            site1.AddField(field1);
            site1.AddField(field2);
            site1.AddField(field3);
            site1.AddField(field4);
            site1.AddField(field5);
            site1.AddField(field6);
            site1.AddField(field7);
            site1.AddField(field8);
            site1.AddField(field9);
            site1.AddField(field10);
            site1.AddField(field11);
            site1.AddField(field12);
            site1.AddField(field13);
            site1.AddField(field14);
            _company.AddSite(site1);
            var site2 = new Site { Name = "Sports Fields", Description = "Site 2" };
            var field15 = new Field { Name = "Baseball", Description = "Baseball", Size = 120000, Status = "Active", Abbreviation = "BB" };
            var field16 = new Field { Name = "Football", Description = "Football", Size = 120000, Status = "Active", Abbreviation = "FB" };
            var field17 = new Field { Name = "Football Practice", Description = "Football Practice", Size = 120000, Status = "Active", Abbreviation = "FBPR" };
            var field18 = new Field { Name = "Lacrosse", Description = "Lacrosse", Size = 120000, Status = "Active", Abbreviation = "LAC" };
            var field19 = new Field { Name = "Softball", Description = "Softball", Size = 120000, Status = "Active", Abbreviation = "SB" };
            var field20 = new Field { Name = "Men's Soccer", Description = "Men's Soccer", Size = 120000, Status = "Active", Abbreviation = "MSOC" };
            var field21 = new Field { Name = "Women's Soccer", Description = "Women's Soccer", Size = 120000, Status = "Active", Abbreviation = "WSOC" };
            var field22 = new Field { Name = "Field 8", Description = "Field", Size = 61000, Status = "Active", Abbreviation = "F8" };
            var field23 = new Field { Name = "Field 9", Description = "Field", Size = 61000, Status = "Active", Abbreviation = "F9" };
            var field24 = new Field { Name = "Field 10", Description = "Field", Size = 61000, Status = "Active", Abbreviation = "F10" };
            var field25 = new Field { Name = "Field 11", Description = "Field", Size = 61000, Status = "Active", Abbreviation = "F11" };
            var field26 = new Field { Name = "Field 12", Description = "Field", Size = 61000, Status = "Active", Abbreviation = "F12" };
            var field27 = new Field { Name = "Field 13", Description = "Field", Size = 61000, Status = "Active", Abbreviation = "F13" };
            var field28 = new Field { Name = "Field 14", Description = "Field", Size = 61000, Status = "Active", Abbreviation = "F14" };
            site2.AddField(field15);
            site2.AddField(field16);
            site2.AddField(field17);
            site2.AddField(field18);
            site2.AddField(field19);
            site2.AddField(field20);
            site2.AddField(field21);
            site2.AddField(field22);
            site2.AddField(field23);
            site2.AddField(field24);
            site2.AddField(field25);
            site2.AddField(field26);
            site2.AddField(field27);
            site2.AddField(field28);
            _company.AddSite(site2);
            _repository.Save(_company);
        }

        private void createUser()
        {
            _userRoleAdmin = new UserRole { Name = UserType.Administrator.ToString(), Status = Status.Active.ToString() };
            _userRoleEmployee = new UserRole { Name = UserType.Employee.ToString(), Status = Status.Active.ToString() };
            _userRoleFac = new UserRole { Name = UserType.Facilities.ToString(), Status = Status.Active.ToString() };
            _repository.Save(_userRoleAdmin);
            _repository.Save(_userRoleEmployee);
            _repository.Save(_userRoleFac);


            var user1 = new User()
            {
                FirstName = "KYTSupport",
                LastName = "TAMU",
                Email = "KYTtamu@kytsoftware.com",
                Company = _company,
                EmployeeId = "123",
                Address1 = "123 street",
                Address2 = "apt a",
                BirthDate = DateTime.Parse("1/5/1972"),
                City = "Austin",
                PhoneHome = "512.123.1234",
                PhoneMobile = "512.123.1234",
                State = "Tx",
                ZipCode = "78701",
            };
            user1.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "KYTtamu"
            };

            user1.UserLoginInfo.Salt = _securityDataService.CreateSalt();
            user1.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("tamu123", user1.UserLoginInfo.Salt);

            user1.AddUserRole(_userRoleAdmin);

            /////////////////////////

            var user2 = new User()
            {
                FirstName = "Larry",
                LastName = "Horn",
                Email = "lhorn@athletics.tamu.edu",
                Company = _company,
                EmployeeId = "0003",
                City = "College Station",
                State = "TX",
                ZipCode = "77840",
            };
            user2.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "lhorn@athletics.tamu.edu"
            };

            user2.UserLoginInfo.Salt = _securityDataService.CreateSalt();
            user2.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("tamu", user2.UserLoginInfo.Salt);

            user2.AddUserRole(_userRoleAdmin);
            user2.AddUserRole(_userRoleEmployee);

            ////////////////////////////

            var user3 = new User()
            {
                FirstName = "Leo",
                LastName = "Goertz",
                Email = "lgoertz@athletics.tamu.edu",
                Company = _company,
                EmployeeId = "14727",
                City = "College Station",
                State = "TX",
                ZipCode = "77840",
            };
            user3.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "lgoertz@athletics.tamu.edu"
            };

            user3.UserLoginInfo.Salt = _securityDataService.CreateSalt();
            user3.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("tamu", user3.UserLoginInfo.Salt);
            user3.AddUserRole(_userRoleAdmin);
            user3.AddUserRole(_userRoleEmployee);

            //////////////

            _repository.Save(user1);
            _repository.Save(user2);
            _repository.Save(user3);
        }

        private void createFieldVendor()
        {
            var vendor = new FieldVendor
            {
                Company = "KYT Supplier1",
                Phone = "555.546.3565",
                Fax = "555.214.3658",
                Website = "http://www.supplies.com/",
                Notes = "notes1"
            };
            var vendorContact = new VendorContact
            {
                Address2 = "4600 Guadalupe St",
                Address1 = "B113",
                City = "Austin",
                Email = "support@deere.com",
                FirstName = "Bill",
                LastName = "Regan",
                Phone = "123.456.7890",
                Fax = "123.456.7890",
                State = "TX",
                Status = "Active"
            };
            vendor.AddContact(vendorContact);
            ///////
            var chemical = new Chemical { Name = "Roundup" };
            var material = new Material { Name = "Sand" };
            var fertilizer = new Fertilizer { Name = "21-14-14", N = 21, P = 14, K = 14 };
            _repository.Save(chemical);
            _repository.Save(material);
            _repository.Save(fertilizer);

            vendor.AddProduct(chemical);
            vendor.AddProduct(material);
            vendor.AddProduct(fertilizer);

            /////// current purchase order
            var purchaseOrder = new PurchaseOrder();
            var purchaseOrderLineItem1 = new PurchaseOrderLineItem { Product = chemical, Price = 10, QuantityOrdered = 2, SizeOfUnit = 1, Taxable = false };
            var purchaseOrderLineItem2 = new PurchaseOrderLineItem { Product = material, Price = 10, QuantityOrdered = 2, SizeOfUnit = 1, Taxable = false };
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder, purchaseOrderLineItem1);
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder, purchaseOrderLineItem2);

            vendor.AddPurchaseOrder(purchaseOrder);

            //// committed purchase order
            var completedPurchaseOrder = new PurchaseOrder();
            var purchaseOrderLineItem3 = new PurchaseOrderLineItem { Product = chemical, Price = 10, QuantityOrdered = 2, TotalReceived = 2, SizeOfUnit = 1, Taxable = false };
            var purchaseOrderLineItem4 = new PurchaseOrderLineItem { Product = material, Price = 10, QuantityOrdered = 2, TotalReceived = 2, SizeOfUnit = 1, Taxable = false };
            _purchaseOrderLineItemService.AddNewItem(ref completedPurchaseOrder, purchaseOrderLineItem3);
            _purchaseOrderLineItemService.AddNewItem(ref completedPurchaseOrder, purchaseOrderLineItem4);
            _inventoryService.ReceivePurchaseOrderLineItem(purchaseOrderLineItem3);
            _inventoryService.ReceivePurchaseOrderLineItem(purchaseOrderLineItem4);
            vendor.AddPurchaseOrder(completedPurchaseOrder);

            _repository.Save(vendor);
        }

        private void createEquipmentAndVendor()
        {
            var vendor = new EquipmentVendor
            {
                Company = "John Deere",
                Phone = "555.123.4567",
                Fax = "123.456.7891",
                Website = "www.deere.com",
                Notes = "notes1"
            };
            var vendorContact = new VendorContact
            {
                Address2 = "4600 Guadalupe St",
                Address1 = "B113",
                City = "College Station",
                Email = "support@deere.com",
                FirstName = "Frank",
                LastName = "Brown",
                Phone = "123.456.7890",
                Fax = "123.456.7890",
                State = "TX",
                Status = "Active"
            };
            vendor.AddContact(vendorContact);

            var equipment = new Equipment
            {
                Name = "Tractor",
                Description = "Tractor",
                EquipmentVendor = vendor,
                EquipmentType = _equipmentType1,
                Make = "John Deere",
                Model = "Z900 Zero Turn Mower",
                SerialNumber = "1234",
                TotalHours = 10,
                Threshold = 1000,
                WarrentyInfo = "No Warrenty"
            };
            _repository.Save(vendor);
            _repository.Save(equipment);
        }

        private void CreateListTypes()
        {
            _documentCategory1 = new DocumentCategory
            {
                Name = "Soil Sample",
                Description = "Soil Sample",
                Status = "Active"
            };

            _documentCategory2 = new DocumentCategory
            {
                Name = "Warranty",
                Description = "Warranty",
                Status = "Active"
            };

            _taskType1 = new TaskType
            {
                Name = "Mow",
                Description = "Mow",
                Status = "Active"
            };

            _taskType2 = new TaskType
            {
                Name = "Water",
                Description = "Water",
                Status = "Active"
            };

            _taskType3 = new TaskType
            {
                Name = "Edge",
                Description = "Edge",
                Status = "Active"
            };

            _taskType4 = new TaskType
            {
                Name = "Aerate",
                Description = "Aerate",
                Status = "Active"
            };
            _taskType5 = new TaskType
            {
                Name = "Blow",
                Description = "Blow",
                Status = "Active"
            };

            _taskType6 = new TaskType
            {
                Name = "Verticut",
                Description = "Verticut",
                Status = "Active"
            };

            _taskType7 = new TaskType
            {
                Name = "Mulch",
                Description = "Mulch",
                Status = "Active"
            };

            _taskType8 = new TaskType
            {
                Name = "Spray",
                Description = "Spray",
                Status = "Active"
            };

            _equipmentTaskType1 = new EquipmentTaskType
            {
                Name = "Oil Change",
                Description = "Oil Change",
                Status = "Active"
            };

            _equipmentTaskType2 = new EquipmentTaskType
            {
                Name = "Wash",
                Description = "Wash",
                Status = "Active"
            };

            _equipmentTaskType3 = new EquipmentTaskType
            {
                Name = "Sharpen Blade",
                Description = "Sharpen Blade",
                Status = "Active"
            };

            _equipmentTaskType4 = new EquipmentTaskType
            {
                Name = "Change Belt",
                Description = "Change Belt",
                Status = "Active"
            };
            _equipmentType1 = new EquipmentType
            {
                Name = "Academics",
                Description = "Academics Dept.",
                Status = "Active"
            };

            _equipmentType2 = new EquipmentType
            {
                Name = "Administration",
                Description = "Athletic Director and staff",
                Status = "Active"
            };

            _equipmentType3 = new EquipmentType
            {
                Name = "Academics",
                Description = "Academics Dept.",
                Status = "Active"
            };

            _equipmentType4 = new EquipmentType
            {
                Name = "Concessions",
                Description = "Athletic Director and staff",
                Status = "Active"
            };

            _equipmentType5 = new EquipmentType
            {
                Name = "Bright Football",
                Description = "Trainers and staff",
                Status = "Active"
            };

            _equipmentType6 = new EquipmentType
            {
                Name = "Equestrian",
                Description = "Equestrian",
                Status = "Active"
            };

            _equipmentType7 = new EquipmentType
            {
                Name = "I.T. Dept.",
                Description = "I.T. Dept.",
                Status = "Active"
            };

            _equipmentType8 = new EquipmentType
            {
                Name = "Mailroom/Inventory Dept.",
                Description = "Mailroom/Inventory Dept.",
                Status = "Active"
            };

            _equipmentType9 = new EquipmentType
            {
                Name = "Marketing Staff",
                Description = "Marketing Staff",
                Status = "Active"
            };

            _equipmentType10 = new EquipmentType
            {
                Name = "Men’s Basketball",
                Description = "Men’s Basketball",
                Status = "Active"
            };

            _equipmentType11 = new EquipmentType
            {
                Name = "Zone Club",
                Description = "Zone Club",
                Status = "Active"
            };

            _equipmentType12 = new EquipmentType
            {
                Name = "12th Man Productions/Video Dept.",
                Description = "12th Man Productions/Video Dept.",
                Status = "Active"
            };

            _equipmentType13 = new EquipmentType
            {
                Name = "West Campus Trainers",
                Description = "West Campus Trainers",
                Status = "Active"
            };
            _eventType1 = new EventType
            {
                Name = "Practice",
                Description = "Practice",
                Status = "Active"
            };

            _eventType2 = new EventType
            {
                Name = "Game",
                Description = "Game",
                Status = "Active"
            };


            _photoCategory1 = new PhotoCategory
            {
                Name = "Fields",
                Description = "Fields",
                Status = "Active"
            };

            _photoCategory2 = new PhotoCategory
            {
                Name = "Equipment",
                Description = "Equipment",
                Status = "Active"
            };

            _part1 = new Part
            {
                Name = "Belt",
                Description = "Belt",
                Status = "Active"
            };

            _part2 = new Part
            {
                Name = "Tire",
                Description = "Tire",
                Status = "Active"
            };
            _repository.Save(_documentCategory1);
            _repository.Save(_documentCategory2);
            _repository.Save(_taskType1);
            _repository.Save(_taskType2);
            _repository.Save(_taskType3);
            _repository.Save(_taskType4);
            _repository.Save(_taskType5);
            _repository.Save(_taskType6);
            _repository.Save(_taskType7);
            _repository.Save(_taskType8);
            _repository.Save(_equipmentTaskType1);
            _repository.Save(_equipmentTaskType2);
            _repository.Save(_equipmentTaskType3);
            _repository.Save(_equipmentTaskType4);
            _repository.Save(_equipmentType1);
            _repository.Save(_equipmentType2);
            _repository.Save(_equipmentType3);
            _repository.Save(_equipmentType4);
            _repository.Save(_equipmentType5);
            _repository.Save(_equipmentType6);
            _repository.Save(_equipmentType7);
            _repository.Save(_equipmentType8);
            _repository.Save(_equipmentType9);
            _repository.Save(_equipmentType10);
            _repository.Save(_equipmentType11);
            _repository.Save(_equipmentType12);
            _repository.Save(_equipmentType13);
            _repository.Save(_eventType1);
            _repository.Save(_eventType2);
            _repository.Save(_photoCategory1);
            _repository.Save(_photoCategory2);
            _repository.Save(_part1);
            _repository.Save(_part2);
        }

        private void CreateEmailTemplate()
        {
            var template = new EmailTemplate
            {
                Name = "EmployeeDailyTask",
                Template =
                    "<p>Hi {%=name%},</p><p>Here are your tasks for {%=data%}:</p><p>{%=tasks%}</p><p>Thank you,</p><p>Management</p>"
            };
            _repository.Save(template);

            var template2 = new EmailTemplate
            {
                Name = "EquipmentMaintenanceNotification",
                Template =
                    "<p>Hi {%=name%},</p><p>Your {%=equipmentName%} has passed the Total Hours limit you identified.</p><p>Please create a Task and update the threshold as needed.</p><p>Thank you,</p><p>Management</p>"
            };
            _repository.Save(template2);
        }
    }


}
