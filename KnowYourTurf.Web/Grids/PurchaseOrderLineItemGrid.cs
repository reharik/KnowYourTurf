using System;
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
    public class PurchaseOrderLineItemGrid : Grid<PurchaseOrderLineItem>, IEntityListGrid<PurchaseOrderLineItem>
    {
        public PurchaseOrderLineItemGrid(IGridBuilder<PurchaseOrderLineItem> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
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
        public ReceivePurchaseOrderLineItemGrid(IGridBuilder<PurchaseOrderLineItem> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<PurchaseOrderLineItem> BuildGrid()
        {

            GridBuilder.LinkColumnFor(x => x.Product.Name)
                .ForAction<PurchaseOrderLineItemController>(x => x.ReceivePurchaseOrderLineItem(null))
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.QuantityOrdered);
            GridBuilder.DisplayFor(x => x.Price).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.SubTotal).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.Tax).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.TotalReceived);
            return this;
        }
    }
}