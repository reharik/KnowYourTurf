using CC.Core.DomainTools;
using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class FertilizerListGrid : Grid<Fertilizer>, IEntityListGrid<Fertilizer>
    {

        public FertilizerListGrid(IGridBuilder<Fertilizer> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<Fertilizer> BuildGrid()
        {

            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<FertilizerController>(x => x.AddUpdate(null))
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.N);
            GridBuilder.DisplayFor(x => x.P);
            GridBuilder.DisplayFor(x => x.K);
            return this;
        }
    }

    public class POFertilizerListGrid : Grid<Fertilizer>, IEntityListGrid<Fertilizer>
    {

        public POFertilizerListGrid(IGridBuilder<Fertilizer> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<Fertilizer> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name, "KYT")
                .ToPerformAction(ColumnAction.DisplayItem)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.N);
            GridBuilder.DisplayFor(x => x.P);
            GridBuilder.DisplayFor(x => x.K);
            return this;
        }
    }
}