using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FubuMVC.UI.Tags;
using HtmlTags;
using System.Linq;

namespace KnowYourTurf.Core.Html.FubuUI.HtmlExpressions
{

    public class ViewDisplayDataRangeExpression<VIEWMODEL> where VIEWMODEL : class
    {
        private readonly ITagGenerator<VIEWMODEL> _generator;
        private readonly Expression<Func<VIEWMODEL, object>> _expressionFrom;
        private readonly Expression<Func<VIEWMODEL, object>> _expressionTo;
        private readonly Expression<Func<VIEWMODEL, object>> _expressionToPresent;
        private HtmlTag _htmlRoot;
        private List<string> _inputRootClasses;

        public ViewDisplayDataRangeExpression(ITagGenerator<VIEWMODEL> generator, Expression<Func<VIEWMODEL, object>> expressionFrom, Expression<Func<VIEWMODEL, object>> expressionTo, Expression<Func<VIEWMODEL, object>> expressionToPresent)
        {
            _generator = generator;
            _expressionFrom = expressionFrom;
            _expressionTo = expressionTo;
            _expressionToPresent = expressionToPresent;
        }


        public string ProcessSpan(HtmlTag spanFrom, HtmlTag spanTo, HtmlTag spanToPresent)
        {
            DateTime dateFrom;
            DateTime dateTo;
            bool isPresent;
            string strFrom ="";
            string strTo = "";
            string result = "";

            string dateFormat = "MMMM d,yyyy";

            bool.TryParse(spanToPresent.Text(), out isPresent);

            if (DateTime.TryParse(spanFrom.Text(), out dateFrom))
            {
                //strFrom = dateFrom.ToShortDateString();
                strFrom = dateFrom.ToString(dateFormat);
            }

            if (isPresent)
            {
                strTo = CoreLocalizationKeys.PRESENT.ToString();
            }
            else
            {
                if (DateTime.TryParse(spanTo.Text(), out dateTo))
                {
                    //strTo = dateTo.ToShortDateString();
                    strTo = dateTo.ToString(dateFormat);
                    //TODO: convert to use a enum - this being KYT_defualt short

                }
            }

            if (strTo.IsNotEmpty())
            {
                strTo = " - " + strTo;
            }
            
            result = strFrom + strTo;

            if (result.IsEmpty())
            {
                result = "-";
            }
            
            return result;
        }
        
        public HtmlTag ToHtmlTag()
        {
            _htmlRoot = new HtmlTag("div").AddClass("view_display");
            var spanFrom = _generator.DisplayFor(_expressionFrom);
            var spanTo = _generator.DisplayFor(_expressionTo);
           
            HtmlTag spanToPresent = _expressionToPresent != null
                                        ? spanToPresent = _generator.DisplayFor(_expressionToPresent)
                                        : new HtmlTag("span");

            if (_inputRootClasses != null && _inputRootClasses.Any())
                _htmlRoot.AddClasses(_inputRootClasses);
            
            var spanTag = new HtmlTag("span");
            string result = ProcessSpan(spanFrom, spanTo, spanToPresent);
            
            spanTag.Text(result);
            _htmlRoot.Children.Add(spanTag);
            return _htmlRoot;
        }

        public ViewDisplayDataRangeExpression<VIEWMODEL> AddClassToInputRoot(string cssClass)
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

    }
}