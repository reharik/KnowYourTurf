using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using KnowYourTurf.Core.Localization;
using FluentNHibernate.Utils.Reflection;
using FubuMVC.UI.Tags;
using HtmlTags;
using System.Linq;

namespace KnowYourTurf.Core.Html.FubuUI.HtmlExpressions
{
    public class ViewDisplayExpression<VIEWMODEL> where VIEWMODEL : class
    {
        private readonly ITagGenerator<VIEWMODEL> _generator;
        private readonly Expression<Func<VIEWMODEL, object>> _expression;
        private HtmlTag _htmlRoot;
        private List<string> _inputRootClasses;
        private List<string> _inputClasses;
        private bool _hide;
        private string _elementId;
        private string _url;
        private string _hrefDisplayName;
        private string _dateFormat;

        public ViewDisplayExpression(ITagGenerator<VIEWMODEL> generator, Expression<Func<VIEWMODEL, object>> expression)
        {
            _generator = generator;
            _expression = expression;
        }

        public HtmlTag ToHtmlTag()
        {
            _htmlRoot = new HtmlTag("div").AddClass("view_display");
            if (_hide) _htmlRoot.Hide();
            HtmlTag input = _generator.DisplayFor(_expression);
            if(input.HasAttr("href")&& _url.IsNotEmpty())
            {
                input.Attr("href", _url);
            }
            if(_hrefDisplayName.IsNotEmpty())
            {
                var spanTag = input.Children.FirstOrDefault(x => x.TagName() == "span");
                if (spanTag != null) 
                    spanTag.Text(_hrefDisplayName);
            }
            addInternalCssClasses(_htmlRoot, input);
            if (_elementId.IsNotEmpty()) input.Id(_elementId);
            handleSpecialFormats(input);
            _htmlRoot.Append(input);
            return _htmlRoot;
        }

        private void handleSpecialFormats(HtmlTag input)
        {
            if (!_dateFormat.IsNotEmpty()) return;
            DateTime date;
            if (DateTime.TryParse(input.Text(), out date))
            {
                input.Text(date.ToString(_dateFormat));
            }
        }

        private void addInternalCssClasses(HtmlTag root, HtmlTag input)
        {
            if (_inputRootClasses != null && _inputRootClasses.Any()) root.AddClasses(_inputRootClasses);
            if (_inputClasses != null && _inputClasses.Any()) input.AddClasses(_inputClasses);
        }

        public ViewDisplayExpression<VIEWMODEL> AddClassToInputRoot(string cssClass)
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

        public ViewDisplayExpression<VIEWMODEL> AddClassToInput(string cssClass)
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

        public ViewDisplayExpression<VIEWMODEL> AddUrlToAnchor(string url)
        {
            _url = url;
            return this;
        }

        public ViewDisplayExpression<VIEWMODEL> AddDisplayNameForHref(string hrefDisplayName)
        {
            _hrefDisplayName = hrefDisplayName;
            return this;
        }

        public ViewDisplayExpression<VIEWMODEL> AddDisplayNameForHref(StringToken hrefDisplayName)
        {
            _hrefDisplayName = hrefDisplayName.ToString();
            return this;
        }

        public ViewDisplayExpression<VIEWMODEL> DateFormat(string dateFormat)
        {
            _dateFormat = dateFormat;
            return this;
        }
    }
}