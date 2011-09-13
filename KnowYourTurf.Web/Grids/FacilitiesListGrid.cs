using System;
using System.Linq;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public interface IFacilitiesListGrid
    {
        void AddColumnModifications(Action<IGridColumn, Facilities> modification);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<Facilities> items, string gridName = "gridContainer");
    }

    public class FacilitiesListGrid : Grid<Facilities>, IFacilitiesListGrid
    {
        public FacilitiesListGrid(IGridBuilder<Facilities> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
        {
        }

        protected override Grid<Facilities> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
               .ForAction<FacilitiesController>(x => x.Delete(null))
               .ToPerformAction(ColumnAction.Delete)
                .ImageName("delete.png")
               .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<FacilitiesController>(x => x.Facilities(null))
                .ToPerformAction(ColumnAction.Edit)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.FullName)
                .ForAction<FacilitiesDashboardController>(x => x.ViewFacilities(null))
                .ToPerformAction(ColumnAction.Redirect)
                .IsSortable(false)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.FacilitiesId);
            GridBuilder.DisplayFor(x => x.PhoneMobile);
            GridBuilder.DisplayFor(x => x.Email);
            return this;
        }
    }

}