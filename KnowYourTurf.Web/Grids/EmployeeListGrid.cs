using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Grids
{
    public class EmployeeListGrid : Grid<User>, IEntityListGrid<User>
    {

        public EmployeeListGrid(IGridBuilder<User> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<User> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.FullName)
                .ToPerformAction(ColumnAction.Redirect)
                .IsSortable(false)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.EmployeeId);
            GridBuilder.DisplayFor(x => x.PhoneMobile);
            GridBuilder.DisplayFor(x => x.Email).FormatValue(GridColumnFormatter.EMail);
            return this;
        }
    }

}