using System;
using FubuMVC.UI.Configuration;
using HtmlTags;
using KnowYourTurf.Core.Html.FubuUI.HtmlConventionRegistries;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class DateDisplayBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.PropertyType == typeof(DateTime)
                || def.Accessor.PropertyType == typeof(DateTime?))
                && !def.Accessor.FieldName.EndsWith("Time");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new HtmlTag("span").Attr("data-bind", "dateString:" + KnowYourTurfHtmlConventions.DeriveElementName(request));
        }
    }

    public class TimeDisplayBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.PropertyType == typeof(DateTime)
                || def.Accessor.PropertyType == typeof(DateTime?))
                && def.Accessor.FieldName.EndsWith("Time");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new HtmlTag("span").Attr("data-bind", "timeString:" + KnowYourTurfHtmlConventions.DeriveElementName(request));
        }
    }
}