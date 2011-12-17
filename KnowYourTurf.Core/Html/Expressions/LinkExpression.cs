using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;
using HtmlTags;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class LinkExpression 
    {
        private string _href;
        private string _baseUrl;

        public IDictionary<string, string> HtmlAttributes { get; set; }
        public LinkExpression()
        {
            HtmlAttributes = new Dictionary<string, string>();
        }

        public LinkExpression Rel(string relationship)
        {
            HtmlAttributes.Add("rel", relationship);
            return this;
        }

        public LinkExpression BasedAt(string baseUrl)
        {
            _baseUrl = baseUrl;
            return this;
        }

        public LinkExpression Href(string href)
        {
            _href = href;
            return this;
        }

        public LinkExpression Type(string type)
        {
            HtmlAttributes.Add("type", type);
            return this;
        }

        public LinkExpression Title(string title)
        {
            HtmlAttributes.Add("title", title);
            return this;
        }

        public LinkExpression Media(string media)
        {
            HtmlAttributes.Add("media", media);
            return this;
        }

        public LinkExpression AsPingBack()
        {
            Rel("pingback");
            return this;
        }

        public LinkExpression AsAlternate()
        {
            Rel("alternate");
            return this;
        }

        public LinkExpression AsOpenSearch()
        {
            Rel("search");
            Type("application/opensearchdescription+xml");
            return this;
        }

        public LinkExpression AsStyleSheet()
        {
            BasedAt(SiteConfig.Settings().CssPath);
            Rel("stylesheet");
            Type("text/css");
            return this;
        }

        public HtmlTag ToHtmlTag()
        {
            if (_baseUrl.IsNotEmpty())
                _href = UrlContext.Combine(_baseUrl, _href).ToFullUrl();
            HtmlAttributes.Add("href",_href);
            var root = new HtmlTag("link");
            addClassesAndAttributesToRoot(root);
            return root;
        }

        private void addClassesAndAttributesToRoot(HtmlTag root)
        {
            HtmlAttributes.Each(x => root.Attr(x.Key, x.Value));
        }

    }
}