using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;
using KnowYourTurf.Web.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace KnowYourTurf.UnitTests.Web.Controllers
{
    public class TaskControllerTester
    {
        
    }

    [TestFixture, Ignore("problem with the sescatcher")]
    public class when_calling_save_on_controller : ControllerTester<TaskController, TaskViewModel, ActionResult>
    {
        private Notification _model;
        private Field _field;
        private ISaveEntityService _saveEntityService;
        private IRepository repo;
        private SpecificationExtensions.CapturingConstraint _sesCatcher;
        private User _employee1;
        private User _employee3;
        private User _employee2;
        private InventoryProduct _validInventoryChemical;
        private Equipment _Equip;
        private Equipment _equipment1;
        private Equipment _equipment2;
        private Equipment _equipment3;
        private ICrudManager _crudManager;

        public when_calling_save_on_controller()
            : base((c,i) => c.Save(i))
        {
        }

        protected override void beforeEach()
        {
            Given = new TaskViewModel
                        {
                            Product ="3_Chemicals",
                            Task = ObjectMother.ValidTask("raif"),
                            
                        };
            _field = ObjectMother.ValidField("raif");
            _equipment1 = ObjectMother.ValidEquipment("raif").WithEntityId(6);
            _equipment2 = ObjectMother.ValidEquipment("poop").WithEntityId(7);
            _equipment3= ObjectMother.ValidEquipment("crap").WithEntityId(8);
            repo = MockFor<IRepository>();
            repo.Stub(x => x.Find<Field>(1)).Return(_field);
            _validInventoryChemical = ObjectMother.ValidInventoryProductChemical("lsd").WithEntityId(3);
            repo.Stub(x => x.Find<InventoryProduct>(3)).Return(_validInventoryChemical);
            _employee1 = ObjectMother.ValidEmployee("raif");
            _employee2 = ObjectMother.ValidEmployee("Amahl");
            _employee3 = ObjectMother.ValidEmployee("Ramsay");
            repo.Stub(x => x.Find<User>(1)).Return(_employee1);
            repo.Stub(x => x.Find<User>(2)).Return(_employee2);
            repo.Stub(x => x.Find<User>(3)).Return(_employee3);
            repo.Stub(x => x.Find<Equipment>(6)).Return(_equipment1);
            repo.Stub(x => x.Find<Equipment>(7)).Return(_equipment2);
            repo.Stub(x => x.Find<Equipment>(8)).Return(_equipment3);
            _crudManager = MockRepository.GenerateMock<ICrudManager>();
            _crudManager.Expect(x => x.Finish()).Return(new Notification());
            _saveEntityService = MockFor<ISaveEntityService>();
            _sesCatcher = _saveEntityService.CaptureArgumentsFor(x => x.ProcessSave(new Task(),null), x => x.Return(_crudManager));
        }

        [Test]
        public void should_find_specified_field()
        {
            TriggerOutputIfNotAlreadyTriggered();
            var task = _sesCatcher.First<Task>();
            task.Field.ShouldEqual(_field);
        }

        [Test]
        public void should_call_save_on_service()
        {
            //_sesCatcher.VerifyAllExpectations();
        }

        [Test]
        public void should_parse_product_info_and_call_repo_for_correct_product_and_id()
        {
            TriggerOutputIfNotAlreadyTriggered();
            var task = _sesCatcher.First<Task>();
            task.InventoryProduct.ShouldEqual(_validInventoryChemical);
        }

        [Test]
        public void should_get_employees_from_repo_for_each_one_in_input()
        {
            TriggerOutputIfNotAlreadyTriggered();
            var task = _sesCatcher.First<Task>();
            task.GetEmployees().FirstOrDefault().ShouldEqual(_employee1);
            task.GetEmployees().LastOrDefault().ShouldEqual(_employee3);
        }

        [Test]
        public void should_get_equipment_from_repo_for_each_one_in_input()
        {
            TriggerOutputIfNotAlreadyTriggered();
            var task = _sesCatcher.First<Task>();
            task.GetEquipment().FirstOrDefault().ShouldEqual(_equipment1);
            task.GetEquipment().LastOrDefault().ShouldEqual(_equipment3);
        }

    }
}