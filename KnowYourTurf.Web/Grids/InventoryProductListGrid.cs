using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class InventoryProductListGrid : Grid<InventoryProduct>, IEntityListGrid<InventoryProduct>
    {

        public InventoryProductListGrid(IGridBuilder<InventoryProduct> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<InventoryProduct> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Product.Name)
                .ToPerformAction(ColumnAction.DisplayItem)
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
}