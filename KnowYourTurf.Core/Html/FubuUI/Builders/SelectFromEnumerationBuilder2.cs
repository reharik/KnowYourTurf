using System;
using System.Collections.Generic;
using System.Linq;
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
            return new SelectTag();
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
            return new SelectTag();
        }
    }
}