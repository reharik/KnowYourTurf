using System;
using System.Linq;
using HtmlTags;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Core.Services
{
    public interface IEntityListGrid<ENTITY> where ENTITY : DomainEntity
    {
        void AddColumnModifications(Action<IGridColumn, ENTITY> modification);
        GridDefinition GetGridDefinition(string url);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<ENTITY> items);
    }
}