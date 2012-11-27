namespace Generator.Commands
{
    public class QADataLoadCommand : IGeneratorCommand
    {
        private readonly IFCDataLoader _qaDataLoader;

        public QADataLoadCommand(IFCDataLoader qaDataLoader)
        {
            _qaDataLoader = qaDataLoader;
        }

        public string Description { get { return "Loads Data for QA"; } }

        public void Execute(string[] args)
        {
            _qaDataLoader.Load();
        }
    }
}