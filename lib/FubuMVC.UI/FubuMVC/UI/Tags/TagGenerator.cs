// Type: FubuMVC.UI.Tags.TagGenerator`1
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Linq.Expressions;
using FubuMVC.Core.Util;
using FubuMVC.UI.Configuration;
using HtmlTags;
using Microsoft.Practices.ServiceLocation;

namespace FubuMVC.UI.Tags
{
    public class TagGenerator<T> : ITagGenerator<T> where T : class
    {
        private readonly TagProfileLibrary _library;
        private readonly IElementNamingConvention _namingConvention;
        private readonly IServiceLocator _services;
        private readonly Stringifier _stringifier;
        private T _model;
        private TagProfile _profile;

        public TagGenerator(TagProfileLibrary library, IElementNamingConvention namingConvention,
                            IServiceLocator services, Stringifier stringifier)
        {
            ElementPrefix = string.Empty;
            _library = library;
            _namingConvention = namingConvention;
            _services = services;
            _stringifier = stringifier;
            _profile = _library.DefaultProfile;
        }

        #region ITagGenerator<T> Members

        public T Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public string CurrentProfile
        {
            get { return _profile.Name; }
        }

        public string ElementPrefix { get; set; }

        public void SetProfile(string profileName)
        {
            _profile = _library[profileName];
        }

        public HtmlTag DisplayFor(Expression<Func<T, object>> expression, string profile)
        {
            return buildTag(expression, _library[profile].Display);
        }

        public ElementRequest GetRequest(Expression<Func<T, object>> expression)
        {
            return GetRequest(expression.ToAccessor());
        }

        public ElementRequest GetRequest(Accessor accessor)
        {
            var request = new ElementRequest(_model, accessor, _services, _stringifier);
            determineElementName(request);
            return request;
        }

        public ElementRequest GetRequest<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var request = new ElementRequest(_model, ReflectionHelper.GetAccessor(expression), _services, _stringifier);
            determineElementName(request);
            return request;
        }

        public HtmlTag LabelFor(ElementRequest request)
        {
            return _profile.Label.Build(request);
        }

        public HtmlTag InputFor(ElementRequest request)
        {
            return _profile.Editor.Build(request);
        }

        public HtmlTag DisplayFor(ElementRequest request)
        {
            return _profile.Display.Build(request);
        }

        public HtmlTag LabelFor(Expression<Func<T, object>> expression)
        {
            return buildTag(expression, _profile.Label);
        }

        public HtmlTag LabelFor(Expression<Func<T, object>> expression, string profile)
        {
            return buildTag(expression, _library[profile].Label);
        }

        public HtmlTag InputFor(Expression<Func<T, object>> expression)
        {
            return buildTag(expression, _profile.Editor);
        }

        public HtmlTag InputFor(Expression<Func<T, object>> expression, string profile)
        {
            return buildTag(expression, _library[profile].Editor);
        }

        public HtmlTag DisplayFor(Expression<Func<T, object>> expression)
        {
            return buildTag(expression, _profile.Display);
        }

        #endregion

        private HtmlTag buildTag(Expression<Func<T, object>> expression, TagFactory factory)
        {
            ElementRequest request = GetRequest(expression);
            return factory.Build(request);
        }

        private void determineElementName(ElementRequest request)
        {
            string str = ElementPrefix ?? string.Empty;
            request.ElementId = str + _namingConvention.GetName(typeof (T), request.Accessor);
        }
    }
}