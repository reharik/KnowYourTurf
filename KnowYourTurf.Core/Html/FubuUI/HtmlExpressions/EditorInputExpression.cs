using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using FubuMVC.Core.Util;
using FubuMVC.UI.Tags;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.HtmlExpressions
{
    public interface IEditorInputExpression<VIEWMODEL> where VIEWMODEL : class
    {
        HtmlTag ToHtmlTag();
        IEditorInputExpression<VIEWMODEL> AddClassToInputRoot(string cssClass);
        IEditorInputExpression<VIEWMODEL> AddClassToInput(string cssClass);
        IEditorInputExpression<VIEWMODEL> Hide();
        IEditorInputExpression<VIEWMODEL> ElementId(string id);
        IEditorInputExpression<VIEWMODEL> CustomLabel(string labelDisplay);
    }

    public class EditorInputExpression<VIEWMODEL> : IEditorInputExpression<VIEWMODEL> where VIEWMODEL : class
    {
        private readonly ITagGenerator<VIEWMODEL> _generator;
        private readonly Expression<Func<VIEWMODEL, object>> _expression;
        private HtmlTag _htmlRoot;
        private List<string> _inputRootClasses;
        private List<string> _inputClasses;
        private bool _hide;
        private string _elementId;
        private string _labelDisplay;

        public EditorInputExpression(ITagGenerator<VIEWMODEL> generator, Expression<Func<VIEWMODEL, object>> expression)
        {
            _generator = generator;
            _expression = expression;
        }

        public HtmlTag ToHtmlTag()
        {
            _htmlRoot = new HtmlTag("div").AddClass("editor_input");
            if (_hide) _htmlRoot.Hide();
            HtmlTag input = _generator.InputFor(_expression);
            addInternalCssClasses(_htmlRoot, input);
            if (_elementId.IsNotEmpty()) input.Id(_elementId);

            _htmlRoot.Append(input);
            return _htmlRoot;
        }

        private void addInternalCssClasses(HtmlTag root, HtmlTag input)
        {
            if(input.GetValidationHelpers().Any())
            {
               var origional = ReflectionHelper.GetProperty(_expression).Name;
               input.GetValidationHelpers().Each(x => x.ErrorMessage = x.ErrorMessage.Replace(origional, _labelDisplay));
            }
            if (_inputRootClasses!=null&&_inputRootClasses.Any()) root.AddClasses(_inputRootClasses);
            if (_inputClasses!=null&&_inputClasses.Any()) input.AddClasses(_inputClasses);
        }

        public IEditorInputExpression<VIEWMODEL> AddClassToInputRoot(string cssClass)
        {
            if (_inputRootClasses == null)
            {
                _inputRootClasses = new List<string>();
            }
            if (cssClass.Contains(" "))
            {
                cssClass.Split(' ').Each(_inputRootClasses.Add);
            }
            else
            {
                _inputRootClasses.Add(cssClass);
            }
            return this;
        }

        public IEditorInputExpression<VIEWMODEL> AddClassToInput(string cssClass)
        {
            if (_inputClasses == null)
            {
                _inputClasses = new List<string>();
            }
            if (cssClass.Contains(" "))
            {
                cssClass.Split(' ').Each(_inputClasses.Add);
            }
            else
            {
                _inputClasses.Add(cssClass);
            }
            return this;
        }

        public IEditorInputExpression<VIEWMODEL> Hide()
        {
            _hide = true;
            return this;
        }

        public IEditorInputExpression<VIEWMODEL> ElementId(string id)
        {
            _elementId = id;
            return this;
        }

        public IEditorInputExpression<VIEWMODEL> CustomLabel(string labelDisplay)
        {
            _labelDisplay = labelDisplay;
            return this;
        }
    }

    internal class ValidationMetaData
    {
        public bool required { get; set; }
        public bool number { get; set; }
        public IEnumerable<Message> messages { get; set; }
    }

    internal class Message
    {
        public string required { get; set; }
        public string number { get; set; }
    }
}