using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class MaterialListGrid : Grid<Material>, IEntityListGrid<Material>
    {
        public MaterialListGrid(IGridBuilder<Material> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<Material> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<MaterialController>(x => x.AddUpdate(null))
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            return this;
        }
    }

    public class POMaterialListGrid : Grid<Material>, IEntityListGrid<Material>
    {
        public POMaterialListGrid(IGridBuilder<Material> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<Material> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ToPerformAction(ColumnAction.DisplayItem)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            return this;
        }
    }

}