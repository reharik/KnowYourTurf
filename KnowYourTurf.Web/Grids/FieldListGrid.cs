using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class FieldListGrid : Grid<Field>, IEntityListGrid<Field>
    {

        public FieldListGrid(IGridBuilder<Field> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<Field> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name, "KYT")
                .ForAction<FieldDashboardController>(x => x.ViewField(null))
                .ToPerformAction(ColumnAction.Redirect)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.Description);
            GridBuilder.DisplayFor(x => x.Size);
            GridBuilder.DisplayFor(x => x.Abbreviation);
            return this;
        }
    }
}