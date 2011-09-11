using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.UnitTests
{
    public static class ObjectMother
    {
        public static Field ValidField(string name)
        {
            return new Field
                       {
                           Description = "a field",
                           Name = name,
                           Size = 1000,
                           Status = "Active"
                       };
        }

        public static Task ValidTask(string name)
        {
            return new Task
                       {
                           ActualTimeSpent = "1:00",
                           Field = ValidField("some field"),
                           TaskType = new TaskType{Name = name},
                           ScheduledDate = DateTime.Parse("01/05/1975"),
                           ScheduledEndTime = DateTime.Parse("01/05/1975 4:00 PM"),
                           ScheduledStartTime = DateTime.Parse("01/05/1975 3:00 PM"),
                           Status = "Active",
                           Notes = "Some notes",
                           InventoryProduct = ValidInventoryProductChemical("product1"),
                           QuantityNeeded = 4
                       };
        }
        public static InventoryProduct ValidInventoryProductChemical(string name)
        {
            return new InventoryProduct
                       {
                           Product = ValidChemical(name),
                           Description = "some chemicals",
                           Quantity = 1000,
                           Cost = 100,
                           LastVendor = ValidVendor("my vendor")
                       };
        }

        public static InventoryProduct ValidInventoryProductFertilizer(string name)
        {
            return new InventoryProduct
            {
                Product = ValidFertilizer(name),
                Description = "some chemicals",
                Quantity = 1000,
                Cost = 100,
                LastVendor = ValidVendor("my vendor")
            };
        }

        public static Vendor ValidVendor(string name)
        {
            return new Vendor
                       {
                           Address1 = "1706 Willow St",
                           Address2 = "b",
                           City = "Austin",
                           Company = name,
                           Country = "USA",
                           Fax = "123.123.1234",
                           Notes = "notes",
                           Phone = "123.123.7890",
                           State = "TX",
                           Website = "www.vendor.com",
                           ZipCode = "78702",
                           Status = "Active"
                       };
        }

        public static Chemical ValidChemical(string name)
        {
            return new Chemical
                       {
                           Name = name,
                       };
        }

        public static Fertilizer ValidFertilizer(string name)
        {
            return new Fertilizer
            {
                Name = name,
                N =10,
                P = 10,
                K=10
            };
        }

        public static Employee ValidEmployee(string name)
        {
            return new Employee
                       {
                           Address1 = "1706 Willow St",
                           Address2 = "b",
                           City = "Austin",
                           Email = "branden@kyt.com",
                           Notes = "notes",
                           PhoneHome = "123.123.7890",
                           PhoneMobile = "123.123.1234",
                           State = "TX",
                           ZipCode = "78702",
                           Status = "Active",
                           BirthDate = DateTime.Parse("1/5/1972"),
                           FirstName = name,
                           LastName = "harik"
                       };
        }

        public static Equipment ValidEquipment(string name)
        {
            return new Equipment
                       {
                           Name = name
                       };
        }

        public static PurchaseOrderLineItem ValidPurchaseOrderLineItem(string name)
        {
            return new PurchaseOrderLineItem
                       {
                           Price = 6,
                           Product = ValidChemical(name),
                           PurchaseOrder = ValidPurchaseOrder("raif"),
                           QuantityOrdered = 4,
                       };
        }

        private static PurchaseOrder ValidPurchaseOrder(string name)
        {
            return new PurchaseOrder
                       {
                           Vendor = ValidVendor("raif")
                       };
        }

        public static InventoryProduct ValidInventoryBaseProduct(string name)
        {
            return new InventoryProduct
                       {
                           Cost=2,
                           Quantity = 4,
                           LastVendor = ValidVendor("raif"),
                           UnitType = UnitType.Tons.ToString()
                       };
        }

        public static Event ValidEvent(string name)
        {
            return new Event
                       {
                           EventType = new EventType {Name = name},
                           Field = ValidField("a Field")
                       };
        }
    }

    public class TestViewModel
    {
        public TestClass TestClass { get; set; }
    }
    public class TestClass
    {
        public string myStringProperty { get; set; }
        public IEnumerable<SelectListItem> myStringPropertyList { get; set; }
        public string myStringProperty2 { get; set; }
        public IEnumerable<SelectListItem> myStringProperty2List { get; set; }
        public string myStringProperty3 { get; set; }
        public IEnumerable<string> myStringProperty3List { get; set; }
        public string myStringProperty4 { get; set; }
        [ValidateNonEmpty]
        public string RequiredField { get; set; }

    }

}