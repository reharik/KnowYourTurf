using System;
using System.Collections;
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
    public interface IChemicalListGrid
    {
        void AddColumnModifications(Action<IGridColumn, Chemical> modifications);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<Chemical> items, string gridName = "gridContainer");
    }

    public class ChemicalListGrid : Grid<Chemical>, IChemicalListGrid
    {
        public ChemicalListGrid(IGridBuilder<Chemical> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
        {
        }

        protected override Grid<Chemical> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
                .ForAction<ChemicalController>(x => x.Delete(null))
                .ToPerformAction(ColumnAction.Delete)
               .ImageName("delete.png")
                .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<ChemicalController>(x => x.AddEdit(null))
                .ToPerformAction(ColumnAction.Edit)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<ChemicalController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.ActiveIngredient);
            GridBuilder.DisplayFor(x => x.ActiveIngredientPercent);
            GridBuilder.DisplayFor(x => x.EPAEstNumber);
            GridBuilder.DisplayFor(x => x.EPARegNumber);
            return this;
        }
    }

    public interface IPOChemicalListGrid
    {
        void AddColumnModifications(Action<IGridColumn,Chemical> modifications);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<Chemical> items, string gridName = "gridContainer");
    }

    public class POChemicalListGrid : Grid<Chemical>, IPOChemicalListGrid
    {
        public POChemicalListGrid(IGridBuilder<Chemical> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
        {
        }

        protected override Grid<Chemical> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<ChemicalController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.Manufacturer);
            GridBuilder.DisplayFor(x => x.ActiveIngredient);
            GridBuilder.DisplayFor(x => x.ActiveIngredientPercent);
            GridBuilder.DisplayFor(x => x.EPAEstNumber);
            GridBuilder.DisplayFor(x => x.EPARegNumber);
            return this;
        }
    }
}