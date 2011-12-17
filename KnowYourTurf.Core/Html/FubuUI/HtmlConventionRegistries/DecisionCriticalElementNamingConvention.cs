using System;
using FubuMVC.Core.Util;
using FubuMVC.UI.Configuration;

namespace KnowYourTurf.Core.Html.FubuUI.HtmlConventionRegistries
{
    public class KnowYourTurfElementNamingConvention : IElementNamingConvention
    {
        public string GetName(Type modelType, Accessor accessor)
        {
            return accessor.FieldName;
        }
    }
}