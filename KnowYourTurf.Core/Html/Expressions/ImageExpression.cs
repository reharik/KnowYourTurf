using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;
using HtmlTags;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class ImageExpression : HtmlTagExpressionBase
    {
        private readonly string _imageSrcUrl;
        private string _baseUrl;

        public ImageExpression(string imageSrcUrl):base("img")
        {
            _imageSrcUrl = imageSrcUrl;
            _baseUrl = SiteConfig.Settings().ImagesPath;
        }

        public override string ToString()
        {
            return ToHtmlTag().ToString();
        }

        private HtmlTag ToHtmlTag()
        {
            var fullUrl = UrlContext.Combine(_baseUrl, _imageSrcUrl).ToFullUrl();
            AddAttr("src", fullUrl);
            return ToHtmlTagBase();
        }

        public ImageExpression BasedAt(string baseUrl)
        {
            _baseUrl = baseUrl;
            return this;
        }

        public ImageExpression Alt(string altText)
        {
            AddAttr("alt", altText);
            return this;
        }
    }
}