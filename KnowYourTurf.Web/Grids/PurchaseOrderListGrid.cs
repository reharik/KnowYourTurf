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

namespace KnowYourTurf.Core.Domain
{
    public interface IPurchaseOrderListGrid
    {
        void AddColumnModifications(Action<IGridColumn, PurchaseOrder> modifications);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<PurchaseOrder> items, string gridName = "gridContainer");
    }

    public class PurchaseOrderListGrid : Grid<PurchaseOrder>, IPurchaseOrderListGrid
    {

        public PurchaseOrderListGrid(IGridBuilder<PurchaseOrder> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
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
                .ImageName("page_edit.png")
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
                .ImageName("delete.png");
            return this;
        }

    }
}