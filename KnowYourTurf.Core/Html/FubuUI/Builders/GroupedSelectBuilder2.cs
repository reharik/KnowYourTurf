using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FubuMVC.Core;
using FubuMVC.UI.Configuration;
using HtmlTags;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using System.Linq;
using KnowYourTurf.Core.Html.FubuUI.HtmlConventionRegistries;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class GroupSelectedBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            var propertyName = def.Accessor.FieldName;
            var listPropertyInfo = def.ModelType.GetProperty("_"+propertyName + "List");
//            var readOnlyListPropertyInfo = def.ModelType.GetProperty(propertyName.Replace("ReadOnly", "") + "List");
            return (listPropertyInfo != null &&
                    listPropertyInfo.PropertyType == typeof(GroupSelectViewModel));
        }

        public override HtmlTag Build(ElementRequest request)
        {
            Action<SelectTag> action = x =>
                                           {
                                               var elementName = KnowYourTurfHtmlConventions.DeriveElementName(request);
                                               x.Attr("data-bind", "foreach: _" + elementName +"List.Groups, selectedOption:"+elementName);
                                               var optGroup = new HtmlTag("optgroup").Attr("data-bind","attr: {label: Label}, foreach: Children");
                                               var opt = new HtmlTag("option").Attr("data-bind","text: Text, option: $data");
                                               optGroup.Children.Add(opt);
                                               x.Children.Add(optGroup);
                                           };
            return new SelectTag(action);
        }
    }
}