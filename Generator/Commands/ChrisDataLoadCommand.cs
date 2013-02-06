using KnowYourTurf.Core.Services;
using StructureMap;

namespace Generator.Commands
{
    public class ChrisDataLoadCommand : IGeneratorCommand
    {
        private readonly IQADataLoader _qaDataLoader;

        public ChrisDataLoadCommand()
        {
            _qaDataLoader = ObjectFactory.Container.GetInstance<IQADataLoader>();
        }

        public string Description { get { return "Loads Data for QA"; } }

        public void Execute(string[] args)
        {
            _qaDataLoader.Load();
        }
    }
}