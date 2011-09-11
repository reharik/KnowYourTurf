using System;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace KnowYourTurf.UnitTests.Services
{
    public class MaterialsCalculatorTester
    {
    }

    [TestFixture]
    public class when_calling_calculate
    {
        private Field _field;
        private IRepository _repo;
        private MaterialsCalculator _SUT;
        private Continuation _result;

        [SetUp]
        public void Setup()
        {
            _field = ObjectMother.ValidField("raif").WithEntityId(1);
            _field.Size = 1000;
            var given = new SuperInputCalcViewModel
            {
                Field = _field.EntityId.ToString(),
                Depth = 10,
                DitchDepth = 10,
                DitchlineWidth = 10,
                Drainageline = 10,
                PipeRadius = 10
            };
            _repo = MockRepository.GenerateMock<IRepository>();
            _repo.Expect(x => x.Find<Field>(Int64.Parse(given.Field))).Return(_field);
            _SUT = new MaterialsCalculator(_repo, null);
            _result = _SUT.Calculate(given);
        }

        [Test,Ignore]
        public void should_return_proper_amount_of_material()
        {
            ((MaterialsCalcViewModel) _result.Target).TotalMaterials.ShouldEqual(30.43);
        }

    }
}