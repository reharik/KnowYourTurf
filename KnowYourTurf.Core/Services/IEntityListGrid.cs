using System;
using System.Linq;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html.Grid;
using CC.Security;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Core.Services
{
    public interface IEntityListGrid<ENTITY> where ENTITY : DomainEntity
    {
        void AddColumnModifications(Action<IGridColumn, ENTITY> modification);
        GridDefinition GetGridDefinition(string url, IUser user);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<ENTITY> items, IUser user);
    }
}