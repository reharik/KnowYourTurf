using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using KnowYourTurf.Core.Localization;
using FubuMVC.Core.Util;
using HtmlTags;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class StyledButtonExpression 
    {
        private const string NORMAL_BUTTON_CLASS = "btn-2";
        private const string HIGHLIGHTED_BUTTON_CLASS = "btn-3";

        private string _text;
        private string _btnType = NORMAL_BUTTON_CLASS;
        private string _href = "#";
        private string _nameAndId;
        private string _id;
        private bool _visible = true;
        private bool _enabled = true;
        private bool _asSubmit;

        public IDictionary<string, string> HtmlAttributes { get; set; }
        public IList<string> CssClasses { get; set; }

        public StyledButtonExpression(string nameAndId, bool small = false)
        {
            CssClasses = new List<string> {small ? "smallbutton" : "button"};
            _nameAndId = nameAndId;
            
            HtmlAttributes = new Dictionary<string, string> {{"name", nameAndId}, {"id", _id??_nameAndId}};
        }

        protected void addMainTagAttributes()
        {
            HtmlAttributes.Add("href", _href);
            if(_href.IsEmpty() || _href == "#")
                HtmlAttributes.Add("onclick", "return false");
            if (!_enabled) HtmlAttributes.Add("disabled", "disabled");
            if (!_visible) HtmlAttributes.Add("style", "display:none");
            CssClasses.Add(_btnType);
        }

        public HtmlTag ToHtmlTag()
        {
            addMainTagAttributes();
            var root = new HtmlTag("a");
            addClassesAndAttributesToRoot(root);
            var firstSpan = new HtmlTag("span");
            var secondSpan = new HtmlTag("span");
            secondSpan.Text(_text);
            firstSpan.Append(secondSpan);
            createHiddenSubmit(firstSpan);
            root.Append(firstSpan);
            return root;
        }

        private void addClassesAndAttributesToRoot(HtmlTag root)
        {
            HtmlAttributes.Each(x => root.Attr(x.Key, x.Value));
            CssClasses.Each(x => root.AddClass(x));
        }

        private void createHiddenSubmit(HtmlTag firstSpan)
        {
            var hiddenSubmit = new HtmlTag("input");
            hiddenSubmit.Attr("id", _nameAndId + "Button");
            hiddenSubmit.Attr("name", "hiddenSubmitButton");
            hiddenSubmit.Style("display", "none");
            hiddenSubmit.Attr("type",_asSubmit?"submit":"button");
            firstSpan.Append(hiddenSubmit);
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
            _asSubmit = true;
            CssClasses.Add("kyt_submitButton");
            _btnType = HIGHLIGHTED_BUTTON_CLASS;
            return this;
        }

        public StyledButtonExpression Highlighted()
        {
            _btnType = HIGHLIGHTED_BUTTON_CLASS;
            return this;
        }

        public StyledButtonExpression ElementId(string id)
        {
            _id = id;
            return this;
        }
    }
}