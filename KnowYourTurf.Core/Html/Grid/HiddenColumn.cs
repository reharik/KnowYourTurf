using System;
using System.Linq.Expressions;
using KnowYourTurf.Core.Localization;
using FubuMVC.Core.Util;

namespace KnowYourTurf.Core.Html.Grid
{
    public class HiddenColumn<ENTITY> : ColumnBase<ENTITY> where ENTITY : IGridEnabledClass 
    {
        public HiddenColumn(Expression<Func<ENTITY, object>> expression)
        {
            propertyAccessor = ReflectionHelper.GetAccessor(expression);
            Properties[GridColumnProperties.hidden.ToString()] = "true";
            Properties[GridColumnProperties.name.ToString()] = LocalizationManager.GetLocalString(expression);
            Properties[GridColumnProperties.header.ToString()] = LocalizationManager.GetHeader(expression).HeaderText;
        }
    }
}