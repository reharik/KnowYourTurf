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

    public interface IFCDataLoader
    {
        void Load();
    }

    public class FCDataLoader : IFCDataLoader
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

        public FCDataLoader(IRepository repository,
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
            _getCompanyIdService.CompanyId = 3;
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
            _company = new Company { Name = "FC Dallas", ZipCode = "75034", TaxRate = 8.25, NumberOfSites = 2 };
            var site1 = new Site { Name = "Soccer Fields", Description = "Site 1" };
            var field1 = new Field { Name = "Stadium", Description = "Game Field", Size = 120000, Status = "Active", Abbreviation = "STAD" };
            var field2 = new Field { Name = "Main Practice", Description = "Main Practice Field", Size = 120000, Status = "Active", Abbreviation = "MP" };
            var field3 = new Field { Name = "Field 1", Description = "Field 1", Size = 120000, Status = "Active", Abbreviation = "F-1" };
            var field4 = new Field { Name = "Field 2", Description = "Field 2", Size = 120000, Status = "Active", Abbreviation = "F-2" };
            var field5 = new Field { Name = "Field 3", Description = "Field 3", Size = 120000, Status = "Active", Abbreviation = "F-3" };
            var field6 = new Field { Name = "Field 4", Description = "Field 4", Size = 120000, Status = "Active", Abbreviation = "F-4" };
            var field7 = new Field { Name = "Field 5", Description = "Field 5", Size = 120000, Status = "Active", Abbreviation = "F-5" };
            var field8 = new Field { Name = "Field 6", Description = "Field 6", Size = 120000, Status = "Active", Abbreviation = "F-6" };
            var field9 = new Field { Name = "Field 7", Description = "Field 7", Size = 120000, Status = "Active", Abbreviation = "F-7" };
            var field10 = new Field { Name = "Field 8", Description = "Field 8", Size = 120000, Status = "Active", Abbreviation = "F-8" };
            var field11 = new Field { Name = "Field 9", Description = "Field 9", Size = 120000, Status = "Active", Abbreviation = "F-9" };
            var field12 = new Field { Name = "Field 10", Description = "Field 10", Size = 120000, Status = "Active", Abbreviation = "F-10" };
            var field13 = new Field { Name = "Field 11", Description = "Field 11", Size = 120000, Status = "Active", Abbreviation = "F-11" };
            var field14 = new Field { Name = "Field 12", Description = "Field 12", Size = 120000, Status = "Active", Abbreviation = "F-12" };
            var field15 = new Field { Name = "Field 13", Description = "Field 13", Size = 120000, Status = "Active", Abbreviation = "F-13" };
            var field16 = new Field { Name = "Field 14", Description = "Field 14", Size = 120000, Status = "Active", Abbreviation = "F-14" };
            var field17 = new Field { Name = "Field 15", Description = "Field 15", Size = 120000, Status = "Active", Abbreviation = "F-15" };
            var field18 = new Field { Name = "Field 16", Description = "Field 16", Size = 120000, Status = "Active", Abbreviation = "F-16" };
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
            site1.AddField(field15);
            site1.AddField(field16);
            site1.AddField(field17);
            site1.AddField(field18);
            _company.AddSite(site1);
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
                FirstName = "Miles",
                LastName = "Studhalter",
                Email = "mstudhalter@fcdallas.net",
                Company = _company,
                EmployeeId = "0001",
                Address1 = "123 street",
                Address2 = "apt a",
                BirthDate = DateTime.Parse("1/5/1972"),
                City = "75034",
                PhoneHome = "512.123.1234",
                PhoneMobile = "512.123.1234",
                State = "Tx",
                ZipCode = "75034",
            };
            user1.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "mstudhalter@fcdallas.net"
            };

            user1.UserLoginInfo.Salt = _securityDataService.CreateSalt();
            user1.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("fcd", user1.UserLoginInfo.Salt);

            user1.AddUserRole(_userRoleAdmin);
            user1.AddUserRole(_userRoleEmployee);

            /////////////////////////

            var user2 = new User()
            {
                FirstName = "Allen",
                LastName = "Reed",
                Email = "areed@fcdallas.net",
                Company = _company,
                EmployeeId = "0002",
                Address1 = "1256 Park Ave.",
                Address2 = "Bldg. B",
                BirthDate = DateTime.Parse("10/15/1972"),
                City = "Frisco",
                PhoneHome = "512.225.3658",
                PhoneMobile = "512.325.1254",
                State = "Tx",
                ZipCode = "75034",
            };
            user2.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "areed@fcdallas.net"
            };

            user2.UserLoginInfo.Salt = _securityDataService.CreateSalt();
            user2.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("fcd", user2.UserLoginInfo.Salt);

            user2.AddUserRole(_userRoleAdmin);
            user2.AddUserRole(_userRoleEmployee);



            //////////////

            _repository.Save(user1);

        }

        private void createFieldVendor()
        {
            var vendor = new FieldVendor
            {
                Company = "Vendor 1",
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
                Email = "support@vendor1.com",
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
//            var purchaseOrder = new PurchaseOrder();
//            var purchaseOrderLineItem1 = new PurchaseOrderLineItem { Product = chemical, Price = 10, QuantityOrdered = 2, SizeOfUnit = 1, Taxable = false };
//            var purchaseOrderLineItem2 = new PurchaseOrderLineItem { Product = material, Price = 10, QuantityOrdered = 2, SizeOfUnit = 1, Taxable = false };
//            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder, purchaseOrderLineItem1);
//            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder, purchaseOrderLineItem2);
//
//            vendor.AddPurchaseOrder(purchaseOrder);
//
//            //// committed purchase order
//            var completedPurchaseOrder = new PurchaseOrder();
//            var purchaseOrderLineItem3 = new PurchaseOrderLineItem { Product = chemical, Price = 10, QuantityOrdered = 2, TotalReceived = 2, SizeOfUnit = 1, Taxable = false };
//            var purchaseOrderLineItem4 = new PurchaseOrderLineItem { Product = material, Price = 10, QuantityOrdered = 2, TotalReceived = 2, SizeOfUnit = 1, Taxable = false };
//            _purchaseOrderLineItemService.AddNewItem(ref completedPurchaseOrder, purchaseOrderLineItem3);
//            _purchaseOrderLineItemService.AddNewItem(ref completedPurchaseOrder, purchaseOrderLineItem4);
//            _inventoryService.ReceivePurchaseOrderLineItem(purchaseOrderLineItem3);
//            _inventoryService.ReceivePurchaseOrderLineItem(purchaseOrderLineItem4);
//            vendor.AddPurchaseOrder(completedPurchaseOrder);

            _repository.Save(vendor);
        }

        private void createEquipmentAndVendor()
        {
            var vendor = new EquipmentVendor
            {
                Company = "Tractor Co.",
                Phone = "555.123.4567",
                Fax = "123.456.7891",
                Website = "www.tractor.com",
                Notes = "notes1"
            };
            var vendorContact = new VendorContact
            {
                Address2 = "4600 Guadalupe St",
                Address1 = "B113",
                City = "Austin",
                Email = "support@tractor.com",
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
                Name = "Stadium",
                Description = "Stadium",
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

