using System;
using System.Collections.Generic;
using System.Linq;
using HtmlTags;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public interface IFertilizerListGrid
    {
        void AddColumnModifications(Action<IGridColumn, Fertilizer> modifications);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<Fertilizer> items, string gridName = "");
    }

    public class FertilizerListGrid : Grid<Fertilizer>, IFertilizerListGrid
    {

        public FertilizerListGrid(IGridBuilder<Fertilizer> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
        {
        }

        protected override Grid<Fertilizer> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
               .ForAction<FertilizerController>(x => x.Delete(null))
               .ToPerformAction(ColumnAction.Delete)
               .ImageName("delete.png")
               .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<FertilizerController>(x => x.AddEdit(null))
                .ToPerformAction(ColumnAction.Edit)
               .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<FertilizerController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.N);
            GridBuilder.DisplayFor(x => x.P);
            GridBuilder.DisplayFor(x => x.K);
            return this;
        }
    }

    public interface IPOFertilizerListGrid
    {
        void AddColumnModifications(Action<IGridColumn,Fertilizer> modifications);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<Fertilizer> items, string gridName = "");
    }

    public class POFertilizerListGrid : Grid<Fertilizer>, IPOFertilizerListGrid
    {

        public POFertilizerListGrid(IGridBuilder<Fertilizer> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
        {
        }

        protected override Grid<Fertilizer> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<FertilizerController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.N);
            GridBuilder.DisplayFor(x => x.P);
            GridBuilder.DisplayFor(x => x.K);
            return this;
        }
    }
}