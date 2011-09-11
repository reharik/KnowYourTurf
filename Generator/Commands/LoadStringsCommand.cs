using KnowYourTurf.Core.Domain;

namespace Generator.Commands
{
    public class LoadStringsCommand : IGeneratorCommand
    {
        private readonly ILocalizedStringLoader _loader;
        private readonly IRepository _repository;

        public LoadStringsCommand(ILocalizedStringLoader loader, IRepository repository)
        {
            _loader = loader;
            _repository = repository;
        }

        public string Description { get { return "Loads localized strings from XML"; } }

        public void Execute(string[] args)
        {
            if (args.Length > 1)
            {
                _loader.SetFileBasePath(args[1]);
            }

            _loader.ClearStrings();
            _loader.LoadStrings(_repository);
        }
    }
}