using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
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

        //public static string ElementNameFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        //{
        //    var convention = ObjectFactory.Container.GetInstance<IElementNamingConvention>();
        //    return convention.GetName(typeof(T), expression.ToAccessor());
        //}

    }
}