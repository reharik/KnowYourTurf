using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Grids
{
    public class CompletedTaskGrid : Grid<Task>, IEntityListGrid<Task>
    {
        public CompletedTaskGrid(IGridBuilder<Task> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<Task> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.TaskType.Name, "KYT")
                .ToPerformAction(ColumnAction.DisplayItem).WithId("completedTaskList")
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.ScheduledDate);
            GridBuilder.DisplayFor(x => x.ScheduledStartTime);
            return this;
        }
    }
}