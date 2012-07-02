using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class PendingTaskGrid : Grid<Task>, IEntityListGrid<Task>
    {
        public PendingTaskGrid(IGridBuilder<Task> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<Task> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.TaskType.Name)
                .ForAction<TaskController>(x => x.AddUpdate(null))
                .ToPerformAction(ColumnAction.AddUpdateItem).WithId("pending")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.ScheduledDate);
            GridBuilder.DisplayFor(x => x.ScheduledStartTime);
            return this;
        }
    }
}