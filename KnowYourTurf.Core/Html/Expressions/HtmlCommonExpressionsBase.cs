using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace KnowYourTurf.Core.Html.Expressions
{
    public abstract class HtmlCommonExpressionBase
    {
        protected string DefaultId { get; set; }

        public IDictionary<string, string> HtmlAttributes { get; set; }
        public IList<string> CssClasses { get; set; }
        protected bool UnEncoded { get; set; }

        protected HtmlCommonExpressionBase()
        {
            HtmlAttributes = new Dictionary<string, string>();
            CssClasses = new List<string>();
        }

        protected IDictionary<string, string> GetAllHtmlAttributes()
        {
            var attributes = new Dictionary<string, string>(HtmlAttributes);

            var builder = new StringBuilder();

            addCssClasses(builder, attributes);

            if (!attributes.ContainsKey("id") && DefaultId.IsNotEmpty()) attributes.Add("id", DefaultId);

            return attributes;
        }

        private void addCssClasses(StringBuilder builder, IDictionary<string, string> attributes)
        {
            if (CssClasses.Count > 0)
            {
                CssClasses.ForEachItem(name => builder.AppendFormat("{0}", name));

                attributes.Add("class", string.Join(" ", CssClasses.ToArray()));

                builder.Length = 0;
            }
        }

        protected string GetHtmlAttributesString()
        {
            var attributes = GetAllHtmlAttributes();

            if (attributes.Count == 0)
            {
                return "";
            }

            var builder = new StringBuilder();

            attributes.ForEachItem(pair => builder.AppendFormat(
                                        CultureInfo.InvariantCulture, " {0}=\"{1}\"",
                                        pair.Key,
                                        HttpUtility.HtmlAttributeEncode(pair.Value)));

            return builder.ToString();
        }

        private bool _isVisible = true;
        protected internal virtual bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }

        public override string ToString()
        {
            if (!IsVisible) return string.Empty;

            var html = new SelfClosingHtmlWriter(new StringWriter(), string.Empty);
            beforeStartingMainTag(html);
            addMainTagAttributes();
            renderCustomAttributes(html);

            html.RenderBeginTag(theMainTagIs());
            beforeMainTagInnerText(html);
            var text = theMainTagInnerTextIs();
            if (text != null)
            {
                if (UnEncoded) html.Write(text);
                else html.WriteEncodedText(text);
            }
            beforeEndingMainTag(html);
            html.EndRender();
            return html.InnerWriter.ToString();
        }



        protected void renderCustomAttributes(HtmlTextWriter html)
        {
            GetAllHtmlAttributes().ForEachItem(a => html.AddAttribute(a.Key, a.Value));
        }

        protected virtual void beforeStartingMainTag(HtmlTextWriter html)
        {

        }

        protected virtual string theMainTagIs()
        {
            return "";
        }

        protected virtual void addMainTagAttributes()
        {
        }

        protected virtual void beforeMainTagInnerText(HtmlTextWriter html)
        {
        }

        protected virtual string theMainTagInnerTextIs()
        {
            return null;
        }

        protected virtual void beforeEndingMainTag(HtmlTextWriter html)
        {

        }
    }

    public class SelfClosingHtmlWriter : HtmlTextWriter
    {
        private int unclosedTags;

        public SelfClosingHtmlWriter(TextWriter writer, string tabString)
            : base(writer, tabString)
        {
            NewLine = "";
            unclosedTags = 0;
        }

        public override void RenderBeginTag(string tagName)
        {
            unclosedTags++;
            base.RenderBeginTag(tagName);
        }

        public override void RenderEndTag()
        {
            base.RenderEndTag();
            unclosedTags--;
        }

        public override void EndRender()
        {
            while (unclosedTags > 0)
            {
                var lastUnclosedTagsCount = unclosedTags;
                RenderEndTag();
                Debug.Assert(unclosedTags < lastUnclosedTagsCount);
            }
            base.EndRender();
        }
    }
 
   
}
