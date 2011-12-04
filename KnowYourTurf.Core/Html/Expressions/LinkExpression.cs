using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using KnowYourTurf.Core.Config;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class LinkExpression : HtmlCommonExpressionBase
    {
        private string _href;
        private string _baseUrl;

        public LinkExpression Rel(string relationship)
        {
            this.Attr("rel", relationship);
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
            this.Attr("type", type);
            return this;
        }

        public LinkExpression Title(string title)
        {
            this.Attr("title", title);
            return this;
        }

        public LinkExpression Media(string media)
        {
            this.Attr("media", media);
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

        public LinkExpression<T> FromList<T>(IEnumerable<T> links, Action<T, LinkExpression> setupAction)
        {
            return new LinkExpression<T>(links, setupAction);
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

        public override string ToString()
        {
            if (_baseUrl.IsNotEmpty())
                _href = UrlContext.Combine(_baseUrl, _href);

            this.Attr("href", _href.ToFullUrl());

            return @"<link{0}/>".ToFormat(GetHtmlAttributesString());
        }
    }

    public class LinkExpression<T>
    {
        private readonly IEnumerable<T> _links;
        private readonly Action<T, LinkExpression> _setupAction;
        private string _indentation;


        public LinkExpression(IEnumerable<T> links, Action<T, LinkExpression> setupAction)
        {
            _links = links;
            _setupAction = setupAction;
            _indentation = "";
        }

        public LinkExpression<T> Indent(string indentation)
        {
            _indentation = indentation;
            return this;
        }
        
        public override string ToString()
        {
            var output = new StringBuilder();
            _links.Each(a =>
            {
                var setup = new LinkExpression();
                _setupAction(a, setup);

                var link = setup.ToString();
                
                if (link.IsNotEmpty())
                    output.AppendFormat("{0}\r\n{1}", link, HttpUtility.HtmlDecode(_indentation));
            });

            return output.ToString(0, output.Length - (2 + _indentation.Length));
        }
    }
}