using System;
using FubuMVC.UI.Configuration;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class DateTimeDisplayBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType == typeof (DateTime) || def.Accessor.PropertyType == typeof (DateTime?);
        }

        public override HtmlTag Build(ElementRequest request)
        {
            if(request.RawValue == null) return new HtmlTag("span");
            var value = request.Accessor.FieldName.Contains("Time")
                            ? DateTime.Parse(request.RawValue.ToString()).ToShortTimeString()
                            : DateTime.Parse(request.RawValue.ToString()).ToLongDateString();
            return new HtmlTag("span").Text(value);
        }
    }
}