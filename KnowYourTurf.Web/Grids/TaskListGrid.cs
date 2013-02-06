using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Grids
{
    public class TaskListGrid : Grid<Task>, IEntityListGrid<Task>
    {

        public TaskListGrid(IGridBuilder<Task> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<Task> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.TaskType.Name)
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x=>x.ScheduledDate);
            GridBuilder.DisplayFor(x=>x.StartTime);
            GridBuilder.DisplayFor(x => x.Complete);
            GridBuilder.SetSearchField(x => x.TaskType.Name);
            GridBuilder.SetDefaultSortColumn(x => x.TaskType.Name);
            return this;
        }
    }
}