using System;
using System.Collections.Generic;
using FubuMVC.Core.Util;
using FubuMVC.UI.Configuration;
using HtmlTags;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Html.FubuUI.HtmlConventionRegistries;
using KnowYourTurf.Core.Html.FubuUI.Tags;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Web.Controllers;

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
            return new TextboxTag().Attr("data-bind", "dateString:" + KnowYourTurfHtmlConventions.DeriveElementName(request)).AddClass("datePicker");
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
            return new TextboxTag().Attr("data-bind", "timeString:" + KnowYourTurfHtmlConventions.DeriveElementName(request)).AddClass("timePicker");
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
            return new HtmlTag("img").Attr("data-bind",
                " attr: { href: " + KnowYourTurfHtmlConventions.DeriveElementName(request) + " }")
                .Attr("alt", request.Accessor.FieldName);
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
            return new TextboxTag().Id(request.Accessor.Name).AddClass("multiSelect").Attr("data-bind", "MultiSelect:" + KnowYourTurfHtmlConventions.DeriveElementName(request));
        }
    }

    public class PictureGallery : ElementBuilder
    {
        protected  override  bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType == typeof(IEnumerable<PhotoDto>);
        }
        public override HtmlTag Build(ElementRequest request)
        {
            var ul = new HtmlTag("ul").Attr("data-bind", "foreach:" + KnowYourTurfHtmlConventions.DeriveElementName(request));
            var li = new HtmlTag("li");
            li.Children.Add(new HtmlTag("image").Attr("data-bind", "attr:{src:FileUrl}"));
            ul.Children.Add(li);
            return ul;
        }
    }

    public class FileUploader : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.Name == "_submitFileUrl";
        }
        public override HtmlTag Build(ElementRequest request)
        {
            
            var file = new HtmlTag("input").Attr("type", "file").Attr("size",45);
            return file;
        }
    }
}

