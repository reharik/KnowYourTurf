using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FubuMVC.UI.Configuration;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class GroupSelectedBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            var propertyName = def.Accessor.FieldName;
            var listPropertyInfo = def.ModelType.GetProperty(propertyName + "List");
            return listPropertyInfo != null && listPropertyInfo.PropertyType == typeof(IDictionary<string, IEnumerable<SelectListItem>>);
        }

        public override HtmlTag Build(ElementRequest request)
        {
            Action<SelectTag> action = x =>
            {
                var value = request.RawValue;

                var propertyName = request.ToAccessorDef().Accessor.FieldName;
                var listPropertyInfo = request.ToAccessorDef().ModelType.GetProperty(propertyName + "List");
                var dictionary = listPropertyInfo.GetValue(request.Model, null) as IDictionary<string, IEnumerable<SelectListItem>>;
                if (dictionary == null) return;
                x.Option(CoreLocalizationKeys.SELECT_ITEM.ToString(),"");
                dictionary.Keys.Each(key =>
                {
                    x.OptGroup(key);
                    dictionary[key].Each(l => x.Option(l.Text, l.Value+"_"+key));
                });
                if (value != null && value.ToString().IsNotEmpty())
                {
                    x.SelectByValue(value.ToString());
                }
            };
            return new SelectTag(action);
        }
    }
}