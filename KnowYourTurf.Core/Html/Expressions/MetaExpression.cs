using System.Collections.Generic;
using System.Linq;
using CC.Core;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class MetaExpression : HtmlCommonExpressionBase
    {
        public MetaExpression Name(string name)
        {
            this.Attr("name", name);
            return this;
        }

        public MetaExpression Content(string content)
        {
            this.Attr("content", content);
            return this;
        }

        public MetaExpression AsContentType()
        {
            this.Attr("http-equiv", "Content-Type");
            this.Attr("content", "text/html; charset=utf-8");
            return this;
        }

        public MetaExpression Content(IEnumerable<string> contentList)
        {
            this.Attr("content", string.Join(", ", contentList.ToArray()));
            return this;
        }

        public override string ToString()
        {
            return "<meta{0}/>".ToFormat(GetHtmlAttributesString());
        }
    }
}