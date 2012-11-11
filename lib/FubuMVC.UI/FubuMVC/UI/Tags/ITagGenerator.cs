// Type: FubuMVC.UI.Tags.ITagGenerator`1
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Linq.Expressions;
using FubuMVC.Core.Util;
using FubuMVC.UI.Configuration;
using HtmlTags;

namespace FubuMVC.UI.Tags
{
    public interface ITagGenerator<T> where T : class
    {
        string ElementPrefix { get; set; }

        string CurrentProfile { get; }

        T Model { get; set; }

        void SetProfile(string profileName);

        HtmlTag LabelFor(Expression<Func<T, object>> expression);

        HtmlTag LabelFor(Expression<Func<T, object>> expression, string profile);

        HtmlTag InputFor(Expression<Func<T, object>> expression);

        HtmlTag InputFor(Expression<Func<T, object>> expression, string profile);

        HtmlTag DisplayFor(Expression<Func<T, object>> expression);

        HtmlTag DisplayFor(Expression<Func<T, object>> expression, string profile);

        ElementRequest GetRequest(Expression<Func<T, object>> expression);

        HtmlTag LabelFor(ElementRequest request);

        HtmlTag InputFor(ElementRequest request);

        HtmlTag DisplayFor(ElementRequest request);

        ElementRequest GetRequest<TProperty>(Expression<Func<T, TProperty>> expression);

        ElementRequest GetRequest(Accessor accessor);
    }
}