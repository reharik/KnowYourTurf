using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KnowYourTurf.Core.CoreViewModelAndDTOs;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using HtmlTags;

namespace KnowYourTurf.Core.Html.Grid
{
    public interface IGrid<T> where T : IGridEnabledClass
    {
        void AddColumnModifications(Action<HtmlTag, T> modification);
        GridDefinition GetGridDefinition(string url);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<T> items);
    }

    public abstract class Grid<T> : IGrid<T> where T : IGridEnabledClass
    {
        protected readonly IGridBuilder<T> GridBuilder;
        private readonly ISessionContext _sessionContext;
        private readonly IRepository _repository;
        private IList<Action<HtmlTag, T>> _modifications;

        protected Grid(IGridBuilder<T> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
        {
            GridBuilder = gridBuilder;
            _sessionContext = sessionContext;
            _repository = repository;
            _modifications = new List<Action<HtmlTag, T>>();
        }

        private IList<IDictionary<string, string>> GetGridColumns(User user)
        {
            return GridBuilder.ToGridColumns(user);
        }

        private IEnumerable GetGridRows(IEnumerable rawResults, User user)
        {
            foreach (T x in rawResults)
            {
                yield return new GridRow { id = x.EntityId, cell = GridBuilder.ToGridRow(x, user, _modifications) };
            }
        }

        public string GetDefaultSortColumnName()
        {
            var column = GridBuilder.columns.FirstOrDefault(
                x => x.Properties.Any(y=>y.Key == GridColumnProperties.sortColumn.ToString()));
            return column==null?string.Empty:column.Properties[GridColumnProperties.header.ToString()];
        }

        public void AddColumnModifications(Action<HtmlTag, T> modification)
        {
            _modifications.Add(modification);
        }

        public GridDefinition GetGridDefinition(string url)
        {
            var userId = _sessionContext.GetUserEntityId();
            var user = _repository.Find<User>(userId);
            return new GridDefinition
            {
                Url = url,
                Columns = BuildGrid().GetGridColumns(user)
            };
        }

        public GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<T> items)
        {
            var userId = _sessionContext.GetUserEntityId();
            var user = _repository.Find<User>(userId);
            var pager = new Pager<T>();
            var pageAndSort = pager.PageAndSort(items, pageSortFilter);
            var model = new GridItemsViewModel
            {
                items = BuildGrid().GetGridRows(pageAndSort.Items, user),
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
        int EntityId { get; set; }
    }

    public class GridRow
    {
        public long id { get; set; }
        public string[] cell { get; set; }
    }
}