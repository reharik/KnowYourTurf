using System;
using System.Linq.Expressions;
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
    }

    public class EditorInputExpression<VIEWMODEL> : IEditorInputExpression<VIEWMODEL> where VIEWMODEL : class
    {
        private readonly ITagGenerator<VIEWMODEL> _generator;
        private readonly Expression<Func<VIEWMODEL, object>> _expression;
        private HtmlTag _htmlRoot;
        private string _inputRootClass;
        private string _inputClass;
        private bool _hide;
        private string _elementId;

        public EditorInputExpression(ITagGenerator<VIEWMODEL> generator, Expression<Func<VIEWMODEL, object>> expression)
        {
            _generator = generator;
            _expression = expression;
        }

        public HtmlTag ToHtmlTag()
        {
            _htmlRoot = new HtmlTag("div").AddClass("KYT_editor_input");
            HtmlTag input = _generator.InputFor(_expression);
            addInternalCssClasses(_htmlRoot, input);
            if (_hide) input.Style("display","none");
            if (_elementId.IsNotEmpty()) input.Id(_elementId);
            
            _htmlRoot.Child(input);
            return _htmlRoot;
        }

        private void addInternalCssClasses(HtmlTag root, HtmlTag input)
        {
            if (_inputRootClass.IsNotEmpty()) root.AddClass(_inputRootClass);
            if (_inputClass.IsNotEmpty()) input.AddClass(_inputClass);
        }

        public IEditorInputExpression<VIEWMODEL> AddClassToInputRoot(string cssClass)
        {
            _inputRootClass = cssClass;
            return this;
        }

        public IEditorInputExpression<VIEWMODEL> AddClassToInput(string cssClass)
        {
            _inputClass = cssClass;
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
    }
}