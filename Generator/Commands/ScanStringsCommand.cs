using System;
using System.Reflection;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Web;
using KnowYourTurf.Web.Config;
using StructureMap;

namespace Generator.Commands
{
    public class ScanStringsCommand : IGeneratorCommand
    {
        private IRepository _repository;

        public string Description
        {
            get { return "Scans for new/missing localized strings"; }
        }

        public void Execute(string[] args)
        {
            _repository = ObjectFactory.GetInstance<IRepository>();
            _repository.Initialize();
            new LocalizationScanner(_repository).Execute();
            _repository.Commit();
        }
    }

    public class LocalizationScanner
    {
        private const string EN_US = "en-US";
        private readonly IRepository _repository;

        public LocalizationScanner(IRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            scanAssembly(typeof(CoreLocalizationKeys).Assembly);
            scanAssembly(typeof(WebLocalizationKeys).Assembly);
            scanValueTypes();
        }

        private void scanValueTypes()
        {
            LocalizedEnumRegistry.ForEachValue(processLocalizedEnum);
        }

        private void processLocalizedEnum(Enumeration localizedEnum)
        {
            var typeName = localizedEnum.GetType().FullName;
            var localizedObject =
                _repository.FindBy<LocalizedEnumeration>(
                    x => x.Culture == EN_US && x.Name == localizedEnum.Key && x.ValueType == typeName);

            if (localizedObject != null) return;

            var message = "Found new LocalizedEnum '{0}' of {1}".ToFormat(localizedEnum.Key, typeName);
            Console.WriteLine(message);

            localizedObject = new LocalizedEnumeration()
            {
                Culture = EN_US,
                Name = localizedEnum.Key,
                Text = EN_US + "-" + localizedEnum.Key,
                ValueType = typeName,
                Tooltip = "Tooltip for en-US-" + localizedEnum.Key
            };

            _repository.Save(localizedObject);
        }

        private void scanAssembly(Assembly assembly)
        {
            foreach (var exportedType in assembly.GetExportedTypes())
            {
                if (typeof(StringToken).IsAssignableFrom(exportedType) && exportedType != typeof(StringToken))
                {
                    scanStringTokenType(exportedType);
                }
            }
        }

        private void scanStringTokenType(Type type)
        {
            Console.WriteLine("Looking at type " + type.FullName);
            foreach (var field in type.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var token = field.GetValue(null) as StringToken;
                if (token == null)
                {
                    return;
                }

                processToken(token);
            }
        }

        private void processToken(StringToken token)
        {
            var text = _repository.FindBy<LocalizedText>(x => x.Name == token.Key && x.Culture == EN_US);
            if (text != null) return;

            Console.WriteLine("Found new StringToken:  " + token.Key);
            text = new LocalizedText(token.Key, EN_US, token.DefaultValue);
            _repository.Save(text);
        }
    }
}