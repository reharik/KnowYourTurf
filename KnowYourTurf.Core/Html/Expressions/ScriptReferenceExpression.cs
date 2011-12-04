using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using KnowYourTurf.Core.Config;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class ScriptReferenceExpression : HtmlCommonExpressionBase
    {
        private readonly IList<string> _scriptRelPaths = new List<string>();
        private string _baseUrl;
        private string _indentation;

        public ScriptReferenceExpression()
        {
            _baseUrl = SiteConfig.Settings().ScriptsPath;
            _indentation = "";
        }

        public ScriptReferenceExpression Add(string scriptRelativePath)
        {
            _scriptRelPaths.Add(scriptRelativePath);
            return this;
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

        public override string ToString()
        {
            var nonEmptyPaths = _scriptRelPaths.Where(path => path.IsNotEmpty());

            if (nonEmptyPaths.Count() == 0)
                return string.Empty;

            var scriptFormat = @"<script type=""text/javascript"" src=""{0}""></script>";

            var html = new StringBuilder();

            nonEmptyPaths.Where(path => path.IsNotEmpty()).Each(path =>
            {
                var fullUrl = UrlContext.Combine(_baseUrl, path).ToFullUrl();

                html.AppendFormat("{0}\r\n{1}", scriptFormat.ToFormat(fullUrl), HttpUtility.HtmlDecode(_indentation));
            });
            
            return html.Length <= (2 + _indentation.Length)
                       ? String.Empty
                       : html.ToString(0, html.Length - (2 + _indentation.Length));
        }
    }
}