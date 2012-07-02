using System;
using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.UI.Configuration;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class ListDisplayBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType == typeof (IEnumerable<string>);
        }

        public override HtmlTag Build(ElementRequest request)
        {
            HtmlTag root = new HtmlTag("div");
            root.AddClass("KYT_ListDisplayRoot");
            var selectListItems = request.RawValue as IEnumerable<string>;
            if (selectListItems == null) return root;
            selectListItems.ForEachItem(item => root.Children.Add(new HtmlTag("div").Text(item)));
            return root;
        }
    }
}