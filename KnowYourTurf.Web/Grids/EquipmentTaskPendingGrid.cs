using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Grids
{
    public class EquipmentTaskPendingGrid : Grid<EquipmentTask>, IEntityListGrid<EquipmentTask>
    {
        public EquipmentTaskPendingGrid(IGridBuilder<EquipmentTask> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<EquipmentTask> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.TaskType.Name)
                .ToPerformAction(ColumnAction.AddUpdateItem).WithId("pendingEquipmentTaskList")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.ScheduledDate);
            GridBuilder.DisplayFor(x => x.StartTime);
            GridBuilder.SetSearchField(x => x.TaskType.Name);
            GridBuilder.SetDefaultSortColumn(x => x.TaskType.Name);
            return this;
        }
    }
}