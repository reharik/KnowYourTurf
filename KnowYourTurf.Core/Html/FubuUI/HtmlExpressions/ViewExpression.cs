using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<string> _rootClasses; 
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
        private string _url;
        private bool _hideIfEmpty;
        private string _dateFormat;

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
            _htmlRoot.AddClass(_noClear ? "view_root_no_clear" : "view_root");

            _htmlRoot = new HtmlTag("div").AddClass("view_root");
            if (_rootId.IsNotEmpty()) _htmlRoot.Id(_rootId);
            if (_rootClasses!=null && _rootClasses.Any()) _htmlRoot.AddClasses(_rootClasses);
            ViewLabelExpression<VIEWMODEL> labelBuilder = new ViewLabelExpression<VIEWMODEL>(_generator, _expression);
            ViewDisplayExpression<VIEWMODEL> displayBuilder = new ViewDisplayExpression<VIEWMODEL>(_generator, _expression);
            addInternalCssClasses(labelBuilder, displayBuilder);
            hideElements(_htmlRoot, labelBuilder, displayBuilder);
            addIds(labelBuilder, displayBuilder);
            addCustomLabel(labelBuilder);
            addCustomAttr(displayBuilder);
            handleSpecialFormats(displayBuilder);
            HtmlTag label = labelBuilder.ToHtmlTag();
            HtmlTag input = displayBuilder.ToHtmlTag();
            if (input.Text().IsEmpty() && _hideIfEmpty) return null;
            _htmlRoot.Append(label);
            _htmlRoot.Append(input);
            return _htmlRoot;
        }

        private void handleSpecialFormats(ViewDisplayExpression<VIEWMODEL> displayBuilder)
        {
            if(_dateFormat.IsNotEmpty())
            {
                displayBuilder.DateFormat(_dateFormat);
            }
        }

        private void addCustomLabel(ViewLabelExpression<VIEWMODEL> label)
        {
            if (_labelDisplay.IsNotEmpty()) label.CustomLabel(_labelDisplay);
        }

        private void addCustomAttr(ViewDisplayExpression<VIEWMODEL> display)
        {
            if (_url.IsNotEmpty()) display.AddUrlToAnchor(_url);
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

        public ViewExpression<VIEWMODEL> AddUrlToDisplayAnchor(string url)
        {
            _url = url;
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
            if (_rootClasses == null)
            {
                _rootClasses = new List<string>();
            }
            if (cssClass.Contains(" "))
            {
                cssClass.Split(' ').Each(_rootClasses.Add);
            }
            else
            {
                _rootClasses.Add(cssClass);
            }
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

        public ViewExpression<VIEWMODEL> HideIfEmpty()
        {
            _hideIfEmpty = true;
            return this;
        }

        public ViewExpression<VIEWMODEL> DateFormat(string format)
        {
            _dateFormat = format;
            return this;
        }




        #endregion

    }
}