using CC.Core.DomainTools;
using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Grids
{
    public class CalculatorListGrid : Grid<Calculator>, IEntityListGrid<Calculator>
    {
        public CalculatorListGrid(IGridBuilder<Calculator> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<Calculator> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x=>x.Name)
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.DateCreated);
            return this;
        }
    }
}