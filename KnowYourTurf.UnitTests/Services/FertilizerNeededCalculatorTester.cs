using System;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace KnowYourTurf.UnitTests.Services
{
    public class FertilizerNeededCalculatorTester
    {
    }

    [TestFixture]
    public class when_calling_calculate_on_calc
    {
        private Field _field;
        private InventoryProduct _product;
        private FertilizerNeededCalculator _SUT;
        private Continuation _result;
        private IRepository _repo;

        [SetUp]
        public void Setup()
        {
            _field = ObjectMother.ValidField("raif").WithEntityId(1);
            _field.Size = 1000;
            _product = ObjectMother.ValidInventoryProductFertilizer("poop").WithEntityId(2);
            _product.SizeOfUnit = 100;
            _product.UnitType = UnitType.Lbs.ToString();
            var given = new SuperInputCalcViewModel
                            {
                                Field = _field.EntityId.ToString(),
                                Product = _product.EntityId.ToString(),
                                FertilizerRate = 100
                            };
            _repo = MockRepository.GenerateMock<IRepository>();
            _repo.Expect(x => x.Find<Field>(Int64.Parse(given.Field))).Return(_field);
            _repo.Expect(x => x.Find<InventoryProduct>(Int64.Parse(given.Product))).Return(_product);
            _SUT = new FertilizerNeededCalculator(_repo, new UnitSizeTimesQuantyCalculator(),null);
            _result = _SUT.Calculate(given);
        }

        [Test]
        public void should_call_repo_for_field_and_product()
        {
            _repo.VerifyAllExpectations();        
        }

        [Test]
        public void should_calculate_proper_value_for_N()
        {
            ((FertilzierNeededCalcViewModel) _result.Target).N.ShouldEqual(100);
        }

        [Test]
        public void should_calculate_proper_value_for_P()
        {
            ((FertilzierNeededCalcViewModel)_result.Target).P.ShouldEqual(44);
        }

        [Test]
        public void should_calculate_proper_value_for_K()
        {
            ((FertilzierNeededCalcViewModel)_result.Target).K.ShouldEqual(83);
        }

        [Test, Ignore]
        public void should_calculate_proper_value_for_bagsneeded()
        {
            ((FertilzierNeededCalcViewModel)_result.Target).BagsNeeded.ShouldEqual(4.55);
        }
    }
}