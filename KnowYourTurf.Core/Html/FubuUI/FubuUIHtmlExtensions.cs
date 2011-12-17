using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Expressions;
using KnowYourTurf.Core.Html.FubuUI.HtmlExpressions;
using FubuMVC.UI.Tags;
using HtmlTags;
using FubuMVC.Core.Util;

namespace KnowYourTurf.Core.Html.FubuUI
{
    public static class FubuUIHtmlExtensions
    {
        private static ITagGenerator<T> GetGenerator<T>(HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            TagGenerator<T> generator = DependencyResolver.Current.GetService<ITagGenerator<T>>() as TagGenerator<T>;
            generator.Model = helper.ViewData.Model;
            if (helper.ViewData.TemplateInfo.HtmlFieldPrefix.IsNotEmpty())
            {
                generator.ElementPrefix = helper.ViewData.TemplateInfo.HtmlFieldPrefix + ".";
            }
            else
            {
                Accessor accessor = expression.ToAccessor();
                if (!accessor.OwnerType.Name.ToLowerInvariant().Contains("viewmodel"))
                {
                    generator.ElementPrefix = accessor.OwnerType.Name + ".";
                }
            }
            return generator;
        }

        public static HtmlTag InputFubu<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            ITagGenerator<T> generator = GetGenerator<T>(helper, expression);
            return generator.InputFor(expression);
        }
        
        public static HtmlTag LabelFubu<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            ITagGenerator<T> generator = GetGenerator<T>(helper, expression);
            HtmlTag tag = generator.LabelFor(expression);
            return tag;
        }

        public static HtmlTag DisplayFubu<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            ITagGenerator<T> generator = GetGenerator<T>(helper, expression);
            return generator.DisplayFor(expression);
        }

        public static EditorExpression<T> EditorInlineReverse<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            ITagGenerator<T> generator = GetGenerator(helper, expression);
            return new EditorExpression<T>(generator, expression).InlineReverse();
        }

        public static EditorExpression<T> SubmissionFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            ITagGenerator<T> generator = GetGenerator(helper, expression);
            return new EditorExpression<T>(generator, expression);
        }

        public static EditorExpression<T> DropdownSubmissionFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression, IEnumerable<SelectListItem> fillWith) where T : class
        {
            ITagGenerator<T> generator = GetGenerator(helper, expression);
            return new EditorExpression<T>(generator, expression).FillWith(fillWith);
        }

        public static ViewExpression<T> ViewFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            ITagGenerator<T> generator = GetGenerator(helper, expression);
            return new ViewExpression<T>(generator, expression);
        }

        public static ViewDisplayExpression<T> ViewDisplayFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            ITagGenerator<T> generator = GetGenerator(helper, expression);
            return new ViewDisplayExpression<T>(generator, expression);
        }

        public static ViewDisplayDataRangeExpression<T> ViewDisplayDateRangeFor<T>(this HtmlHelper<T> helper, 
                                                                            Expression<Func<T, object>> expressionFrom,
                                                                            Expression<Func<T, object>> expressionTo,
                                                                            Expression<Func<T, object>> expressionToPresent = null) where T : class
        {
            ITagGenerator<T> generator = GetGenerator(helper, expressionFrom);
            return new ViewDisplayDataRangeExpression<T>(generator, expressionFrom, expressionTo, expressionToPresent);
        }


        public static ViewDisplayCityStateExpression<T> ViewDisplayCityStateFor<T>(this HtmlHelper<T> helper,
                                                                           Expression<Func<T, object>> expressionCity,
                                                                           Expression<Func<T, object>> expressionState,
                                                                           Expression<Func<T, object>> expressionZip) where T : class
        {
            ITagGenerator<T> generator = GetGenerator(helper, expressionCity);
            return new ViewDisplayCityStateExpression<T>(generator, expressionCity, expressionState, expressionZip);
        }

        public static ViewDisplayAddressExpression<T> ViewDisplayAddressFor<T>(this HtmlHelper<T> helper,
              Expression<Func<T, object>> address) where T : class
        {
            ITagGenerator<T> generator = GetGenerator(helper, address);
            return new ViewDisplayAddressExpression<T>(generator, address);
        }



        //public static string ElementNameFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        //{
        //    var convention = ObjectFactory.Container.GetInstance<IElementNamingConvention>();
        //    return convention.GetName(typeof(T), expression.ToAccessor());
        //}

    }

    
}