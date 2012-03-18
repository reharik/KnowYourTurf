using System;
using System.Linq.Expressions;
using KnowYourTurf.Core.Localization;
using FubuMVC.Core.Util;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class StyledButtonExpression : HtmlCommonExpressionBase 
    {
        private const string NORMAL_BUTTON_CLASS = "btn-2";
        private const string HIGHLIGHTED_BUTTON_CLASS = "btn-3";

        private string _text;
        private string _btnType = NORMAL_BUTTON_CLASS;
        private string _href = "#";
        private string _nameAndId;
        private bool _visible = true;
        private bool _enabled = true;

        public StyledButtonExpression(string nameAndId)
        {
            _nameAndId = nameAndId;
            this.Attr("name", nameAndId);
            this.Attr("id", nameAndId);
            this.Class("button");
        }

        protected override void addMainTagAttributes()
        {
            this.Attr("href", _href);
            this.Attr("onclick", "return false");
            if (!_enabled) this.Attr("disabled", "disabled");
            if(!_visible) this.Attr("style", "display:none");
            this.Class(_btnType);
        }

        protected override string theMainTagIs()
        {
            return "a";
        }
        protected override void beforeMainTagInnerText(System.Web.UI.HtmlTextWriter html)
        {
            html.RenderBeginTag("span");
            html.RenderBeginTag("span");
        }
        protected override string theMainTagInnerTextIs()
        {
            return _text;
        }
        protected override void beforeEndingMainTag(System.Web.UI.HtmlTextWriter html)
        {
            html.RenderEndTag();

            html.AddAttribute("type", "submit");
            html.AddAttribute("id", _nameAndId + "Button");
            html.AddAttribute("name", "hiddenSubmitButton");
            html.AddStyleAttribute("display", "none");
            html.RenderBeginTag("input");
        }

        public StyledButtonExpression LocalizedText(StringToken token)
        {
            return NonLocalizedText(token.ToString());
        }

        public StyledButtonExpression NonLocalizedText(string rawText)
        {
            _text = rawText;
            return this;
        }

        public StyledButtonExpression Visible(bool visible)
        {
            _visible = visible;
            return this;
        }

        public StyledButtonExpression Enabled(bool enabled)
        {
            _enabled = enabled;
            return this;
        }

        public StyledButtonExpression AsRedirect<VIEWMODEL>(VIEWMODEL viewModel, Expression<Func<VIEWMODEL, object>> expression) where VIEWMODEL : class       
        {
            var _accessor = ReflectionHelper.GetAccessor(expression);
            _href = _accessor.GetValue(viewModel).ToString();
            return this;
        }

        public StyledButtonExpression AsRedirect(string action)
        {
            _href = action;
            return this;
        }

        public StyledButtonExpression AddClass(string cssClass)
        {
            CssClasses.Add(cssClass);
            return this;
        }

        public StyledButtonExpression AsSubmit()
        {
            this.Class("submitButton");
            _btnType = HIGHLIGHTED_BUTTON_CLASS;
            return this;
        }

        public StyledButtonExpression Highlighted()
        {
            _btnType = HIGHLIGHTED_BUTTON_CLASS;
            return this;
        }
    }
}