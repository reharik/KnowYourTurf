using System;
using System.Collections.Generic;
using System.Linq;
using KnowYourTurf.Core.Localization;
using FubuMVC.Core.Util;
using FubuMVC.UI.Configuration;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class SelectFromEnumerationBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.HasAttribute<ValueOfAttribute>();
        }

        public override HtmlTag Build(ElementRequest request)
        {
            Action<SelectTag> action = x =>
            {
                var value = request.RawValue;
                Enumeration enumeration = request.Accessor.InnerProperty.GetEnumeration("");
                if (enumeration == null) return;

                IEnumerable<Enumeration> enumerations = Enumeration.GetAllActive(enumeration);
                if (enumerations == null) return;

                foreach (Enumeration option in enumerations)
                {
                    x.Option(option.Key,
                             option.Value.IsEmpty() ? option.Key : option.Value);
                }
                if (value != null && value.ToString().IsNotEmpty())
                {
                    x.SelectByValue(value);
                }
                else
                {
                    Enumeration defaultOption = enumerations.FirstOrDefault(o => o.IsDefault);
                    if (defaultOption != null)
                    {
                        x.SelectByValue(defaultOption.Value.IsEmpty() ? defaultOption.Key : defaultOption.Value);
                    }
                }
                x.AddClass("kyt_fixedWidthDropdown");
                x.AddClass("fixedWidthDropdown");
            };
            return new SelectTag(action);
        }
    }
}