using CC.Core.DomainTools;
using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Grids
{
    public class PurchaseOrderLineItemGrid : Grid<PurchaseOrderLineItem>, IEntityListGrid<PurchaseOrderLineItem>
    {
        public PurchaseOrderLineItemGrid(IGridBuilder<PurchaseOrderLineItem> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<PurchaseOrderLineItem> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
                .ToPerformAction(ColumnAction.Delete).WithId("poliGrid")
                .ImageName("delete_sm.png")
                .ToolTip(WebLocalizationKeys.DELETE_ITEM).Width(22);
            GridBuilder.LinkColumnFor(x => x.Product.Name)
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .WithId("poliGrid")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.QuantityOrdered);
            GridBuilder.DisplayFor(x => x.Price).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.SubTotal).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.Tax).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.TotalReceived);
            return this;
        }
    }

    public class ReceivePurchaseOrderLineItemGrid : Grid<PurchaseOrderLineItem>, IEntityListGrid<PurchaseOrderLineItem>
    {
        public ReceivePurchaseOrderLineItemGrid(IGridBuilder<PurchaseOrderLineItem> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<PurchaseOrderLineItem> BuildGrid()
        {

            GridBuilder.LinkColumnFor(x => x.Product.Name)
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .WithId("commitPoliGrid")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.QuantityOrdered);
            GridBuilder.DisplayFor(x => x.Price).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.SubTotal).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.Tax).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.TotalReceived);
            return this;
        }
    }

    public class CompetedPurchaseOrderLineItemGrid : Grid<PurchaseOrderLineItem>, IEntityListGrid<PurchaseOrderLineItem>
    {
        public CompetedPurchaseOrderLineItemGrid(IGridBuilder<PurchaseOrderLineItem> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<PurchaseOrderLineItem> BuildGrid()
        {
            GridBuilder.DisplayFor(x => x.Product.Name);
            GridBuilder.DisplayFor(x => x.QuantityOrdered);
            GridBuilder.DisplayFor(x => x.Price).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.SubTotal).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.Tax).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.TotalReceived);
            return this;
        }
    }

}