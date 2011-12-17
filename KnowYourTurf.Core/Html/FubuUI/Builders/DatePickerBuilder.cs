using System;
using FubuMVC.UI.Configuration;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class DatePickerBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.PropertyType == typeof(DateTime)
                || def.Accessor.PropertyType == typeof(DateTime?))
                && !def.Accessor.FieldName.EndsWith("Time");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var date = request.StringValue().IsNotEmpty() ? DateTime.Parse(request.StringValue()).ToShortDateString() : "";
            return new TextboxTag().Attr("value", date).AddClass("datePicker");
        }
    }

    public class CheckboxBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.PropertyType == typeof (bool)
                    || def.Accessor.PropertyType == typeof (bool?));
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var isChecked =request.RawValue!=null ? (bool) request.RawValue:false;
            return new CheckboxTag(isChecked);
        }
    }

    public class TimePickerBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.PropertyType == typeof(DateTime)
                || def.Accessor.PropertyType == typeof(DateTime?))
                && def.Accessor.FieldName.EndsWith("Time");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var date = request.StringValue().IsNotEmpty() ? DateTime.Parse(request.StringValue()).ToShortTimeString() : "";
            return new TextboxTag().Attr("value", date).AddClass("timePicker");
        }
    }

    public class ImageBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType == typeof(string)
                && def.Accessor.FieldName.EndsWith("ImageUrl");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new HtmlTag("img").Attr("src", request.StringValue()).Attr("alt",request.Accessor.FieldName);
        }
    }
}
    
