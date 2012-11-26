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
    public interface IQADataLoader
    {
        void Load();
    }

    public class QADataLoader : IQADataLoader
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
        private EventType _eventType1;
        private EventType _eventType2;
        private PhotoCategory _photoCategory1;
        private PhotoCategory _photoCategory2;
        private Part _part1;
        private Part _part2;
        private IGetCompanyIdService _getCompanyIdService;

        public QADataLoader(IRepository repository,
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
            _getCompanyIdService.CompanyId = 1;
            ObjectFactory.Container.Inject(_getCompanyIdService);

            CreateCompany();
            CreateListTypes();
            createUser();
            createFieldVendor();
            createEquipmentAndVendor();
            CreateEmailTemplate();
            _repository.Commit();
        }

        private void CreateCompany()
        {
            _company = new Company { Name = "KYT - Demo", ZipCode = "78701", TaxRate = 8.25, NumberOfSites = 2 };
            var site1 = new Site { Name = "Sports Fields", Description = "Site 1" };
            var field1 = new Field { Name = "Baseball", Description = "Baseball", Size = 120000, Status = "Active", Abbreviation = "BB" };
            var field2 = new Field { Name = "Football", Description = "Football", Size = 120000, Status = "Active", Abbreviation = "FB" };
            var field3 = new Field { Name = "Football Practice", Description = "Football Practice", Size = 120000, Status = "Active", Abbreviation = "FBPR" };
            var field4 = new Field { Name = "Lacrosse", Description = "Lacrosse", Size = 120000, Status = "Active", Abbreviation = "LAC" };
            var field5 = new Field { Name = "Softball", Description = "Softball", Size = 120000, Status = "Active", Abbreviation = "SB" };
            var field6 = new Field { Name = "Men's Soccer", Description = "Men's Soccer", Size = 120000, Status = "Active", Abbreviation = "MSOC" };
            var field7 = new Field { Name = "Women's Soccer", Description = "Women's Soccer", Size = 120000, Status = "Active", Abbreviation = "WSOC" };
            site1.AddField(field1);
            site1.AddField(field2);
            site1.AddField(field3);
            site1.AddField(field4);
            site1.AddField(field5);
            site1.AddField(field6);
            site1.AddField(field7);
            _company.AddSite(site1);
            var site2 = new Site { Name = "KYT Golf Course", Description = "Site 2" };
            var field8 = new Field { Name = "Hole 1", Description = "Hole 1", Size = 120000, Status = "Active", Abbreviation = "H-1" };
            var field9 = new Field { Name = "Hole 2", Description = "Hole 2", Size = 120000, Status = "Active", Abbreviation = "H-2" };
            var field10 = new Field { Name = "Hole 3", Description = "Hole 3", Size = 120000, Status = "Active", Abbreviation = "H-3" };
            var field11 = new Field { Name = "Hole 4", Description = "Hole 4", Size = 120000, Status = "Active", Abbreviation = "H-4" };
            var field12 = new Field { Name = "Hole 5", Description = "Hole 5", Size = 120000, Status = "Active", Abbreviation = "H-5" };
            var field13 = new Field { Name = "Hole 6", Description = "Hole 6", Size = 120000, Status = "Active", Abbreviation = "H-6" };
            var field14 = new Field { Name = "Hole 7", Description = "Hole 7", Size = 120000, Status = "Active", Abbreviation = "H-7" };
            var field15 = new Field { Name = "Hole 8", Description = "Hole 8", Size = 120000, Status = "Active", Abbreviation = "H-8" };
            var field16 = new Field { Name = "Hole 9", Description = "Hole 9", Size = 120000, Status = "Active", Abbreviation = "H-9" };
            var field17 = new Field { Name = "Hole 10", Description = "Hole 10", Size = 120000, Status = "Active", Abbreviation = "H-10" };
            var field18 = new Field { Name = "Hole 11", Description = "Hole 11", Size = 120000, Status = "Active", Abbreviation = "H-11" };
            var field19 = new Field { Name = "Hole 12", Description = "Hole 12", Size = 120000, Status = "Active", Abbreviation = "H-12" };
            var field20 = new Field { Name = "Hole 13", Description = "Hole 13", Size = 120000, Status = "Active", Abbreviation = "H-13" };
            var field21 = new Field { Name = "Hole 14", Description = "Hole 14", Size = 120000, Status = "Active", Abbreviation = "H-14" };
            var field22 = new Field { Name = "Hole 15", Description = "Hole 15", Size = 120000, Status = "Active", Abbreviation = "H-15" };
            var field23 = new Field { Name = "Hole 16", Description = "Hole 16", Size = 120000, Status = "Active", Abbreviation = "H-16" };
            var field24 = new Field { Name = "Hole 17", Description = "Hole 17", Size = 120000, Status = "Active", Abbreviation = "H-17" };
            var field25 = new Field { Name = "Hole 18", Description = "Hole 18", Size = 120000, Status = "Active", Abbreviation = "H-18" };
            site2.AddField(field8);
            site2.AddField(field9);
            site2.AddField(field10);
            site2.AddField(field11);
            site2.AddField(field12);
            site2.AddField(field13);
            site2.AddField(field14);
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
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@KYTSoftware.com",
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
                LoginName = "john.smith@KYTSoftware.com"
            };

            user1.UserLoginInfo.Salt = _securityDataService.CreateSalt();
            user1.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("123", user1.UserLoginInfo.Salt);

            user1.AddUserRole(_userRoleAdmin);
            user1.AddUserRole(_userRoleEmployee);

            /////////////////////////

            var user2 = new User()
            {
                FirstName = "Tom",
                LastName = "Davis",
                Email = "tom.davis@KYTSoftware.com",
                Company = _company,
                EmployeeId = "100234",
                Address1 = "1256 Park Ave.",
                Address2 = "Bldg. B",
                BirthDate = DateTime.Parse("10/15/1972"),
                City = "Austin",
                PhoneHome = "512.225.3658",
                PhoneMobile = "512.325.1254",
                State = "Tx",
                ZipCode = "78701",
            };
            user2.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "tom.davis@KYTSoftware.com"
            };

            user2.UserLoginInfo.Salt = _securityDataService.CreateSalt();
            user2.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("123", user2.UserLoginInfo.Salt);

            user2.AddUserRole(_userRoleAdmin);
            user2.AddUserRole(_userRoleEmployee);

            ////////////////////////////

            var user3 = new User()
            {
                FirstName = "KYT",
                LastName = "Demo",
                Email = "kyt.demo@KYTSoftware.com",
                Company = _company,
                EmployeeId = "14727",
                Address1 = "1568 Straight Dr.",
                Address2 = "Apt. #4",
                BirthDate = DateTime.Parse("2/16/1972"),
                City = "Austin",
                PhoneHome = "512.236.3758",
                PhoneMobile = "512.345.1654",
                State = "Tx",
                ZipCode = "78701",
            };
            user3.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "KYTDemo"
            };

            user3.UserLoginInfo.Salt = _securityDataService.CreateSalt();
            user3.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("123", user3.UserLoginInfo.Salt);
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
                City = "Austin",
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
                Name = "Tractor",
                Description = "Tractor",
                Status = "Active"
            };

            _equipmentType2 = new EquipmentType
            {
                Name = "Mower",
                Description = "Mower",
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
                Name = "Parade",
                Description = "Parade",
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
                Name = "Course",
                Description = "Course",
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
