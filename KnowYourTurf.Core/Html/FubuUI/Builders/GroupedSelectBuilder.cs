using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FubuMVC.Core;
using FubuMVC.UI.Configuration;
using HtmlTags;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class GroupSelectedBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            var propertyName = def.Accessor.FieldName;
            var listPropertyInfo = def.ModelType.GetProperty(propertyName + "List");
            var readOnlyListPropertyInfo = def.ModelType.GetProperty(propertyName.Replace("ReadOnly", "") + "List");
            return (listPropertyInfo != null && listPropertyInfo.PropertyType == typeof(IDictionary<string, IEnumerable<SelectListItem>>)) || (readOnlyListPropertyInfo != null && readOnlyListPropertyInfo.PropertyType == typeof(IDictionary<string, IEnumerable<SelectListItem>>));
        }

        public override HtmlTag Build(ElementRequest request)
        {
            Action<SelectTag> action = x =>
            {
                var value = request.RawValue is DomainEntity ? ((DomainEntity)request.RawValue).EntityId : request.RawValue;

                var propertyName = request.ToAccessorDef().Accessor.FieldName;
                propertyName = propertyName.Contains("ReadOnly")
                                   ? propertyName.Replace("ReadOnly", "")
                                   : propertyName;

                var listPropertyInfo = request.ToAccessorDef().ModelType.GetProperty(propertyName + "List");
                var dictionary = listPropertyInfo.GetValue(request.Model, null) as IDictionary<string, IEnumerable<SelectListItem>>;
                if (dictionary == null) return;
                x.Option(CoreLocalizationKeys.SELECT_ITEM.ToString(),"");

                var valueToSelect = string.Empty;
                dictionary.Keys.ForEachItem(key =>
                {
                    x.OptionGroup(key);
                    dictionary[key].ForEachItem(l =>
                                                    {
                                                        if(value!=null && l.Value == value.ToString())
                                                        {
                                                            valueToSelect = l.Value;
                                                        }
                                                        x.Option(l.Text, l.Value);
                                                    });
                });

                x.SelectByValue(valueToSelect);
            };
            return new SelectTag(action);
        }
    }

    public static class SelectTagExtensions
    {
        public static HtmlTag MakeOptionGroup(this SelectTag tag, string display)
        {
            return new HtmlTag("optgroup").Attr("label", display).Attr("value", "");
        }

        public static HtmlTag OptionGroup(this SelectTag tag, string display)
        {
            HtmlTag option = tag.MakeOptionGroup(display);
            tag.Append(option);
            return option;

        }
    }
}