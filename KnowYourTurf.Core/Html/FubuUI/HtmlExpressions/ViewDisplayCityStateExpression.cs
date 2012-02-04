using System;
using System.Linq.Expressions;
using KnowYourTurf.Core;
using FubuMVC.UI.Tags;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.HtmlExpressions
{
    public class ViewDisplayCityStateExpression<VIEWMODEL> where VIEWMODEL:class 
    {
        private ITagGenerator<VIEWMODEL> _tagGenerator;
        private Expression<Func<VIEWMODEL, object>> _expressionCity;
        private Expression<Func<VIEWMODEL, object>> _expressionState;
        private Expression<Func<VIEWMODEL, object>> _expressionZip;
        private HtmlTag _htmlRoot;

        public ViewDisplayCityStateExpression(ITagGenerator<VIEWMODEL> tagGenerator,Expression<Func<VIEWMODEL, object>> expressionCity, Expression<Func<VIEWMODEL, object>> expressionState, Expression<Func<VIEWMODEL, object>> expressionZip)
        {
            _expressionZip = expressionZip;
            _expressionState = expressionState;
            _expressionCity = expressionCity;
            _tagGenerator = tagGenerator;
        }

        public HtmlTag ToHtmlTag()
        {
            _htmlRoot = new HtmlTag("div").AddClass("view_display");
            //var spanTag = new HtmlTag("span");
            //var displayForCity = _tagGenerator.DisplayFor(_expressionCity);
            //var displayForState = _tagGenerator.DisplayFor(_expressionState);
            //var displayForZip = _tagGenerator.DisplayFor(_expressionZip);
            
          //  spanTag.Text(ProcessSpan(displayForCity,displayForState,displayForZip));
            _htmlRoot.Children.Add(ToHtmlSpanTag());
            return _htmlRoot;
        }


        public HtmlTag ToHtmlSpanTag()
        {
            var spanCity = _tagGenerator.DisplayFor(_expressionCity);
            var spanState = _tagGenerator.DisplayFor(_expressionState);
            var spanZip = _tagGenerator.DisplayFor(_expressionZip);
            var spanTag = new HtmlTag("span");
            string formCitStZp = "";
            formCitStZp = spanCity.Text().IsNotEmpty() ? spanCity.Text() : "-";
            formCitStZp += ", ";
            formCitStZp += spanState.Text().IsNotEmpty() ? spanState.Text() : "-";
            formCitStZp += " ";
            formCitStZp += spanZip.Text().IsNotEmpty() ? spanZip.Text() : "-";
            spanTag.Text(formCitStZp);
            return spanTag;
        }
    }
}