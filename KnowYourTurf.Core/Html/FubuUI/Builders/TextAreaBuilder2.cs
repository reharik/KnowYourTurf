using System;
using FubuMVC.Core.Util;
using FubuMVC.UI.Configuration;
using HtmlTags;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using KnowYourTurf.Core.Html.FubuUI.HtmlConventionRegistries;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class TextAreaBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.PropertyType == typeof(string)
                    && def.Accessor.HasAttribute<TextAreaAttribute>());
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new HtmlTag("textarea").Attr("data-bind", "value:" + KnowYourTurfHtmlConventions2.DeriveElementName(request)).AddClass("textArea").Attr("name", request.ElementId);
        }
    }
}

