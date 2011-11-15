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
    public interface IPurchaseOrderLineItemGrid
    {
        void AddColumnModifications(Action<IGridColumn, PurchaseOrderLineItem> modifications);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<PurchaseOrderLineItem> items, string gridName = "");
    }

    public class PurchaseOrderLineItemGrid : Grid<PurchaseOrderLineItem>, IPurchaseOrderLineItemGrid
    {
        public PurchaseOrderLineItemGrid(IGridBuilder<PurchaseOrderLineItem> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
        {
        }

        protected override Grid<PurchaseOrderLineItem> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
               .ForAction<PurchaseOrderLineItemController>(x => x.Delete(null))
               .ToPerformAction(ColumnAction.Delete)
               .ImageName("delete.png")
               .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<PurchaseOrderLineItemController>(x => x.AddEdit(null))
                .ToPerformAction(ColumnAction.Edit)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.Product.Name)
                .ForAction<PurchaseOrderLineItemController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.QuantityOrdered);
            GridBuilder.DisplayFor(x => x.Price).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.SubTotal).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.Tax).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.TotalReceived);
            return this;
        }
    }

    public interface IReceivePurchaseOrderLineItemGrid
    {
        void AddColumnModifications(Action<IGridColumn,PurchaseOrderLineItem> modifications);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<PurchaseOrderLineItem> items, string gridName = "");
    }

    public class ReceivePurchaseOrderLineItemGrid : Grid<PurchaseOrderLineItem>, IReceivePurchaseOrderLineItemGrid 
    {
        public ReceivePurchaseOrderLineItemGrid(IGridBuilder<PurchaseOrderLineItem> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
        {
        }

        protected override Grid<PurchaseOrderLineItem> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
               .ForAction<PurchaseOrderLineItemController>(x => x.Delete(null))
               .ToPerformAction(ColumnAction.Delete)
               .ImageName("delete.png")
               .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<PurchaseOrderLineItemController>(x => x.ReceivePurchaseOrderLineItem(null))
                .ToPerformAction(ColumnAction.Edit)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.Product.Name)
                .ForAction<PurchaseOrderLineItemController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.QuantityOrdered);
            GridBuilder.DisplayFor(x => x.Price).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.SubTotal).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.Tax).FormatValue(GridColumnFormatter.Currency);
            GridBuilder.DisplayFor(x => x.TotalReceived);
            return this;
        }
    }
}