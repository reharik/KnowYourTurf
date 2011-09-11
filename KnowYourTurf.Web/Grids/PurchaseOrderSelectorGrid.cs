using System;
using System.Linq;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public interface IPurchaseOrderSelectorGrid
    {
        void AddColumnModifications(Action<IGridColumn, BaseProduct> modification);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<BaseProduct> items, string gridName = "gridContainer");
    }

    public class PurchaseOrderSelectorGrid : Grid<BaseProduct>, IPurchaseOrderSelectorGrid
    {
        public PurchaseOrderSelectorGrid(IGridBuilder<BaseProduct> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
        {
        }

        protected override Grid<BaseProduct> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
                .ForAction<PurchaseOrderController>(x=>x.Save(null))
                .ToPerformAction(ColumnAction.Other)
                .ImageName("delete.png")
                .ToolTip(WebLocalizationKeys.ADD_ITEM_TO_PO);
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction(x => x.InstantiatingType, "Display")
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.Description);
            GridBuilder.DisplayFor(x => x.InstantiatingType);
            return this;
        }
    }

}