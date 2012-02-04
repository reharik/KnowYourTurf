using System;
using System.Linq;
using System.Linq.Expressions;
using FubuMVC.UI.Tags;
using HtmlTags;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core.Html.FubuUI.HtmlExpressions
{
    public class ViewDisplayExpression<VIEWMODEL> where VIEWMODEL : class
    {
        private readonly ITagGenerator<VIEWMODEL> _generator;
        private readonly Expression<Func<VIEWMODEL, object>> _expression;
        private HtmlTag _htmlRoot;
        private string _inputRootClass;
        private string _inputClass;
        private bool _hide;
        private string _elementId;
        private string _displayName;

        public ViewDisplayExpression(ITagGenerator<VIEWMODEL> generator, Expression<Func<VIEWMODEL, object>> expression)
        {
            _generator = generator;
            _expression = expression;
        }

        public HtmlTag ToHtmlTag()
        {
            _htmlRoot = new HtmlTag("div").AddClass("KYT_view_display");
            if (_hide) _htmlRoot.Hide();
            HtmlTag input = _generator.DisplayFor(_expression);
            if (_displayName.IsNotEmpty())
            {
                var spanTag = input.Children.FirstOrDefault(x => x.TagName() == "span");
                if (spanTag != null)
                    spanTag.Text(_displayName);
            } 
            addInternalCssClasses(_htmlRoot, input);
            if (_elementId.IsNotEmpty()) input.Id(_elementId);

            _htmlRoot.Children.Add(input);
            return _htmlRoot;
        }

        private void addInternalCssClasses(HtmlTag root, HtmlTag input)
        {
            if (_inputRootClass.IsNotEmpty()) root.AddClass(_inputRootClass);
            if (_inputClass.IsNotEmpty()) input.AddClass(_inputClass);
        }

        public ViewDisplayExpression<VIEWMODEL> AddClassToInputRoot(string cssClass)
        {
            _inputRootClass = cssClass;
            return this;
        }

        public ViewDisplayExpression<VIEWMODEL> AddClassToInput(string cssClass)
        {
            _inputClass = cssClass;
            return this;
        }

        public ViewDisplayExpression<VIEWMODEL> Hide()
        {
            _hide = true;
            return this;
        }

        public ViewDisplayExpression<VIEWMODEL> ElementId(string id)
        {
            _elementId = id;
            return this;
        }

        public ViewDisplayExpression<VIEWMODEL> AddDisplayNameForHref(string displayName)
        {
            _displayName = displayName;
            return this;
        }

        public ViewDisplayExpression<VIEWMODEL> AddDisplayNameForHref(StringToken displayName)
        {
            _displayName = displayName.ToString();
            return this;
        }
    }
}