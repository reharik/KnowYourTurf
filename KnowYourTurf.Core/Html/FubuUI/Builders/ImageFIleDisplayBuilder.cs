using FubuMVC.UI.Configuration;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class ImageFileDisplayBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.FieldName.ToLowerInvariant().Contains("fileurl"));
        }

        public override HtmlTag Build(ElementRequest request)
        {
            HtmlTag root = new HtmlTag("a");
            root.Attr("href", request.RawValue);
            root.Attr("target", "_blank");
            root.Id(request.Accessor.FieldName);
            root.AddChildren(new HtmlTag("span"));
            return root;
        }
    } 
}
