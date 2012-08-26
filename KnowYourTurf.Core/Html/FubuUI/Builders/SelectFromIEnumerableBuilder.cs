using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FubuMVC.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Localization;
using FubuMVC.Core.Util;
using FubuMVC.UI.Configuration;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class SelectFromIEnumerableBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            var propertyName = def.Accessor.FieldName;
            var listPropertyInfo = def.ModelType.GetProperty(propertyName + "List");
            var readOnlyListPropertyInfo = def.ModelType.GetProperty(propertyName.Replace("ReadOnly","") + "List");
            return (listPropertyInfo != null && listPropertyInfo.PropertyType == typeof(IEnumerable<SelectListItem>)) || (readOnlyListPropertyInfo != null && readOnlyListPropertyInfo.PropertyType == typeof(IEnumerable<SelectListItem>));
        }

        public override HtmlTag Build(ElementRequest request)
        {
            Action<SelectTag> action = x =>
                                           {
                                               var value = request.RawValue is DomainEntity ? ((DomainEntity)request.RawValue).EntityId : request.RawValue;

                                                var propertyName = request.ToAccessorDef().Accessor.FieldName;
                                               propertyName = propertyName.Contains("ReadOnly")
                                                                  ? propertyName.Replace("ReadOnly","")
                                                                  :propertyName;
                                                var listPropertyInfo = request.ToAccessorDef().ModelType.GetProperty(propertyName+"List");
                                               var selectListItems = listPropertyInfo.GetValue(request.Model, null) as IEnumerable<SelectListItem>;
                                               if (selectListItems == null) return;
                                               
                                               selectListItems.ForEachItem(option=> x.Option(option.Text, option.Value.IsNotEmpty() ? option.Value: ""));

                                               if (value != null && value.ToString().IsNotEmpty())
                                               {
                                                   x.SelectByValue(value.ToString());
                                               }
                                           };
            return new SelectTag(action);
        }
    }
}