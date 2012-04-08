using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using FubuMVC.Core;
using FubuMVC.UI.Configuration;
using FubuMVC.UI.Tags;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.HtmlExpressions
{
    public class DropdownInputExpression<VIEWMODEL> : IEditorInputExpression<VIEWMODEL> where VIEWMODEL : class
    {
        private readonly ITagGenerator<VIEWMODEL> _generator;
        private readonly Expression<Func<VIEWMODEL, object>> _expression;
        private readonly IEnumerable<SelectListItem> _items;
        private HtmlTag _htmlRoot;
        private string _inputRootClass;
        private string _inputClass;
        private bool _hide;
        private string _elementId;

        public DropdownInputExpression(ITagGenerator<VIEWMODEL> generator, Expression<Func<VIEWMODEL, object>> expression, IEnumerable<SelectListItem> items)
        {
            _generator = generator;
            _expression = expression;
            _items = items;
        }

        public HtmlTag ToHtmlTag()
        {
            _htmlRoot = new HtmlTag("div").AddClass("KYT_editor_input");
            if (_hide) _htmlRoot.Hide();
            ElementRequest request = _generator.GetRequest(_expression);

            Action<SelectTag> action = x =>
                                           {
                                               var value = request.RawValue;
                                               if (_items!=null)
                                               {
                                                   foreach (SelectListItem option in _items)
                                                   {
                                                       x.Option(option.Text, option.Value);
                                                   }
                                                   if (value != null && value.ToString().IsNotEmpty())
                                                   {
                                                       x.SelectByValue(value.ToString());
                                                   }
                                                   else
                                                   {
                                                       SelectListItem defaultOption =
                                                           _items.FirstOrDefault(o => o.Selected);
                                                       if (defaultOption != null)
                                                       {
                                                           x.SelectByValue(defaultOption.Value);
                                                       }
                                                   }
                                               }
                                           };
            SelectTag tag = new SelectTag(action);
            string name = string.Empty;
            request.Accessor.Names.Each(x => name += x + ".");
            name = name.Substring(0, name.Length-1);
            tag.Attr("name", name);
            addInternalCssClasses(_htmlRoot, tag);
            if (_elementId.IsNotEmpty()) tag.Id(_elementId);
            _htmlRoot.Children.Add(tag);
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