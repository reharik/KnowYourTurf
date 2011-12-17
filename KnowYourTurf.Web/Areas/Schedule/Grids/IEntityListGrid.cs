using System;
using System.Linq;
using HtmlTags;
using KnowYourTurf.Core.CoreViewModelAndDTOs;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Web.Areas.Schedule.Grids
{
    public interface IEntityListGrid<ENTITY> where ENTITY : Entity
    {
        void AddColumnModifications(Action<HtmlTag, ENTITY> modification);
        GridDefinition GetGridDefinition(string url);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<ENTITY> items);
    }
}