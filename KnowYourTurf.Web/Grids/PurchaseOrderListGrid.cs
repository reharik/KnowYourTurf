using System;
using System.Collections.Generic;
using System.Linq;
using HtmlTags;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web;
using KnowYourTurf.Web.Controllers;
using KnowYourTurf.Web.Grids;

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
            GridBuilder.ImageButtonColumn()
               .ForAction<PurchaseOrderController>(x => x.Delete(null))
               .ToPerformAction(ColumnAction.Delete)
               .ImageName("delete.png")
               .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<PurchaseOrderController>(x => x.AddEdit(null))
                .ToPerformAction(ColumnAction.Redirect)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.DateCreated)
                .ForAction<PurchaseOrderController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.Completed);
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