using System;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using NUnit.Framework;

namespace KnowYourTurf.UnitTests.Services
{
    public class IUnitSizeTimesQuantyCalculatorTester
    {
    }

    [TestFixture]
    public class when_calling_service_for_lbs
    {
        private UnitSizeTimesQuantyCalculator _SUT;
        private decimal _result;
        private InventoryProduct _product;

        [SetUp]
        public void Setup()
        {
            _product = ObjectMother.ValidInventoryProductChemical("poop");
            _product.SizeOfUnit = 100;
            _SUT = new UnitSizeTimesQuantyCalculator();
        }

        [Test]
        public void should_return_unitsize_for_pounds()
        {
            _product.UnitType = UnitType.Lbs.ToString();
            _result = _SUT.CalculateLbsPerUnit(_product);
            _result.ShouldEqual(100);
        }

        [Test]
        public void shoudl_return_proper_amount_for_oz()
        {
            _product.UnitType = UnitType.Oz.ToString();
            _result = _SUT.CalculateLbsPerUnit(_product);
            _result.ShouldEqual(6.25);
        }

        [Test]
        public void shoudl_return_proper_amount_for_tons()
        {
            _product.UnitType = UnitType.Tons.ToString();
            _result = _SUT.CalculateLbsPerUnit(_product);
            _result.ShouldEqual(200000);
        }

    }
}