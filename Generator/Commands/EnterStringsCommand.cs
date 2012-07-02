//using System;
//using KnowYourTurf.Core.Domain;
//
//namespace Generator.Commands
//{
//    public class EnterStringsCommand : IGeneratorCommand
//    {
//        private readonly ILocalizedStringLoader _loader;
//        private readonly IRepository _repository;
//
//        public EnterStringsCommand(ILocalizedStringLoader loader, IRepository repository)
//        {
//            _loader = loader;
//            _repository = repository;
//        }
//
//        public string Description { get { return "Prompts for each missing localized string"; } }
//
//        public void Execute(string[] args)
//        {
//            if (args.Length > 1)
//            {
//                _loader.SetFileBasePath(args[1]);
//
//            }
//            _loader.EnterStrings(_repository, Console.In, Console.Out);
//            _repository.Commit();
//
//        }
//    }
//}