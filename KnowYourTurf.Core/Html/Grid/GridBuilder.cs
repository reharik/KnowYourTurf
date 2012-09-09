using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using KnowYourTurf.Security.Interfaces;
using FubuMVC.Core;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Core.Html.Grid
{
    public interface IGridBuilder<ENTITY> where ENTITY : IGridEnabledClass
    {
        List<IGridColumn> columns { get; }
        IList<IDictionary<string, string>> ToGridColumns(User user);
        string[] ToGridRow(ENTITY item, User user, IEnumerable<Action<IGridColumn, ENTITY>> modifications);

        DisplayColumn<ENTITY> DisplayFor(Expression<Func<ENTITY, object>> expression);
        HiddenColumn<ENTITY> HideColumnFor(Expression<Func<ENTITY, object>> expression);
        ImageColumn<ENTITY> ImageColumn();
        ImageButtonColumn<ENTITY> ImageButtonColumn();
        LinkColumn<ENTITY> LinkColumnFor(Expression<Func<ENTITY, object>> expression, string gridName = "");
        GroupingColumn<ENTITY> GroupingColumnFor(Expression<Func<ENTITY, object>> expression);
    }

    public class GridBuilder<ENTITY> : IGridBuilder<ENTITY> where ENTITY : IGridEnabledClass
    {
        private readonly IAuthorizationService _authorizationService;

        public GridBuilder(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        private List<IGridColumn> _columns = new List<IGridColumn>();
        public List<IGridColumn> columns
        {
            get { return _columns; }
        }

        public string[] ToGridRow(ENTITY item, User user, IEnumerable<Action<IGridColumn, ENTITY>> modifications)
        {
            var cellValues = new List<string>();
            foreach (var column in columns)
            {
                modifications.ForEachItem(x => x.Invoke(column, item));
                string value = column.BuildColumn(item, user, _authorizationService);
                cellValues.Add(value ?? string.Empty);
            }
            return cellValues.ToArray();
        }

        public IList<IDictionary<string, string>> ToGridColumns(User user)
        {
            var values = new List<IDictionary<string, string>>();
            foreach (var column in columns)
            {
                bool isAllowed = !column.Operation.IsNotEmpty() || _authorizationService.IsAllowed(user, column.Operation);
                if (!isAllowed) continue;
                values.Add(column.Properties);
            }
            return values;
        }

        public DisplayColumn<ENTITY> DisplayFor(Expression<Func<ENTITY, object>> expression)
        {
            return AddColumn(new DisplayColumn<ENTITY>(expression));
        }

        public HiddenColumn<ENTITY> HideColumnFor(Expression<Func<ENTITY, object>> expression)
        {
            return AddColumn(new HiddenColumn<ENTITY>(expression));
        }

        public ImageColumn<ENTITY> ImageColumn()
        {
            return AddColumn(new ImageColumn<ENTITY>());
        }

        public ImageButtonColumn<ENTITY> ImageButtonColumn()
        {
            return AddColumn(new ImageButtonColumn<ENTITY>());
        }

        public LinkColumn<ENTITY> LinkColumnFor(Expression<Func<ENTITY, object>> expression,string gridName = "")
        {
            return AddColumn(new LinkColumn<ENTITY>(expression,gridName));
        }

        public GroupingColumn<ENTITY> GroupingColumnFor(Expression<Func<ENTITY, object>> expression)
        {
            return AddColumn(new GroupingColumn<ENTITY>(expression));
        }

        public COLUMN AddColumn<COLUMN>(COLUMN column) where COLUMN : ColumnBase<ENTITY>
        {
            var count = _columns.Count;
            column.ColumnIndex = count + 1;
            _columns.Add(column);
            return column;
        }
    }

    public enum GridColumnProperties
    {
        name,
        header,
        actionUrl,
        action,
        formatter,
        formatoptions,
        sortable,
        sortColumn,
        width,
        imageName,
        hidden,
        align,
        toolTip,
        isImage,
        isClickable,
        cssClass
    }

    public enum GridColumnFormatterOptions
    {
        Number_thousandsSeparator,
        Number_decimalSeparator,
        Number_decimalPlaces,
        Number_defaultValue,
        Currency_prefix,
        Currency_suffix,
        Date_srcformat,
        Date_newformat,
    }

    public enum GridColumnAlign
    {
        Left,
        Center,
        Right,
    }

    public enum GridColumnFormatter
    {
        Integer,
        Number,
        EMail,
        Date,
        Currency,
        Checkbox,
        Time
    }

    public class GridDefinition
    {
        public string Url { get; set; }
        public string GridName { get; set; }
        public IList<IDictionary<string, string>> Columns { get; set; }
        public string Title { get; set; }
    }
}