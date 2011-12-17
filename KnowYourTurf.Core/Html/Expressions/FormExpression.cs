using HtmlTags;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class FormExpression : HtmlTagExpressionBase
    {
        private readonly string _actionUrl;

        public FormExpression(string actionUrl) :base(new FormTag())
        {
            _actionUrl = actionUrl;
        }

        public FormExpression(string actionUrl, string id = null)
            : base(new FormTag())
        {
            _actionUrl = actionUrl;
            if(id!=null) ElementId(id);
        }

        public HtmlTag ToHtmlTag()
        {
            AddAttr("action", _actionUrl);
            AddAttr("method", "post");
            return ToHtmlTagBase();
        }

        public override string ToString()
        {
            return ToHtmlTag().ToString();
        }
    }
}