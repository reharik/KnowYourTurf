using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Grids
{
    public class EquipmentTaskCompletedGrid : Grid<EquipmentTask>, IEntityListGrid<EquipmentTask>
    {
        public EquipmentTaskCompletedGrid(IGridBuilder<EquipmentTask> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<EquipmentTask> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.TaskType.Name)
                .ToPerformAction(ColumnAction.DisplayItem).WithId("completedEquipmentTaskList")
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.ScheduledDate);
            GridBuilder.DisplayFor(x => x.StartTime);
            GridBuilder.SetSearchField(x => x.TaskType.Name);
            GridBuilder.SetDefaultSortColumn(x => x.TaskType.Name);
            return this;
        }
    }
}