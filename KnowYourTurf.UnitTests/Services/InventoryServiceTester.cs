using System;
using System.Collections.Generic;
using System.Linq;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace KnowYourTurf.UnitTests.Services
{
    public class InventoryServiceTester
    {
    }

    [TestFixture]
    public class when_calling_receive_lineItem_for_product_already_in_inventory
    {
        private IRepository _repo;
        private IInventoryService _SUT;
        private long _poliId;
        private Continuation _continuation;
        private ISaveEntityService _saveEntityService;
        private IQueryable<InventoryProduct> _inventoryBaseProducts;
        private PurchaseOrderLineItem _purchaseOrderLineItem;
        private SpecificationExtensions.CapturingConstraint _sesCatcher;
        private ICrudManager _crudManager;

        [SetUp]
        public void Setup()
        {
            _poliId = 0;
            _repo = MockRepository.GenerateMock<IRepository>();
            _purchaseOrderLineItem = ObjectMother.ValidPurchaseOrderLineItem("raif");
            _inventoryBaseProducts = new List<InventoryProduct> { ObjectMother.ValidInventoryBaseProduct("raif")}.AsQueryable();
            _purchaseOrderLineItem.TotalReceived = 4;
            _repo.Expect(x => x.Query<InventoryProduct>(null)).IgnoreArguments().Return(_inventoryBaseProducts);
            _saveEntityService = MockRepository.GenerateMock<ISaveEntityService>();
            _crudManager = MockRepository.GenerateMock<ICrudManager>();
            _crudManager.Expect(x => x.Finish()).Return(new Notification());
            _sesCatcher = _saveEntityService.CaptureArgumentsFor(x=>x.ProcessSave(_inventoryBaseProducts.FirstOrDefault(),null),x=>x.Return(_crudManager));
            _SUT = new InventoryService(_repo, _saveEntityService);
             _crudManager = _SUT.ReceivePurchaseOrderLineItem(_purchaseOrderLineItem);
        }

        [Test]
        public void should_call_repo_to_get__corrisonding_inventory_product()
        {
            _repo.VerifyAllExpectations();
        }
        
        [Test]
        public void should_update_quantity_of_base_product_and_save()
        {
            _sesCatcher.First<InventoryProduct>().Quantity.ShouldEqual(8);
        }

        [Test]
        public void should_set_the_lastvendor_to_the_vendor_from_the_purchaseorder()
        {
            _sesCatcher.First<InventoryProduct>().LastVendor.ShouldEqual(_purchaseOrderLineItem.PurchaseOrder.Vendor);
        }
    }

    [TestFixture]
    public class when_calling_receive_lineItem_for_product_not_in_inventory
    {
        private IRepository _repo;
        private IInventoryService _SUT;
        private long _poliId;
        private Continuation _continuation;
        private ISaveEntityService _saveEntityService;
        private IQueryable<InventoryProduct> _inventoryBaseProducts;
        private PurchaseOrderLineItem _purchaseOrderLineItem;
        private SpecificationExtensions.CapturingConstraint _sesCatcher;
        private ICrudManager _crudManager;

        [SetUp]
        public void Setup()
        {
            _poliId = 0;
            _repo = MockRepository.GenerateMock<IRepository>();
            _purchaseOrderLineItem = ObjectMother.ValidPurchaseOrderLineItem("raif");
            _inventoryBaseProducts = new List<InventoryProduct>().AsQueryable();
            _purchaseOrderLineItem.TotalReceived = 4;
            _repo.Expect(x => x.Query<InventoryProduct>(null)).IgnoreArguments().Return(_inventoryBaseProducts);
            _crudManager = MockRepository.GenerateMock<ICrudManager>();
            _crudManager.Expect(x => x.Finish()).Return(new Notification());
            _saveEntityService = MockRepository.GenerateMock<ISaveEntityService>();
            _sesCatcher = _saveEntityService.CaptureArgumentsFor(x => x.ProcessSave(_inventoryBaseProducts.FirstOrDefault(),null), x => x.Return(_crudManager));
            _SUT = new InventoryService(_repo, _saveEntityService);
            _crudManager= _SUT.ReceivePurchaseOrderLineItem(_purchaseOrderLineItem);
        }

        [Test]
        public void should_call_repo_to_get__corrisonding_inventory_product()
        {
            _repo.VerifyAllExpectations();
        }

        [Test]
        public void should_create_and_save_new_inventoryitem_with_proper_quantity()
        {
            _sesCatcher.First<InventoryProduct>().Quantity.ShouldEqual(4);
        }

        [Test]
        public void should_create_and_save_new_inventoryitem()
        {
            _sesCatcher.First<InventoryProduct>().Product.ShouldEqual(_purchaseOrderLineItem.Product);
        }
    }

    //this is actually bullshit It's doing the same as above but i can't think of how to test that the query is 
    // correct right now.
    [TestFixture]
    public class when_calling_receive_lineItem_for_product_in_inventory_but_with_different_units_of_measure
    {
        private IRepository _repo;
        private IInventoryService _SUT;
        private long _poliId;
        private Continuation _continuation;
        private ISaveEntityService _saveEntityService;
        private IQueryable<InventoryProduct> _inventoryBaseProducts;
        private PurchaseOrderLineItem _purchaseOrderLineItem;
        private SpecificationExtensions.CapturingConstraint _sesCatcher;
        private ICrudManager _crudManager;

        [SetUp]
        public void Setup()
        {
            _poliId = 0;
            _repo = MockRepository.GenerateMock<IRepository>();
            _purchaseOrderLineItem = ObjectMother.ValidPurchaseOrderLineItem("raif");
            _inventoryBaseProducts = new List<InventoryProduct>().AsQueryable();
            _purchaseOrderLineItem.TotalReceived = 4;
            _repo.Expect(x => x.Query<InventoryProduct>(null)).IgnoreArguments().Return(_inventoryBaseProducts);
            _crudManager = MockRepository.GenerateMock<ICrudManager>();
            _crudManager.Expect(x => x.Finish()).Return(new Notification());
            _saveEntityService = MockRepository.GenerateMock<ISaveEntityService>();
            _sesCatcher = _saveEntityService.CaptureArgumentsFor(x => x.ProcessSave(_inventoryBaseProducts.FirstOrDefault(),null), x => x.Return(_crudManager));
            _SUT = new InventoryService(_repo, _saveEntityService);
            _crudManager = _SUT.ReceivePurchaseOrderLineItem(_purchaseOrderLineItem);
        }

        [Test]
        public void should_call_repo_to_get__corrisonding_inventory_product()
        {
            _repo.VerifyAllExpectations();
        }

        [Test]
        public void should_create_and_save_new_inventoryitem_with_proper_quantity()
        {
            _sesCatcher.First<InventoryProduct>().Quantity.ShouldEqual(4);
        }

        [Test]
        public void should_create_and_save_new_inventoryitem()
        {
            _sesCatcher.First<InventoryProduct>().Product.ShouldEqual(_purchaseOrderLineItem.Product);
        }
    }

    [TestFixture]
    public class when_calling_DecrementTaskProduct
    {
        private Task _validTask;
        private ISaveEntityService _saveEntityService;
        private Continuation _continuation;
        private SpecificationExtensions.CapturingConstraint _sesCatcher;
        private InventoryService _SUT;
        private ICrudManager _crudManager;

        [SetUp]
        public void Setup()
        {
            _continuation = new Continuation {Success = true};
            _validTask = ObjectMother.ValidTask("raif");
            _validTask.QuantityUsed = 5;
            _crudManager = MockRepository.GenerateMock<ICrudManager>();
            _crudManager.Expect(x => x.Finish()).Return(new Notification());
            _saveEntityService = MockRepository.GenerateMock<ISaveEntityService>();
            _sesCatcher = _saveEntityService.CaptureArgumentsFor(x => x.ProcessSave(_validTask.InventoryProduct, _crudManager), x => x.Return(_crudManager));
            _SUT = new InventoryService(null,_saveEntityService);
            _SUT.DecrementTaskProduct(_validTask, _crudManager);
        }

        [Test]
        public void should_reduce_the_amount_of_the_product_by_the_quantity_used()
        {
            _sesCatcher.First<InventoryProduct>().Quantity.ShouldEqual(995);
        }

    }

    
    
}