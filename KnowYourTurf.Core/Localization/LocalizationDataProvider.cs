using System.Globalization;
using System.Linq;
using System.Reflection;
using FubuMVC.Core.Util;

namespace KnowYourTurf.Core.Localization
{
    public interface ILocalizationDataProvider
    {
        CultureInfo Culture { get; set; }
        string GetTextForKey(StringToken key);
        string GetText(Enumeration localizedEnum);
        Header GetHeader(Accessor property);
    }

    public class LocalizationDataProvider : ILocalizationDataProvider
    {
        private readonly Cache<StringToken, string> _textValues = new Cache<StringToken, string>();
        private readonly Cache<Enumeration, string> _enumerations = new Cache<Enumeration, string>();
        private readonly Cache<Accessor, Header> _headers = new Cache<Accessor, Header>();

        public LocalizationDataProvider()
        {
            _textValues.OnMissing = findMissingLocalizedText;
            _enumerations.OnMissing = findMissingLocalizedEnumText;
            _headers.OnMissing = findMissingProperty;

        }

        public CultureInfo Culture { get; set; }

        private Header findMissingProperty(Accessor accessor)
        {
            //var localized =
            //    _repository.FindBy<LocalizedProperty>(
            //        p => p.Culture == Culture.Name && p.ParentType == property.DeclaringType.FullName && p.Name == property.Name);

            //if (localized == null)
            //{
            //    localized = new LocalizedProperty()
            //    {
            //        Culture = Culture.Name,
            //        ParentType = property.DeclaringType.FullName,
            //        Name = property.Name,
            //        Text = Culture + "_" + property.Name,
            //        Tooltip = "Tooltip for " + property.Name
            //    };

            //    _repository.Save(localized);
            //}
            string value = string.Empty;
            if (accessor is PropertyChain)
            {
                var names = ((PropertyChain) (accessor)).Names;
                if (names.Last() != "Name")
                {
                    value = names.Last();
                }
                else
                {
                    var className = names.ToArray()[names.Count() - 2];
                    value = className + " " + names.Last();
                }
            }else
            {
                value = accessor.Name;
            }

            return new Header() { HeaderText = value.ToSeperateWordsFromPascalCase(), Tooltip = "Tooltip for " + value.ToSeperateWordsFromPascalCase() };
        }

        private string findMissingLocalizedText(StringToken token)
        {
            //return "Error String Not Found!";
            //var localizedText = _repository.FindBy<LocalizedText>(t => t.Name == token.Key && t.Culture == Culture.Name);
            //if (localizedText == null)
            //{
            string defaultText = "Error String Not Found!";

            if (token.DefaultValue.IsNotEmpty())
            {
                var prefix = ""; //((Culture.Name.Equals("en-US", StringComparison.InvariantCultureIgnoreCase)) ? "" : Culture.Name + "_");
                defaultText = prefix + token.DefaultValue;
            }
            //    else
            //    {
            //        defaultText = Culture + "_" + token.Key;
            //    }

            //    localizedText = new LocalizedText(token.Key, Culture.Name, defaultText);
            //    _repository.Save(localizedText);
            //}

            //return localizedText.Text;
            return defaultText;
        }

        private string findMissingLocalizedEnumText(Enumeration key)
        {
            return key.Key;

            //var localized = _repository.FindBy<LocalizedEnumeration>(
            //    v => v.Culture == Culture.Name &&
            //         v.ValueType == key.GetType().FullName &&
            //         v.Name == key.Key);

            //if (localized == null)
            //{
            //    localized = new LocalizedEnumeration()
            //    {
            //        Culture = Culture.Name,
            //        ValueType = key.GetType().FullName,
            //        Name = key.Key,
            //        Text = Culture + "_" + key.Key
            //    };

            //    _repository.Save(localized);
            //}

            //return localized.Text;
        }

        public string GetTextForKey(StringToken key)
        {
            return _textValues.Retrieve(key);
        }

        public string GetText(Enumeration localizedEnum)
        {
            return _enumerations.Retrieve(localizedEnum);
        }

        public Header GetHeader(Accessor accessor)
        {
            return _headers.Retrieve(accessor);
        }
    }
}