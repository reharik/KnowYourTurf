using System;
using KnowYourTurf.Core.Domain;
using FubuMVC.UI.Configuration;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class ImageFileDisplayBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return ( def.Accessor.FieldName.ToLowerInvariant().Contains("fileurl"));
        }

        public override HtmlTag Build(ElementRequest request)
        {
            HtmlTag root = new HtmlTag("a");
            root.Attr("href", request.RawValue);
            root.Attr("target", "_blank");
            root.Id(request.Accessor.FieldName);
            var img = new HtmlTag("img");
            img.Attr("src", request.RawValue);
            root.Append(img);
            return root;
        }
    }


    public class DateFormatter : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.PropertyType == typeof(DateTime)
                || def.Accessor.PropertyType == typeof(DateTime?))
                && !def.Accessor.FieldName.EndsWith("Time");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var date = request.StringValue().IsNotEmpty() ? DateTime.Parse(request.StringValue()).ToString("MMMM d,yyyy") : "";
            return new HtmlTag("span").Text(date);
        }
    }

}