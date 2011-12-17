using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Core.Html.Grid
{
    public interface IPager<ENTITY>
    {
        PageDetail PageAndSort(IEnumerable<ENTITY> query, PageSortFilter _pageSortFilter);
    }

    //public static class myType
    //{
    //    public static bool startsWith<ENTITY>(string value)
    //    {
    //        value.StartsWith()
    //    }
    //}
    public class Pager<ENTITY> : IPager<ENTITY> where ENTITY : IGridEnabledClass
    {
        public PageDetail PageAndSort(IEnumerable<ENTITY> query, PageSortFilter _pageSortFilter)
        {

            var count = query != null ? query.Count() : 0;
            var theQuery = query.AsQueryable();
            if (_pageSortFilter.SortColumn.IsNotEmpty())
            {
                query = (_pageSortFilter.SortAscending)
                            ? theQuery.OrderBy(_pageSortFilter.SortColumn)
                            : theQuery.OrderByDescending(_pageSortFilter.SortColumn);
            }
            return new PageDetail
            {
                Items = query.Skip(_pageSortFilter.Skip).Take(_pageSortFilter.Take),
                RowsPersPage = _pageSortFilter.Take,
                TotalRows = count,
                Page = _pageSortFilter.Page
            };
        }
    }

    public interface IPageDetail
    {
        int TotalPages { get; }
        int RowsPersPage { get; set; }
        IEnumerable Items { get; set; }
        int TotalRows { get; set; }
        int Page { get; set; }
    }

    public class PageDetail : IPageDetail
    {
        public int TotalPages { get { return (int)Math.Ceiling((double)TotalRows / RowsPersPage); } }
        public int RowsPersPage { get; set; }
        public IEnumerable Items { get; set; }
        public int TotalRows { get; set; }
        public int Page { get; set; }
        public string param1 { get; set; }
        public string param2 { get; set; }
    }
}