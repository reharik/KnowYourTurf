using System;
using System.Collections.Generic;
using System.Linq;
using KnowYourTurf.Core.Html.FubuUI.HtmlConventionRegistries;
using KnowYourTurf.Core.Localization;
using FubuMVC.Core.Util;
using FubuMVC.UI.Configuration;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class SelectFromEnumerationBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.HasAttribute<ValueOfEnumerationAttribute>();
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var selectTag = new SelectTag();
            var elementName = KnowYourTurfHtmlConventions.DeriveElementName(request);
            selectTag.Attr("data-bind", "options:" + elementName + "List," +
                                        "optionsText:'Text'," +
                                        "optionsValue:'Value'," +
                                        "value:" + elementName );
            return selectTag;
        }
    }

    public class SelectFromIEnumerableBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.HasAttribute<ValueOfIEnumerableAttribute>();
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var selectTag = new SelectTag();
            var elementName = KnowYourTurfHtmlConventions.DeriveElementName(request);
            var elementRoot = elementName.Contains("EntityId") ? elementName.Replace(".EntityId", "") : elementName;
            selectTag.Attr("data-bind", "options:" + elementRoot +"List," +
                                        "optionsValue:'Value'," +
                                        "optionsText:'Text'," +
                                        "value:" + elementName);

            return selectTag;
        }
    }
}