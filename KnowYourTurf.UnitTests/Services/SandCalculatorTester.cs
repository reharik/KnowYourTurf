using System;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace KnowYourTurf.UnitTests.Services
{
    public class SandCalculatorTester
    {
    }

    [TestFixture]
    public class when_calling_calculate_for_sand
    {
        private SandCalculator _SUT;
        private Continuation _result;

        [SetUp]
        public void Setup()
        {
            var given = new SuperInputCalcViewModel
            {
                Area = 261.67,
                Height = 10,
                Diameter = 10
            };

            _SUT = new SandCalculator();
            _result = _SUT.Calculate(given);
        }

        [Test]
        public void should_return_proper_amount_of_material()
        {
            ((SandCalcViewModel) _result.Target).TotalSand.ShouldEqual(9.69);
        }

    }
}