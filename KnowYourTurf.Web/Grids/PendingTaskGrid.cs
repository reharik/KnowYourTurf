using System;
using System.Collections.Generic;
using System.Linq;
using HtmlTags;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public interface IPendingTaskGrid
    {
        void AddColumnModifications(Action<IGridColumn, Task> modifications);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<Task> items, string gridName = "gridContainer");
    }

    public class PendingTaskGrid : Grid<Task>, IPendingTaskGrid
    {
        public PendingTaskGrid(IGridBuilder<Task> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
        {
        }

        protected override Grid<Task> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
               .ForAction<TaskController>(x => x.Delete(null), "pendingTaskGrid")
               .ToPerformAction(ColumnAction.Delete)
                .ImageName("delete.png")
               .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<TaskController>(x => x.AddEdit(null), "pendingTaskGrid")
                .ToPerformAction(ColumnAction.Edit)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.TaskType.Name, "pendingTaskGrid")
                .ForAction<TaskController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.ScheduledDate);
            GridBuilder.DisplayFor(x => x.ScheduledStartTime);
            return this;
        }
    }
}