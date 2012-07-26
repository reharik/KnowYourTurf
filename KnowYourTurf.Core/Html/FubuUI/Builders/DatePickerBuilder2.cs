using System;
using FubuMVC.UI.Configuration;
using HtmlTags;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Html.FubuUI.HtmlConventionRegistries;
using KnowYourTurf.Core.Html.FubuUI.Tags;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class TextboxBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return true;
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new TextboxTag().Attr("data-bind", "value:" + KnowYourTurfHtmlConventions.DeriveElementName(request));
        }
    }

    public class DatePickerBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.PropertyType == typeof(DateTime)
                || def.Accessor.PropertyType == typeof(DateTime?))
                && !def.Accessor.FieldName.EndsWith("Time");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new TextboxTag().Attr("data-bind", "value:" + KnowYourTurfHtmlConventions.DeriveElementName(request)).AddClass("datePicker");
        }
    }

    public class TimePickerBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.PropertyType == typeof(DateTime)
                || def.Accessor.PropertyType == typeof(DateTime?))
                && def.Accessor.FieldName.EndsWith("Time");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new TextboxTag().Attr("data-bind", "value:" + KnowYourTurfHtmlConventions.DeriveElementName(request)).AddClass("timePicker");
        }
    }

    public class ImageBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType == typeof(string)
                && def.Accessor.FieldName.EndsWith("ImageUrl");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new HtmlTag("img").Attr("data-bind", "attr: { href: " + KnowYourTurfHtmlConventions.DeriveElementName(request) + " }").Attr("alt", request.Accessor.FieldName);
        }
    }

    public class CheckboxBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType == typeof (bool);
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new CheckboxTag(false).Attr("data-bind",
                                          "checked:" + KnowYourTurfHtmlConventions.DeriveElementName(request));
        }
    }

    public class PasswordBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.Name.ToLowerInvariant().Contains("password");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new PasswordTag().Attr("data-bind", "value:" + KnowYourTurfHtmlConventions.DeriveElementName(request));
        }
    }

    public class EmailDisplayBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.FieldName.ToLowerInvariant().Contains("email");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            HtmlTag root = new HtmlTag("a");
            root.Attr("data-bind", "attr: { href: mailto:" + KnowYourTurfHtmlConventions.DeriveElementName(request)+"}");
            root.Children.Add(new HtmlTag("span").Attr("data-bind", "text:" + KnowYourTurfHtmlConventions.DeriveElementName(request)));
            return root;
        }
    }

    public class MultiSelectBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType == typeof(TokenInputViewModel);
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new HtmlTag("div").Attr("data-bind", "MultiSelect:" + KnowYourTurfHtmlConventions.DeriveElementName(request));
        }
    }
}

