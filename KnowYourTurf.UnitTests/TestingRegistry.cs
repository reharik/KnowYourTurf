using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using StructureMap.Configuration.DSL;

namespace KnowYourTurf.UnitTests
{
    public class TestingRegistry : Registry
    {
        public TestingRegistry()
        {
            For<IGetCompanyIdService>().Use<DataLoaderGetCompanyIdService>();
        }
    }
}