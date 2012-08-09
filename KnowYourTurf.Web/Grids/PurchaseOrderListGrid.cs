using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Core.Domain
{
    public class PurchaseOrderListGrid : Grid<PurchaseOrder>, IEntityListGrid<PurchaseOrder>
    {

        public PurchaseOrderListGrid(IGridBuilder<PurchaseOrder> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<PurchaseOrder> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.DateCreated)
                .ForAction<PurchaseOrderController>(x => x.AddUpdate(null))
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.EntityId).DisplayHeader(WebLocalizationKeys.PO_Number);
            GridBuilder.DisplayFor(x => x.Vendor.Company);
            GridBuilder.DisplayFor(x => x.SubTotal).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.Tax).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.Fees).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.Total).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.ImageButtonColumn()
                .ForAction<PurchaseOrderCommitController>(x => x.PurchaseOrderCommit(null))
                .ToPerformAction(ColumnAction.Redirect)
                .ImageName("KYTcopy.png");
            return this;
        }

    }
}