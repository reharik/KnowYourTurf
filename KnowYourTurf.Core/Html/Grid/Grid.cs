using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HtmlTags;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Core.Html.Grid
{
    public interface IGrid<T> where T : IGridEnabledClass
    {
        void AddColumnModifications(Action<IGridColumn, T> modification);
        GridDefinition GetGridDefinition(string url);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<T> items, string gridName = null);
    }

    public abstract class Grid<T> : IGrid<T> where T : IGridEnabledClass
    {
        protected readonly IGridBuilder<T> GridBuilder;
        private readonly ISessionContext _sessionContext;
        private readonly IRepository _repository;
        private IList<Action<IGridColumn, T>> _modifications;

        protected Grid(IGridBuilder<T> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
        {
            GridBuilder = gridBuilder;
            _sessionContext = sessionContext;
            _repository = repository;
            _modifications = new List<Action<IGridColumn, T>>();
        }

        private IList<IDictionary<string, string>> GetGridColumns(User user)
        {
            return GridBuilder.ToGridColumns(user);
        }

        private IEnumerable GetGridRows(IEnumerable rawResults, User user, string gridName)
        {
            foreach (T x in rawResults)
            {
                yield return new GridRow { id = x.EntityId, cell = GridBuilder.ToGridRow(x, user, _modifications, gridName) };
            }
        }

        public string GetDefaultSortColumnName()
        {
            var column = GridBuilder.columns.FirstOrDefault(
                x => x.Properties.Any(y => y.Key == GridColumnProperties.sortColumn.ToString()));
            return column == null ? string.Empty : column.Properties[GridColumnProperties.header.ToString()];
        }

        public void AddColumnModifications(Action<IGridColumn, T> modification)
        {
            _modifications.Add(modification);
        }


        public GridDefinition GetGridDefinition(string url)
        {
            var userId = _sessionContext.GetUserId();
            var user = _repository.Find<User>(userId);
            return new GridDefinition
            {
                Url = url,
                Columns = BuildGrid().GetGridColumns(user)
            };
        }

        public GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<T> items, string gridName = null)
        {
            var userId = _sessionContext.GetUserId();
            var user = _repository.Find<User>(userId);
            var pager = new Pager<T>();
            var pageAndSort = pager.PageAndSort(items, pageSortFilter);
            var model = new GridItemsViewModel
            {
                items = BuildGrid().GetGridRows(pageAndSort.Items, user,gridName),
                page = pageAndSort.Page,
                records = pageAndSort.TotalRows,
                total = pageAndSort.TotalPages
            };
            return model;
        }

        protected virtual Grid<T> BuildGrid()
        {
            return this;
        }
    }

    public interface IGridEnabledClass
    {
        long EntityId { get; set; }
    }

    public class GridRow
    {
        public long id { get; set; }
        public string[] cell { get; set; }
    }
}