using System.Collections.Generic;
using CC.Core.Localization;
using HtmlTags;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class StandardButtonExpression
    {
        private string _name;
        private string _text;
        private string _id;
        public IDictionary<string, string> HtmlAttributes { get; set; }
        public IList<string> CssClasses { get; set; }

        public StandardButtonExpression(string name)
        {
            CssClasses = new List<string> { };
            _name = name;

            HtmlAttributes = new Dictionary<string, string> { { "name", name }, { "id", _id ?? _name } };
        }

        protected void addMainTagAttributes()
        {
            HtmlAttributes.Add("onclick", "return false");
        }

        public HtmlTag ToHtmlTag()
        {
            var root = new HtmlTag("Button");
            root.Text(_text);
            addMainTagAttributes();
            addClassesAndAttributesToRoot(root);
            return root;
        }

        private void addClassesAndAttributesToRoot(HtmlTag root)
        {
            HtmlAttributes.ForEachItem(x => root.Attr(x.Key, x.Value));
            CssClasses.ForEachItem(x => root.AddClass(x));
        }

        public StandardButtonExpression LocalizedText(StringToken token)
        {
            return NonLocalizedText(token.ToString());
        }

        public StandardButtonExpression NonLocalizedText(string rawText)
        {
            _text = rawText;
            return this;
        }

        public StandardButtonExpression AddClass(string cssClass)
        {
            CssClasses.Add(cssClass);
            return this;
        }

        public StandardButtonExpression Attr(string attribute, string value)
        {
            HtmlAttributes.Add(attribute,value);
            return this;
        }


        public StandardButtonExpression ElementId(string id)
        {
            _id = id;
            return this;
        }
    }
}