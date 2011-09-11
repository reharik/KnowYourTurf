using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    //public class InventoryMaterialListGrid : Grid<InventoryMaterial>
    //{

    //    public InventoryMaterialListGrid(IGridBuilder<InventoryMaterial> grid)
    //        : base(grid)
    //    {
    //    }

    //    public override IGrid<InventoryMaterial> BuildGrid()
    //    {
    //        _grid.DisplayWithInPopup<InventoryListController>(f => f.Material.Name, c => c.DisplayMaterial(null), "kyt.popupCrud.controller.displayItem")
    //            .Display(f => f.Quantity)
    //            .Display(f => f.Material.UnitType)
    //            .Display(f => f.Description)
    //            .Display(f => f.DatePurchased).Formatter(GridColumnFormatter.Date)
    //            .Display(f => f.Cost).Formatter(GridColumnFormatter.Currency)
    //            .Display(f => f.Vendor.Company);
    //        return this;
    //    }
    //}
}