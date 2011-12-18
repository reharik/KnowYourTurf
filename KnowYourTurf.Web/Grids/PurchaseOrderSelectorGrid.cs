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
                .ForAction<PurchaseOrderController>(x=>x.Save(null))
                .ToPerformAction(ColumnAction.Other)
                .ImageName("KYTadd.png")
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