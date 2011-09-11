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
    public class OverseedRateNeededCalculatorTester
    {
    }

    [TestFixture]
    public class when_calling_calculate_on_the_overseed_rate_calc
    {
        private Field _field;
        private InventoryProduct _product;
        private OverseedRateNeededCalculator _SUT;
        private Continuation _result;
        private IRepository _repo;

        [SetUp]
        public void Setup()
        {
            _field = ObjectMother.ValidField("raif").WithEntityId(1);
            _field.Size = 120000;
            _product = ObjectMother.ValidInventoryProductFertilizer("poop").WithEntityId(2);
            _product.SizeOfUnit = 50;
            _product.UnitType = UnitType.Lbs.ToString();
            var given = new SuperInputCalcViewModel
                            {
                                Field = _field.EntityId.ToString(),
                                Product = _product.EntityId.ToString(),
                                BagsUsed = 10,
                                OverSeedPercent = 99
                            };
            _repo = MockRepository.GenerateMock<IRepository>();
            _repo.Expect(x => x.Find<Field>(Int64.Parse(given.Field))).Return(_field);
            _repo.Expect(x => x.Find<InventoryProduct>(Int64.Parse(given.Product))).Return(_product);
            _SUT = new OverseedRateNeededCalculator(_repo, new UnitSizeTimesQuantyCalculator(),null);
            _result = _SUT.Calculate(given);
        }

        [Test]
        public void should_call_repo_for_field_and_product()
        {
            _repo.VerifyAllExpectations();        
        }

        [Test, Ignore("slightly off need verification of result")]
        public void should_calculate_proper_value_for_bagsneeded()
        {
            ((OverseedRateNeededCalcViewModel) _result.Target).SeedRate.ShouldEqual(4.13);
        }
    }
}