using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Grids
{
    public class EquipmentTaskListGrid : Grid<EquipmentTask>, IEntityListGrid<EquipmentTask>
    {

        public EquipmentTaskListGrid(IGridBuilder<EquipmentTask> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<EquipmentTask> BuildGrid()
        {

            GridBuilder.LinkColumnFor(x => x.TaskType.Name)
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x=>x.ScheduledDate);
            GridBuilder.DisplayFor(x => x.Complete);
            return this;
        }
    }
}