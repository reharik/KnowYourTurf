using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Grids
{
    public class PurchaseOrderSelectorGrid : Grid<BaseProduct>, IEntityListGrid<BaseProduct>
    {
        public PurchaseOrderSelectorGrid(IGridBuilder<BaseProduct> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<BaseProduct> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
                .ToPerformAction(ColumnAction.Other).WithId("productGrid")
                .ImageName("KYTadd.png")
                .ToolTip(WebLocalizationKeys.ADD_ITEM_TO_PO);
            GridBuilder.DisplayFor(x => x.Name);
            GridBuilder.DisplayFor(x => x.Description);
            GridBuilder.GroupingColumnFor(x => x.InstantiatingType);
            return this;
        }
    }

}