using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Grids
{
    public class PendingTaskGrid : Grid<Task>, IEntityListGrid<Task>
    {
        public PendingTaskGrid(IGridBuilder<Task> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<Task> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.TaskType.Name)
                .ToPerformAction(ColumnAction.AddUpdateItem).WithId("pendingTaskList")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.ScheduledDate);
            GridBuilder.DisplayFor(x => x.StartTime);
            return this;
        }
    }
}