using KnowYourTurf.Core.Domain;

namespace Generator.Commands
{
    public class DumpStringsCommand : IGeneratorCommand
    {
        private readonly ILocalizedStringLoader _loader;
        private readonly IRepository _repository;

        public DumpStringsCommand(ILocalizedStringLoader loader, IRepository repository)
        {
            _loader = loader;
            _repository = repository;
        }

        public string Description { get { return "Dumps localized strings to XML"; } }

        public void Execute(string[] args)
        {
            if(args.Length>0)
            {
             _loader.SetFileBasePath(args[1]);   
            }
            _loader.DumpStrings(_repository);
        }
    }
}