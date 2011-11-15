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
    public interface IInventoryProductListGrid
    {
        void AddColumnModifications(Action<IGridColumn, InventoryProduct> modifications);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<InventoryProduct> items, string gridName = "");
    }

    public class InventoryProductListGrid : Grid<InventoryProduct>, IInventoryProductListGrid
    {

        public InventoryProductListGrid(IGridBuilder<InventoryProduct> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
        {
        }

        protected override Grid<InventoryProduct> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Product.Name)
                .ForAction<InventoryListController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.Quantity);
            GridBuilder.DisplayFor(x => x.SizeOfUnit);
            GridBuilder.DisplayFor(x => x.UnitType);
            GridBuilder.DisplayFor(x => x.DatePurchased);
            GridBuilder.DisplayFor(x => x.Cost).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.LastVendor.Company);
            return this;
        }
    }

    //public class InventoryChemicalListGrid : Grid<InventoryChemical>
    //{

    //    public InventoryChemicalListGrid(IGridBuilder<InventoryChemical> grid)
    //        : base(grid)
    //    {
    //    }

    //    public override IGrid<InventoryChemical> BuildGrid()
    //    {
    //        _grid.DisplayWithInPopup<InventoryListController>(f => f.Chemical.Name, c => c.DisplayChemical(null), "kyt.popupCrud.controller.displayItem")
    //            .Display(f => f.Quantity)
    //            .Display(f => f.Chemical.UnitType)
    //            .Display(f => f.Description)
    //            .Display(f => f.DatePurchased).Formatter(GridColumnFormatter.Date)
    //            .Display(f => f.Cost).Formatter(GridColumnFormatter.Currency)
    //            .Display(f => f.Vendor.Company);
    //        return this;
    //    }
    //}
}