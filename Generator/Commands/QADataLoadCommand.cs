namespace Generator.Commands
{
    public class QADataLoadCommand : IGeneratorCommand
    {
        private readonly IQADataLoader _qaDataLoader;

        public QADataLoadCommand(IQADataLoader qaDataLoader)
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