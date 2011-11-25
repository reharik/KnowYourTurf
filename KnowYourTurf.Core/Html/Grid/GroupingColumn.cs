using System;
using System.Linq.Expressions;
using FubuMVC.Core.Util;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core.Html.Grid
{
    public class GroupingColumn<ENTITY> : ColumnBase<ENTITY> where ENTITY : IGridEnabledClass 
    {
        private string _groupingColumnName;

        public GroupingColumn(Expression<Func<ENTITY, object>> expression)
        {
            propertyAccessor = ReflectionHelper.GetAccessor(expression);
            Properties[GridColumnProperties.name.ToString()] = LocalizationManager.GetLocalString(expression);
            Properties[GridColumnProperties.header.ToString()] = LocalizationManager.GetHeader(expression).HeaderText;
        }

        public GroupingColumn<ENTITY> FormatValue(GridColumnFormatter formatter)
        {
            Properties[GridColumnProperties.formatter.ToString()] = formatter.ToString().ToLowerInvariant();
            return this;
        }

        public GroupingColumn<ENTITY> GroupingColumnName(string groupingColumnName)
        {
            Properties[GridColumnProperties.name.ToString()] = groupingColumnName;
            return this;
        }

    }
}