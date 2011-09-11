using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace KnowYourTurf.UnitTests
{
    public class SelectBoxPickerServiceTester
    {
    }

    [TestFixture]
    public class when_calling_get_pickerdto
    {
        private ISelectBoxPickerService _selectBoxPickerService;
        private Employee _validEmployee1;
        private Employee _validEmployee2;
        private Employee _validEmployee3;
        private Employee _validEmployee4;
        private Employee[] _selectedEmployees;
        private SelectBoxPickerDto _result;
        private ISelectListItemService _selectListItemService;
        private SelectListItem[] _selectedSelectListItems;
        private SelectListItem[] _availableSelectListItems;
        private SelectListItem _selectListItem1 = new SelectListItem {Text = "emp1", Value = "1"};
        private SelectListItem _selectListItem2 = new SelectListItem {Text = "emp2", Value = "2"};

        [SetUp]
        public void Setup()
        {
            _validEmployee1 = ObjectMother.ValidEmployee("emp1").WithEntityId(1);
            _validEmployee2 = ObjectMother.ValidEmployee("emp2").WithEntityId(2);
            _validEmployee3 = ObjectMother.ValidEmployee("emp3").WithEntityId(3);
            _validEmployee4 = ObjectMother.ValidEmployee("emp4").WithEntityId(4);
            _selectedEmployees = new []{_validEmployee3,_validEmployee4};
            _selectListItemService = MockRepository.GenerateMock<ISelectListItemService>();
            _availableSelectListItems = new[]
                                            {
                                                _selectListItem1,
                                                _selectListItem2,
                                                new SelectListItem {Text = "emp3", Value = "3"},
                                                new SelectListItem {Text = "emp4", Value = "4"}
                                            };
            _selectedSelectListItems = new[]
                                           {
                                               new SelectListItem {Text = "emp3", Value = "3"},
                                               new SelectListItem {Text = "emp4", Value = "4"}
                                           };
            _selectListItemService.Expect(x => x.CreateList<Employee>(null, null, false,true)).IgnoreArguments().Return(_availableSelectListItems);
            _selectListItemService.Expect(x => x.CreateList(_selectedEmployees, null, null, false)).IgnoreArguments().Return(_selectedSelectListItems);
            _selectBoxPickerService = new SelectBoxPickerService(_selectListItemService, null);
            _result = _selectBoxPickerService.GetPickerDto(_selectedEmployees,x=>x.FullName,x=>x.EntityId);
        }

        [Test]
        public void should_return_selectboxpickerdto()
        {
            _result.ShouldNotBeNull();
            _result.GetType().ShouldEqual(typeof (SelectBoxPickerDto));
        }

        [Test]
        public void should_call_selecteditemservice_twice()
        {
            _selectListItemService.VerifyAllExpectations();
        }

        [Test]
        public void should_return_model_with_proper_selectListitems_for_selected()
        {
            _result.SelectedListItems.ShouldEqual(_selectedSelectListItems);
        }

        [Test]
        public void should_return_model_with_proper_available_list_items()
        {
            _result.AvailableListItems.ShouldEqual(new[]
                                            {
                                                _selectListItem1,
                                                _selectListItem2
                                            });
        }

    }

    [TestFixture]
    public class when_calling_get_list_of_selected_entities
    {

        private ISelectBoxPickerService _selectBoxPickerService;
        private Employee _validEmployee1;
        private Employee _validEmployee2;
        private Employee[] _selectedEmployees;
        private ISelectListItemService _selectListItemService;
        private SelectListItem _selectListItem1 = new SelectListItem { Text = "emp1", Value = "1" };
        private SelectListItem _selectListItem2 = new SelectListItem { Text = "emp2", Value = "2" };
        private Employee[] _selectedEntities;
        private SelectBoxPickerDto dto;
        private IEnumerable<Employee> _result;
        private IRepository _repo;

        [SetUp]
        public void Setup()
        {
            _validEmployee1 = ObjectMother.ValidEmployee("emp1").WithEntityId(1);
            _validEmployee2 = ObjectMother.ValidEmployee("emp2").WithEntityId(2);
            _selectedEmployees = new[] { _validEmployee2, _validEmployee2 };
            _selectedEntities = new[]{_validEmployee1,_validEmployee2};
            dto = new SelectBoxPickerDto{Selected = new[]{"1","2"}};
            _repo = MockRepository.GenerateMock<IRepository>();
            _repo.Expect(x => x.Find<Employee>(1)).Return(_validEmployee1);
            _repo.Expect(x => x.Find<Employee>(2)).Return(_validEmployee2);
            _selectBoxPickerService = new SelectBoxPickerService(_selectListItemService,_repo);
            _result = _selectBoxPickerService.GetListOfSelectedEntities<Employee>(dto);
        }

        [Test]
        public void should_return_list_of_entities_corrisponding_to_list_of_ids()
        {
            _result.ToList()[0].ShouldEqual(_validEmployee1);
            _result.ToList()[1].ShouldEqual(_validEmployee2);
        }

    }


}