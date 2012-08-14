using System;
using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.UI.Configuration;
using HtmlTags;
using KnowYourTurf.Core.Html.FubuUI.HtmlConventionRegistries;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class ListDisplayBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType == typeof (IEnumerable<string>);
        }

        public override HtmlTag Build(ElementRequest request)
        {
            HtmlTag root = new HtmlTag("div").Attr("data-bind", "foreach: "+ KnowYourTurfHtmlConventions.DeriveElementName(request));
            var child = new HtmlTag("div").Attr("data-bind", "text: $data" );
            root.Append(child);
            return root;
        }
    }
}