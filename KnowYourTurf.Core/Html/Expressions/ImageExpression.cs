using CC.Core;
using CC.Core.Html;
using KnowYourTurf.Core.Config;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class ImageExpression : HtmlCommonExpressionBase
    {
        private readonly string _imageSrcUrl;
        private string _baseUrl;

        public ImageExpression(string imageSrcUrl)
        {
            _imageSrcUrl = imageSrcUrl;
            _baseUrl = SiteConfig.Config.ImagesPath;
        }

        public override string ToString()
        {
            var fullUrl = UrlContext.Combine(_baseUrl, _imageSrcUrl).ToFullUrl();
            var html = @"<img src=""{0}""{1}/>".ToFormat(fullUrl, GetHtmlAttributesString());
            return html;
        }

        public ImageExpression BasedAt(string baseUrl)
        {
            _baseUrl = baseUrl;
            return this;
        }

        public ImageExpression Alt(string altText)
        {
            this.Attr("alt", altText);
            return this;
        }
    }
}