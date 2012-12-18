namespace Generator.Commands
{
    public class CODataLoadCommand : IGeneratorCommand
    {
        private readonly ICODataLoader _coDataLoader;

        public CODataLoadCommand(ICODataLoader coDataLoader)
        {
            _coDataLoader = coDataLoader;
        }

        public string Description { get { return "Loads Data for QA"; } }

        public void Execute(string[] args)
        {
            _coDataLoader.Load();
        }
    }
}