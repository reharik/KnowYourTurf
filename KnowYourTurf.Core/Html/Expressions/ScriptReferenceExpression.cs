using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CC.Core.Html;
using HtmlTags;
using KnowYourTurf.Core.Config;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class ScriptReferenceExpression
    {
        private string _baseUrl;
        private string _indentation;
        private string _fileName;

        public ScriptReferenceExpression(string fileName)
        {
            _baseUrl = SiteConfig.Settings().ScriptsPath;
            _indentation = "";
            _fileName = fileName;
        }

        public ScriptReferenceExpression Indent(string indentation)
        {
            _indentation = indentation;
            return this;
        }

        public ScriptReferenceExpression BasedAt(string baseUrl)
        {
            _baseUrl = baseUrl;
            return this;
        }

        public HtmlTag ToHtmlTag()
        {
            var fullUrl = UrlContext.Combine(_baseUrl, _fileName).ToFullUrl();
            return new HtmlTag("script").Attr("src", fullUrl).Attr("type", "text/javascript");
        }
    }
}