using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    //public class InventoryFertilizerListGrid : Grid<InventoryFertilizer>
    //{

    //    public InventoryFertilizerListGrid(IGridBuilder<InventoryFertilizer> grid)
    //        : base(grid)
    //    {
    //    }

    //    public override IGrid<InventoryFertilizer> BuildGrid()
    //    {
    //        _grid.DisplayWithInPopup<InventoryListController>(f => f.Fertilizer.Name, c => c.DisplayFertilizer(null), "kyt.popupCrud.controller.displayItem")
    //            .Display(f => f.Quantity)
    //            .Display(f => f.Fertilizer.UnitType)
    //            .Display(f => f.Description)
    //            .Display(f => f.DatePurchased).Formatter(GridColumnFormatter.Date)
    //            .Display(f => f.Cost).Formatter(GridColumnFormatter.Currency)
    //            .Display(f => f.Vendor.Company);
    //        return this;
    //    }
    //}
}