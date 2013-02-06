//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Net;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Xml;
//using System.Xml.Serialization;
//using KnowYourTurf.Core.Domain;
//using NHibernate;
//using StructureMap;
//using DomainEntity = KnowYourTurf.Core.Domain.DomainEntity;
//
//namespace Generator
//{
//    public interface ILocalizedStringLoader
//    {
//        void LoadStrings(IRepository repository);
//        void DumpStrings(IRepository repository);
//        void DeleteAll(IEnumerable<object> items, IRepository repository);
//        void ClearStrings();
//        void ExecuteCommands(params string[] commands);
//        void EnterStrings(IRepository repository, TextReader inReader, TextWriter outWriter);
//        void PromptForString(ILocalizedItem item, IRepository repository, TextReader inReader, TextWriter outWriter);
//        void SetFileBasePath(string path);
//    }
//
//    public class LocalizedStringLoader : ILocalizedStringLoader
//    {
//        private static readonly Regex _rex = new Regex(
//            @"\<div id=result_box\s*(|dir=\""(?<dir>.*?)\"")\s*\>(?<contents>.*?)\</div\>",
//            RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnoreCase);
//
//
//        private string _textXmlFileName;
//        private string _propXmlFileName;
//        private string _valueObjXmlFileName;
//
//        public LocalizedStringLoader()
//        {
//            SetFileBasePath(Path.Combine(GetType().Assembly.Location, "../../../dataimport"));
//        }
//
//        public void SetFileBasePath(string basePath)
//        {
//            var dataImportPath = Path.GetFullPath(basePath);
//
//            _textXmlFileName = Path.Combine(dataImportPath, "localizedText.xml");
//            _propXmlFileName = Path.Combine(dataImportPath, "localizedProperty.xml");
//            _valueObjXmlFileName = Path.Combine(dataImportPath, "localizedValueObject.xml");
//        }
//
//        public void LoadStrings(IRepository repository)
//        {
//            var localizedTextItems = ReadFromXml<LocalizedText>(_textXmlFileName);
//            var localizedPropItems = ReadFromXml<LocalizedProperty>(_propXmlFileName);
//            var localizedValueItems = ReadFromXml<LocalizedEnumeration>(_valueObjXmlFileName);
//
//            // TODO: should probably have a transaction around this
//
//            localizedTextItems.Each(x=> repository.Save(x));
//            localizedPropItems.Each(x => repository.Save(x));
//            localizedValueItems.Each(x => repository.Save(x));
//        }
//
//        public void DumpStrings(IRepository repository)
//        {
//            DumpToXml(repository.Query<LocalizedText>(l => true), _textXmlFileName);
//            DumpToXml(repository.Query<LocalizedProperty>(l => true), _propXmlFileName);
//            DumpToXml(repository.Query<LocalizedEnumeration>(l => true), _valueObjXmlFileName);
//        }
//
//        public void TranslateStrings(string fromCulture, string toCulture, IRepository repository)
//        {
//            var comparer = StringComparison.InvariantCultureIgnoreCase;
//            // Only get the ones from the culture we want
//            var textItems = ReadFromXml<LocalizedText>(_textXmlFileName).Where(l => l.Culture.Equals(fromCulture, comparer)).ToArray();
//            var propItems = ReadFromXml<LocalizedProperty>(_propXmlFileName).Where(l => l.Culture.Equals(fromCulture, comparer)).ToArray();
//            var valObjItems = ReadFromXml<LocalizedEnumeration>(_valueObjXmlFileName).Where(l => l.Culture.Equals(fromCulture, comparer)).ToArray();
//
//            textItems.Each(l => l.Culture = toCulture);
//            propItems.Each(l => l.Culture = toCulture);
//            valObjItems.Each(l => l.Culture = toCulture);
//
//            Console.WriteLine("Translating localized text strings from '{0}' to '{1}' ({2}): ",
//                              fromCulture, toCulture, textItems.Count());
//
//            TranslateToCulture(
//                textItems,
//                fromCulture,
//                toCulture,
//                l => l.Text);
//
//            Console.WriteLine("Translating localized property strings from '{0}' to '{1}' ({2}): ",
//                              fromCulture, toCulture, propItems.Count());
//
//            TranslateToCulture(
//                propItems,
//                fromCulture,
//                toCulture,
//                l => l.Text);
//
//            Console.WriteLine("Translating localized value object strings from '{0}' to '{1}' ({2}): ",
//                              fromCulture, toCulture, valObjItems.Count());
//
//            TranslateToCulture(
//                valObjItems,
//                fromCulture,
//                toCulture,
//                l => l.Text);
//
//            textItems.Each(l => repository.Save(l));
//            propItems.Each(l => repository.Save(l));
//            valObjItems.Each(l => repository.Save(l));
//        }
//
//        private static T[] ReadFromXml<T>(string xmlFileName)
//            where T : DomainEntity
//        {
//            if (!File.Exists(xmlFileName))
//            {
//                Console.WriteLine("WARNING: File missing. Could not import data from '{0}'.", xmlFileName);
//                return null;
//            }
//
//            var xmlSerializer = new XmlSerializer(typeof(T[]));
//
//            T[] items;
//
//            using (var reader = new StreamReader(xmlFileName))
//            {
//                items = xmlSerializer.Deserialize(reader) as T[];
//            }
//
//            items.Each(l => l.EntityId = 0);
//            return items;
//        }
//
//        private static void DumpToXml<T>(IEnumerable<T> items, string xmlFileName)
//            where T : DomainEntity
//        {
//            items.Each(l => l.EntityId = 0);
//
//            var xmlSerializer = new XmlSerializer(typeof(T[]));
//
//            using (var xmlWriter = new XmlTextWriter(xmlFileName, Encoding.UTF8))
//            {
//                xmlWriter.Formatting = Formatting.Indented;
//                xmlSerializer.Serialize(xmlWriter, items.ToArray());
//            }
//        }
//
//        private static void TranslateToCulture<T>(IEnumerable<T> items, string fromCulture, string toCulture, Expression<Func<T, string>> expression)
//            where T : DomainEntity
//        {
//            var prop = FubuMVC.Core.Util.ReflectionHelper.GetAccessor(expression);
//
//            fromCulture = fromCulture.Substring(0, 2);
//            toCulture = toCulture.Substring(0, 2);
//            int totalRead = 0;
//
//            foreach (var item in items)
//            {
//                item.EntityId = 0;
//                var text = prop.GetValue(item);
//
//                var googleUri = string.Format("http://translate.google.com/translate_t?ie=UTF8&sl={0}&tl={1}&text={2}",
//                                              fromCulture,
//                                              toCulture,
//                                              text);
//
//                var result = getResponseBody(googleUri);
//
//                Match m = _rex.Match(result);
//
//                if (m.Success)
//                {
//                    prop.SetValue(item, m.Groups["contents"].Value);
//                }
//
//                totalRead++;
//
//                Console.Write("\r{0}", totalRead);
//            }
//
//            Console.WriteLine();
//        }
//
//        private static string getResponseBody(string uri)
//        {
//            var request = (HttpWebRequest)WebRequest.Create(uri);
//
//            using (var response = (HttpWebResponse)request.GetResponse())
//            {
//                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet)))
//                {
//                    return reader.ReadToEnd();
//                }
//            }
//        }
//
//        public void DeleteAll(IEnumerable<object> items, IRepository repository)
//        {
//            items.Each(repository.HardDelete);
//        }
//
//
//        public void ClearStrings()
//        {
//            ExecuteCommands(
//                "Delete from LocalizedProperty",
//                "Delete from LocalizedText",
//                "Delete from LocalizedEnumeration");
//        }
//
//        public void ExecuteCommands(params string[] commands)
//        {
//            var source = ObjectFactory.GetInstance<ISessionFactory>();
//
//            using (ISession session = source.OpenSession())
//            {
//                IDbConnection connection = session.Connection;
//                using (IDbCommand command = connection.CreateCommand())
//                {
//                    commands.Each(text =>
//                    {
//                        command.CommandText = text;
//                        command.ExecuteNonQuery();
//                    });
//                }
//            }
//        }
//
//        public void ClearStringsForCulture(string culture, IRepository repository)
//        {
//            var comparer = StringComparer.InvariantCultureIgnoreCase;
//
//            string baseFmt = string.Format("DELETE FROM {{0}} WHERE Culture = '{0}'", culture);
//
//            ExecuteCommands(
//                string.Format(baseFmt, "LocalizedText"),
//                string.Format(baseFmt, "LocalizedProperty"),
//                string.Format(baseFmt, "LocalizedEnumeration")
//                );
//        }
//
//        public void EnterStrings(IRepository repository, TextReader inReader, TextWriter outWriter)
//        {
//            repository.Initialize();
//            outWriter.WriteLine("--------------------");
//            outWriter.WriteLine("Localized entries in the database which have no default text will be displayed.");
//            outWriter.WriteLine("Please type in text for each entry and press RETURN to proceed to the next.");
//            outWriter.WriteLine("Cancel anytime by pressing CTRL+C.");
//            outWriter.WriteLine("--------------------");
//            outWriter.WriteLine();
//
//            repository.Query<LocalizedText>(l => l.Culture == "en-US" && l.Text.StartsWith("en-US_")).ToList().Cast<ILocalizedItem>()
//                   .Concat(repository.Query<LocalizedProperty>(l => l.Culture == "en-US" && l.Text.StartsWith("en-US_")).ToList().Cast<ILocalizedItem>())
//                   .Concat(repository.Query<LocalizedEnumeration>(l => l.Culture == "en-US" && l.Text.StartsWith("en-US_")).ToList().Cast<ILocalizedItem>())
//                   .Each(l => PromptForString(l, repository, inReader, outWriter));
//        }
//
//        public void PromptForString(ILocalizedItem item, IRepository repository, TextReader inReader, TextWriter outWriter)
//        {
//            outWriter.Write("[{0}] FriendlyName: {1} (Current: {2}): ", item.GetType().Name, item.Name, item.Text);
//            item.Text = inReader.ReadLine();
//            repository.Save((DomainEntity)item);
//        }
//    }
//}