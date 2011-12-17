using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FubuMVC.UI.Tags;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.HtmlExpressions
{
    public class ViewLabelExpression<VIEWMODEL> where VIEWMODEL : class
    {
       private readonly ITagGenerator<VIEWMODEL> _generator;
        private readonly Expression<Func<VIEWMODEL, object>> _expression;
        private HtmlTag _htmlRoot;
        private bool _noColon;
        private bool _LeadingColon;
        private List<string> _labelRootClasses;
        private List<string> _labelClasses;
        private bool _hide;
        private string _elementId;
        private string _customDisplay;

        public ViewLabelExpression(ITagGenerator<VIEWMODEL> generator, Expression<Func<VIEWMODEL, object>> expression)
        {
            _generator = generator;
            _expression = expression;
        }

        public HtmlTag ToHtmlTag()
        {
            _htmlRoot = new HtmlTag("div").AddClass("view_label");
            if (_labelRootClasses != null && _labelRootClasses.Any()) _htmlRoot.AddClasses(_labelRootClasses);
            if (_hide) _htmlRoot.Hide();
            HtmlTag label = _generator.LabelFor(_expression);
            if (_labelClasses != null && _labelClasses.Any()) label.AddClasses(_labelClasses);
            if (_elementId.IsNotEmpty()) label.Id(_elementId);
            if (_customDisplay.IsNotEmpty()) label.Text(_customDisplay);
            if (!_noColon && !_LeadingColon)
            {
                label.Text(label.Text() + ":");
            }
            if (!_noColon && _LeadingColon)
            {
                label.Text(":" + label.Text());
            }
            _htmlRoot.Append(label);
            return _htmlRoot;
        }

        public ViewLabelExpression<VIEWMODEL> NoColon()
        {
            _noColon = true;
            return this;
        }

        public ViewLabelExpression<VIEWMODEL> LeadingColon()
        {
            _LeadingColon = true;
            return this;
        }

        public ViewLabelExpression<VIEWMODEL> AddClassToLabelRoot(string cssClass)
        {
            if (_labelRootClasses == null)
            {
                _labelRootClasses = new List<string>();
            }
            if (cssClass.Contains(" "))
            {
                cssClass.Split(' ').Each(_labelRootClasses.Add);
            }
            else
            {
                _labelRootClasses.Add(cssClass);
            }
            return this;
        }

        public ViewLabelExpression<VIEWMODEL> AddClassToLabel(string cssClass)
        {
            if (_labelClasses == null)
            {
                _labelClasses = new List<string>();
            }
            if (cssClass.Contains(" "))
            {
                cssClass.Split(' ').Each(_labelClasses.Add);
            }
            else
            {
                _labelClasses.Add(cssClass);
            }
            return this;
        }

        public ViewLabelExpression<VIEWMODEL> Hide()
        {
            _hide = true;
            return this;
        }

        public ViewLabelExpression<VIEWMODEL> ElementId(string id)
        {
            _elementId = id;
            return this;
        }

        public ViewLabelExpression<VIEWMODEL> CustomLabel(string display)
        {
            _customDisplay = display;
            return this;
        }

    }
}