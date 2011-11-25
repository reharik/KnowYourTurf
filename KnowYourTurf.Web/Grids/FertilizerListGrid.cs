using System;
using System.Collections.Generic;
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
    public class FertilizerListGrid : Grid<Fertilizer>, IEntityListGrid<Fertilizer>
    {

        public FertilizerListGrid(IGridBuilder<Fertilizer> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<Fertilizer> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
               .ForAction<FertilizerController>(x => x.Delete(null))
               .ToPerformAction(ColumnAction.Delete)
               .ImageName("delete.png")
               .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<FertilizerController>(x => x.AddEdit(null))
                .ToPerformAction(ColumnAction.Edit)
               .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<FertilizerController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.N);
            GridBuilder.DisplayFor(x => x.P);
            GridBuilder.DisplayFor(x => x.K);
            return this;
        }
    }

    public class POFertilizerListGrid : Grid<Fertilizer>, IEntityListGrid<Fertilizer>
    {

        public POFertilizerListGrid(IGridBuilder<Fertilizer> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<Fertilizer> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<FertilizerController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.N);
            GridBuilder.DisplayFor(x => x.P);
            GridBuilder.DisplayFor(x => x.K);
            return this;
        }
    }
}