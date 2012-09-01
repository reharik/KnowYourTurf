// Type: FubuMVC.UI.FubuPageExtensions
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FubuMVC.Core;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Util;
using FubuMVC.Core.View;
using FubuMVC.Core.View.WebForms;
using FubuMVC.UI.Configuration;
using FubuMVC.UI.Tags;
using HtmlTags;

namespace FubuMVC.UI
{
    public static class FubuPageExtensions
    {
        public static TagGenerator<T> Tags<T>(this IFubuPage<T> page) where T : class
        {
            var tagGenerator = page.Get<TagGenerator<T>>();
            tagGenerator.Model = page.Model;
            tagGenerator.ElementPrefix = page.ElementPrefix;
            return tagGenerator;
        }

        public static void Partial<TInputModel>(this IFubuPage page) where TInputModel : class
        {
            InvokePartial<TInputModel>(page, null);
        }

        public static void Partial<TInputModel>(this IFubuPage page, TInputModel model) where TInputModel : class
        {
            page.Get<IFubuRequest>().Set<TInputModel>(model);
            InvokePartial<TInputModel>(page, null);
        }

        public static RenderPartialExpression<TInputModel> PartialForEach<TInputModel, TPartialModel>(
            this IFubuPage<TInputModel> page, Expression<Func<TInputModel, IEnumerable<TPartialModel>>> listExpression)
            where TInputModel : class
            where TPartialModel : class
        {
            return
                new RenderPartialExpression<TInputModel>(page, page.Get<IPartialRenderer>(), page.Get<IFubuRequest>()).
                    ForEachOf(listExpression);
        }

        private static void InvokePartial<TInputModel>(IFubuPage page, string prefix) where TInputModel : class
        {
            page.Get<IPartialFactory>().BuildPartial(typeof (TInputModel)).InvokePartial();
        }

        public static HtmlTag LinkTo<TInputModel>(this IFubuPage page) where TInputModel : class, new()
        {
            return page.LinkTo(Activator.CreateInstance<TInputModel>());
        }

        public static HtmlTag LinkTo(this IFubuPage page, object inputModel)
        {
            return new LinkTag("", page.Urls.UrlFor(inputModel), new string[0]);
        }

        public static HtmlTag InputFor<T>(this IFubuPage<T> page, Expression<Func<T, object>> expression)
            where T : class
        {
            return Tags(page).InputFor(expression);
        }

        public static HtmlTag LabelFor<T>(this IFubuPage<T> page, Expression<Func<T, object>> expression)
            where T : class
        {
            return page.Tags().LabelFor(expression);
        }

        public static HtmlTag DisplayFor<T>(this IFubuPage<T> page, Expression<Func<T, object>> expression)
            where T : class
        {
            return page.Tags().DisplayFor(expression);
        }

        public static string ElementNameFor<T>(this IFubuPage<T> page, Expression<Func<T, object>> expression)
            where T : class
        {
            return page.Get<IElementNamingConvention>().GetName(typeof (T), expression.ToAccessor());
        }

        public static TextboxTag TextBoxFor<T>(this IFubuPage<T> page, Expression<Func<T, object>> expression)
            where T : class
        {
            return new TextboxTag(page.ElementNameFor(expression), page.Model.ValueOrDefault(expression).ToString());
        }

        public static FormTag FormFor(this IFubuPage page)
        {
            return new FormTag();
        }

        public static FormTag FormFor<TInputModel>(this IFubuPage page) where TInputModel : new()
        {
            return new FormTag(page.Urls.UrlFor(new TInputModel()));
        }

        public static FormTag FormFor<TInputModel>(this IFubuPage page, TInputModel model)
        {
            return new FormTag(page.Urls.UrlFor(model));
        }

        public static FormTag FormFor<TController>(this IFubuPage view, Expression<Action<TController>> expression)
        {
            return new FormTag(view.Urls.UrlFor(expression));
        }

        public static FormTag FormFor(this IFubuPage view, object modelOrUrl)
        {
            return new FormTag(modelOrUrl as string ?? view.Urls.UrlFor(modelOrUrl));
        }

        public static string EndForm(this IFubuPage page)
        {
            return "</form>";
        }
    }
}