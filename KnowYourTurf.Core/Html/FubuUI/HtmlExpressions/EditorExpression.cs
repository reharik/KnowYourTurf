using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using KnowYourTurf.Core.Localization;
using FubuMVC.UI.Tags;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.HtmlExpressions
{
    public class EditorExpression<VIEWMODEL> where VIEWMODEL : class
    {
        private readonly ITagGenerator<VIEWMODEL> _generator;
        private readonly Expression<Func<VIEWMODEL, object>> _expression;
        private HtmlTag _htmlRoot;
        private bool _inlineReverse;
        private IEnumerable<SelectListItem> _dropdownWithItems;
        private List<string> _rootClasses; 
        private string _labelRootClass;
        private string _labelClass;
        private string _inputRootClass;
        private string _inputClass;
        private bool _hideRoot;
        private bool _hideInput;
        private bool _hideLabel;
        private bool _colonAfterLabel;
        private string _labelId;
        private string _inputId;
        private string _rootId;
        private bool _dropdown;
        private bool _noClear;
        private string _labelDisplay;
        private bool _inline;

        public EditorExpression(ITagGenerator<VIEWMODEL> generator, Expression<Func<VIEWMODEL, object>> expression)
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
            if(_inlineReverse)
            {
                return renderInlineReverse();
            }
            return renderStandard();
        }

        private HtmlTag renderStandard()
        {
            _htmlRoot = new HtmlTag("div");
            _htmlRoot.AddClass("editor_root");
            if (_rootId.IsNotEmpty()) _htmlRoot.Id(_rootId);
            if (_rootClasses!=null && _rootClasses.Any()) _htmlRoot.AddClasses(_rootClasses);
            EditorLabelExpression<VIEWMODEL> labelBuilder = new EditorLabelExpression<VIEWMODEL>(_generator, _expression);
            IEditorInputExpression<VIEWMODEL> inputBuilder;
            if (_dropdown)
            {
                inputBuilder = new DropdownInputExpression<VIEWMODEL>(_generator, _expression,_dropdownWithItems);
            }
            else
            {
                inputBuilder = new EditorInputExpression<VIEWMODEL>(_generator, _expression);
            }
            addInternalCssClasses(labelBuilder, inputBuilder);
            hideElements(_htmlRoot, labelBuilder, inputBuilder);
            addIds(labelBuilder, inputBuilder);
            addCustomLabel(labelBuilder, inputBuilder);
            labelBuilder.InLine(_inline);
            HtmlTag input = inputBuilder.ToHtmlTag();
            HtmlTag label = labelBuilder.ToHtmlTag();
            _htmlRoot.Append(label);
            _htmlRoot.Append(input);
            return _htmlRoot;
        }
        
        private void addCustomLabel(EditorLabelExpression<VIEWMODEL> label, IEditorInputExpression<VIEWMODEL> input)
        {
            if (_labelDisplay.IsNotEmpty())
            {
                input.CustomLabel(_labelDisplay);
                label.CustomLabel(_labelDisplay);
            }
            if (_colonAfterLabel) label.ShowColonAfterLabel();
        }

        private void addIds(EditorLabelExpression<VIEWMODEL> label, IEditorInputExpression<VIEWMODEL> input)
        {
            if(_inputId.IsNotEmpty()) input.ElementId(_inputId);
            if (_labelId.IsNotEmpty()) label.ElementId(_labelId);
        }

        private HtmlTag renderInlineReverse()
        {
            _htmlRoot = new HtmlTag("div").AddClass("editor_root");
            if (_rootId.IsNotEmpty()) _htmlRoot.Id(_rootId);
            if (_rootClasses!=null && _rootClasses.Any()) _htmlRoot.AddClasses(_rootClasses);
            EditorLabelExpression<VIEWMODEL> labelBuilder = new EditorLabelExpression<VIEWMODEL>(_generator, _expression);
            EditorInputExpression<VIEWMODEL> inputBuilder = new EditorInputExpression<VIEWMODEL>(_generator, _expression);
            addInternalCssClasses(labelBuilder, inputBuilder);
            hideElements(_htmlRoot, labelBuilder, inputBuilder);
            addIds(labelBuilder, inputBuilder);
            addCustomLabel(labelBuilder, inputBuilder);
            HtmlTag label = labelBuilder.LeadingColon().ToHtmlTag();
            HtmlTag input = inputBuilder.ToHtmlTag();
            _htmlRoot.Append(input);
            _htmlRoot.Append(label);
            return _htmlRoot;
        }

        private void hideElements(HtmlTag root, EditorLabelExpression<VIEWMODEL> label, IEditorInputExpression<VIEWMODEL> input)
        {
            if (_hideRoot) root.Hide();
            if (_hideLabel) label.Hide();
            if (_hideInput) input.Hide();
        }

        private void addInternalCssClasses(EditorLabelExpression<VIEWMODEL> labelBuilder, IEditorInputExpression<VIEWMODEL> inputBuilder)
        {
            if (_labelRootClass.IsNotEmpty()) labelBuilder.AddClassToLabelRoot(_labelRootClass);
            if (_labelClass.IsNotEmpty()) labelBuilder.AddClassToLabel(_labelClass);
            if (_inputRootClass.IsNotEmpty()) inputBuilder.AddClassToInputRoot(_inputRootClass);
            if (_inputClass.IsNotEmpty()) inputBuilder.AddClassToInput(_inputClass);
        }

        #region Extensions

        public EditorExpression<VIEWMODEL> RadioButton()
        {
            _inlineReverse = true;
            return this;
        }
        public EditorExpression<VIEWMODEL> RadioButton(string groupName)
        {
            _inlineReverse = true;
            return this;
        }

        public EditorExpression<VIEWMODEL> LabelDisplay(StringToken display)
        {
            _labelDisplay = display.ToString();
            return this;
        }

        public EditorExpression<VIEWMODEL> LabelDisplay(string display)
        {
            _labelDisplay = display;
            return this;
        }
        
        public EditorExpression<VIEWMODEL> NoClear()
        {
            _noClear = true;
            return this;
        }

        public EditorExpression<VIEWMODEL> InLine()
        {
            _inline = true;
            return this;
        }

        public EditorExpression<VIEWMODEL> InlineReverse()
        {
            _inlineReverse = true;
            return this;
        }

        public EditorExpression<VIEWMODEL> ShowColonAfterLabel()
        {
            _colonAfterLabel= true;
            return this;
        }


        public EditorExpression<VIEWMODEL> FillWith(IEnumerable<SelectListItem> enumerable)
        {
            _dropdown = true;
            _dropdownWithItems = enumerable;
            return this;
        }

        public EditorExpression<VIEWMODEL> AddClassToRoot(string cssClass)
        {
            if(_rootClasses==null)
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

        public EditorExpression<VIEWMODEL> AddClassToLabelRoot(string cssClass)
        {
            _labelRootClass = cssClass;
            return this;
        }

        public EditorExpression<VIEWMODEL> AddClassToLabel(string cssClass)
        {
            _labelClass = cssClass;
            return this;
        }

        public EditorExpression<VIEWMODEL> AddClassToInputRoot(string cssClass)
        {
            _inputRootClass = cssClass;
            return this;
        }

        public EditorExpression<VIEWMODEL> AddClassToInput(string cssClass)
        {
            _inputClass = cssClass;
            return this;
        }

        public EditorExpression<VIEWMODEL> HideRoot()
        {
            _hideRoot = true;
            return this;
        }

        public EditorExpression<VIEWMODEL> HideLabel()
        {
            _hideLabel = true;
            return this;
        }
        
        public EditorExpression<VIEWMODEL> HideInput()
        {
            _hideInput = true;
            return this;
        }

        public EditorExpression<VIEWMODEL> RootId(string id)
        {
            _rootId = id;
            return this;
        }

        public EditorExpression<VIEWMODEL> InputId(string id)
        {
            _inputId = id;
            return this;
        }

        public EditorExpression<VIEWMODEL> labelId(string id)
        {
            _labelId = id;
            return this;
        }


        
        #endregion

    }
}