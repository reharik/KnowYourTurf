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
        private EquipmentTaskType _equipmentTaskType1;
        private EquipmentTaskType _equipmentTaskType2;
        private EquipmentType _equipmentType1;
        private EquipmentType _equipmentType2;
        private EventType _eventType1;
        private EventType _eventType2;
        private PhotoCategory _photoCategory1;
        private PhotoCategory _photoCategory2;
        private Part _part1;
        private Part _part2;

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
            _company = new Company {Name = "KYT", ZipCode = "78702", TaxRate = 8.25, NumberOfSites = 2};
            var site = new Site {Name = "Site 1",Description = "Site 1"};
            var field = new Field {Name = "Field 1", Description = "Field 1", Size = 10000, Status = "Active", Abbreviation = "F1"};
            site.AddField(field);
            _company.AddSite(site);
            _repository.Save(_company);
        }

        private void createUser()
        {
            _defaultUser = new User()
            {
                FirstName = "KYT",
                LastName = "User",
                Email = "support@KYTSoftware.com",
                Company = _company,
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
                LoginName = "Admin"
            };

            _defaultUser.UserLoginInfo.Salt = _securityDataService.CreateSalt();
            _defaultUser.UserLoginInfo.Password = _securityDataService.CreatePasswordHash("123",
                                                        _defaultUser.UserLoginInfo.Salt);

            _userRoleAdmin = new UserRole {Name = UserType.Administrator.ToString(), Status = Status.Active.ToString()};
            _userRoleEmployee = new UserRole {Name = UserType.Employee.ToString(), Status = Status.Active.ToString()};
            _userRoleFac = new UserRole {Name = UserType.Facilities.ToString(), Status = Status.Active.ToString()};
            _repository.Save(_userRoleAdmin);
            _repository.Save(_userRoleEmployee);
            _repository.Save(_userRoleFac);
            _defaultUser.AddUserRole(_userRoleAdmin);
            _defaultUser.AddUserRole(_userRoleEmployee);
            _repository.Save(_defaultUser);
        }

        private void createFieldVendor()
        {
            var vendor = new FieldVendor
                           {
                               Company = "KYT Field Supplies",
                               Phone = "555.123.4567",
                               Fax = "123.456.7891",
                               Website = "www.kytfieldsupplies.com",
                               Notes = "notes1"
                           };
            var vendorContact = new VendorContact
                                    {
                                        Address2 = "4600 Guadalupe St",
                                        Address1 = "B113",
                                        City = "Austin",
                                        Email = "support@kytsoftware.com",
                                        FirstName = "contact",
                                        LastName = "one",
                                        Phone = "123.456.7890",
                                        Fax = "123.456.7890",
                                        State = "RI",
                                        Status = "Active"
                                    };
            vendor.AddContact(vendorContact);
            ///////
            var chemical = new Chemical {Name = "HCL"};
            var material = new Material { Name = "Kryptonite" };
            var fertilizer = new Fertilizer { Name = "Manure", N = 10, P = 10, K = 10 };
            _repository.Save(chemical);
            _repository.Save(material);
            _repository.Save(fertilizer);

            vendor.AddProduct(chemical);
            vendor.AddProduct(material);
            vendor.AddProduct(fertilizer);

            /////// current purchase order
            var purchaseOrder = new PurchaseOrder();
            var purchaseOrderLineItem1 = new PurchaseOrderLineItem {Product = chemical, Price = 10, QuantityOrdered = 2, SizeOfUnit = 1, Taxable = false};
            var purchaseOrderLineItem2 = new PurchaseOrderLineItem {Product = material, Price = 10, QuantityOrdered = 2, SizeOfUnit = 1,Taxable = false};
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder,purchaseOrderLineItem1);
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder,purchaseOrderLineItem2);
            
            vendor.AddPurchaseOrder(purchaseOrder);

            //// committed purchase order
            var completedPurchaseOrder = new PurchaseOrder();
            var purchaseOrderLineItem3 = new PurchaseOrderLineItem {Product = chemical, Price = 10, QuantityOrdered = 2, TotalReceived = 2, SizeOfUnit = 1, Taxable = false};
            var purchaseOrderLineItem4 = new PurchaseOrderLineItem {Product = material, Price = 10, QuantityOrdered = 2, TotalReceived = 2, SizeOfUnit = 1,Taxable = false};
            _purchaseOrderLineItemService.AddNewItem(ref completedPurchaseOrder,purchaseOrderLineItem3);
            _purchaseOrderLineItemService.AddNewItem(ref completedPurchaseOrder,purchaseOrderLineItem4);
            _inventoryService.ReceivePurchaseOrderLineItem(purchaseOrderLineItem3);
            _inventoryService.ReceivePurchaseOrderLineItem(purchaseOrderLineItem4);
            vendor.AddPurchaseOrder(completedPurchaseOrder);

            _repository.Save(vendor);
        }

        private void createEquipmentAndVendor()
        {
            var vendor = new EquipmentVendor
            {
                Company = "KYT Equipment Vendor",
                Phone = "555.123.4567",
                Fax = "123.456.7891",
                Website = "www.kytequipmentvendor.com",
                Notes = "notes1"
            };
            var vendorContact = new VendorContact
            {
                Address2 = "4600 Guadalupe St",
                Address1 = "B113",
                City = "Austin",
                Email = "support@kytsoftware.com",
                FirstName = "contact",
                LastName = "one",
                Phone = "123.456.7890",
                Fax = "123.456.7890",
                State = "RI",
                Status = "Active"
            };
            vendor.AddContact(vendorContact);

            var equipment = new Equipment
                                {
                                    Name = "Tractor",
                                    Description = "Tractor",
                                    EquipmentVendor = vendor,
                                    EquipmentType = _equipmentType1,
                                    Make = "Ford",
                                    Model = "Model-T",
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
                Name = "Schedule",
                Description = "Schedule",
                Status = "Active"
            };

            _documentCategory2 = new DocumentCategory
            {
                Name = "Instructions",
                Description = "Instructions",
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

            _equipmentType1 = new EquipmentType
            {
                Name = "Tractor",
                Description = "Tractor",
                Status = "Active"
            };

            _equipmentType2 = new EquipmentType
            {
                Name = "Shovel",
                Description = "Shovel",
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
                Name = "Landscape",
                Description = "Landscape",
                Status = "Active"
            };

            _photoCategory2 = new PhotoCategory
            {
                Name = "Portrate",
                Description = "Portrate",
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
            _repository.Save(_equipmentTaskType1);
            _repository.Save(_equipmentTaskType2);
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
