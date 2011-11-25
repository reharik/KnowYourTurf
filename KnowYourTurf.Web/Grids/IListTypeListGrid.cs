using System;
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
    public class EventTypeListGrid : Grid<EventType>, IEntityListGrid<EventType>
    {
        public EventTypeListGrid(IGridBuilder<EventType> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<EventType> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
               .ForAction<EventTypeController>(x => x.AddUpdate(null),"eventTypeGrid")
               .ToPerformAction(ColumnAction.Edit)
               .ImageName("KYTedit.png")
               .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.Name, "eventTypeGrid")
                .ForAction<EventTypeController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.Status);
            return this;
        }
    }

    public class TaskTypeListGrid : Grid<TaskType>, IEntityListGrid<TaskType>
    {
        public TaskTypeListGrid(IGridBuilder<TaskType> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<TaskType> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
               .ForAction<TaskTypeController>(x => x.AddUpdate(null),"taskTypeGrid")
               .ToPerformAction(ColumnAction.Edit)
               .ImageName("KYTedit.png")
               .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.Name, "taskTypeGrid")
                .ForAction<TaskTypeController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.Status);
            return this;
        }
    }

    public class PhotoCategoryListGrid : Grid<PhotoCategory>, IEntityListGrid<PhotoCategory>
    {
        public PhotoCategoryListGrid(IGridBuilder<PhotoCategory> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<PhotoCategory> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
                .ForAction<PhotoCategoryController>(x => x.AddUpdate(null),"photoCategoryGrid")
               .ToPerformAction(ColumnAction.Edit)
               .ImageName("KYTedit.png")
               .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.Name, "photoCategoryGrid")
                .ForAction<PhotoCategoryController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.Status);
            return this;
        }
    }

    public class DocumentCategoryListGrid : Grid<DocumentCategory>, IEntityListGrid<DocumentCategory>
    {
        public DocumentCategoryListGrid(IGridBuilder<DocumentCategory> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<DocumentCategory> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
               .ForAction<DocumentCategoryController>(x => x.AddUpdate(null),"documentCategoryGrid")
               .ToPerformAction(ColumnAction.Edit)
               .ImageName("KYTedit.png")
               .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.Name, "documentCategoryGrid")
                .ForAction<DocumentCategoryController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.Status);
            return this;
        }
    }

}
