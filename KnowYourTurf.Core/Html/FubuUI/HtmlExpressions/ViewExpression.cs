using System;
using System.Linq.Expressions;
using KnowYourTurf.Core.Localization;
using FubuMVC.UI.Tags;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.HtmlExpressions
{
    public class ViewExpression<VIEWMODEL> where VIEWMODEL : class
    {
        private readonly ITagGenerator<VIEWMODEL> _generator;
        private readonly Expression<Func<VIEWMODEL, object>> _expression;
        private HtmlTag _htmlRoot;
        private string _rootClass;
        private string _labelRootClass;
        private string _labelClass;
        private string _inputRootClass;
        private string _inputClass;
        private bool _hideRoot;
        private bool _hideInput;
        private bool _hideLabel;
        private string _labelId;
        private string _inputId;
        private string _rootId;
        private bool _noClear;
        private string _labelDisplay;
        private string _displayName;

        public ViewExpression(ITagGenerator<VIEWMODEL> generator, Expression<Func<VIEWMODEL, object>> expression)
        {
            _generator = generator;
            _expression = expression;
        }

        public override string ToString()
        {
            return ToHtmlTag().ToString();
        }

        public HtmlTag ToHtmlTag()
        {
            _htmlRoot = new HtmlTag("div");
            _htmlRoot.AddClass(_noClear ? "KYT_view_root_no_clear" : "KYT_view_root");

            _htmlRoot = new HtmlTag("div").AddClass("KYT_view_root");
            if (_rootId.IsNotEmpty()) _htmlRoot.Id(_rootId);
            if (_rootClass.IsNotEmpty()) _htmlRoot.AddClass(_rootClass);
            ViewLabelExpression<VIEWMODEL> labelBuilder = new ViewLabelExpression<VIEWMODEL>(_generator, _expression);
            ViewDisplayExpression<VIEWMODEL> displayBuilder = new ViewDisplayExpression<VIEWMODEL>(_generator, _expression);
            if (_displayName.IsNotEmpty()) displayBuilder.AddDisplayNameForHref(_displayName);
            addInternalCssClasses(labelBuilder, displayBuilder);
            hideElements(_htmlRoot, labelBuilder, displayBuilder);
            addIds(labelBuilder, displayBuilder);
            addCustomLabel(labelBuilder);
            HtmlTag label = labelBuilder.ToHtmlTag();
            HtmlTag input = displayBuilder.ToHtmlTag();

            _htmlRoot.Child(label);
            _htmlRoot.Child(input);
            return _htmlRoot;
        }

        private void addCustomLabel(ViewLabelExpression<VIEWMODEL> label)
        {
            if (_labelDisplay.IsNotEmpty()) label.CustomLabel(_labelDisplay);
        }

        private void addIds(ViewLabelExpression<VIEWMODEL> label, ViewDisplayExpression<VIEWMODEL> input)
        {
            if (_inputId.IsNotEmpty()) input.ElementId(_inputId);
            if (_labelId.IsNotEmpty()) label.ElementId(_labelId);
        }

        private void hideElements(HtmlTag root, ViewLabelExpression<VIEWMODEL> label, ViewDisplayExpression<VIEWMODEL> input)
        {
            if (_hideRoot) root.Hide();
            if (_hideLabel) label.Hide();
            if (_hideInput) input.Hide();
        }

        private void addInternalCssClasses(ViewLabelExpression<VIEWMODEL> labelBuilder, ViewDisplayExpression<VIEWMODEL> inputBuilder)
        {
            if (_labelRootClass.IsNotEmpty()) labelBuilder.AddClassToLabelRoot(_labelRootClass);
            if (_labelClass.IsNotEmpty()) labelBuilder.AddClassToLabel(_labelClass);
            if (_inputRootClass.IsNotEmpty()) inputBuilder.AddClassToInputRoot(_inputRootClass);
            if (_inputClass.IsNotEmpty()) inputBuilder.AddClassToInput(_inputClass);
        }

        #region Extensions

        public ViewExpression<VIEWMODEL> LabelDisplay(StringToken display)
        {
            _labelDisplay = display.ToString();
            return this;
        }

        public ViewExpression<VIEWMODEL> LabelDisplay(string display)
        {
            _labelDisplay = display;
            return this;
        }

        public ViewExpression<VIEWMODEL> NoClear()
        {
            _noClear = true;
            return this;
        }

        public ViewExpression<VIEWMODEL> AddClassToRoot(string cssClass)
        {
            _rootClass = cssClass;
            return this;
        }

        public ViewExpression<VIEWMODEL> AddClassToLabelRoot(string cssClass)
        {
            _labelRootClass = cssClass;
            return this;
        }

        public ViewExpression<VIEWMODEL> AddClassToLabel(string cssClass)
        {
            _labelClass = cssClass;
            return this;
        }

        public ViewExpression<VIEWMODEL> AddClassToInputRoot(string cssClass)
        {
            _inputRootClass = cssClass;
            return this;
        }

        public ViewExpression<VIEWMODEL> AddClassToInput(string cssClass)
        {
            _inputClass = cssClass;
            return this;
        }

        public ViewExpression<VIEWMODEL> HideRoot()
        {
            _hideRoot = true;
            return this;
        }

        public ViewExpression<VIEWMODEL> HideLabel()
        {
            _hideLabel = true;
            return this;
        }

        public ViewExpression<VIEWMODEL> HideInput()
        {
            _hideInput = true;
            return this;
        }

        public ViewExpression<VIEWMODEL> RootId(string id)
        {
            _rootId = id;
            return this;
        }

        public ViewExpression<VIEWMODEL> InputId(string id)
        {
            _inputId = id;
            return this;
        }

        public ViewExpression<VIEWMODEL> labelId(string id)
        {
            _labelId = id;
            return this;
        }

        public ViewExpression<VIEWMODEL> AddDisplayNameForHref(string displayName)
        {
            _displayName = displayName;
            return this;
        }

        #endregion


    }
}