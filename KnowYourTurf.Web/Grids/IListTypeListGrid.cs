using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Grids
{
    public class EventTypeListGrid : Grid<EventType>, IEntityListGrid<EventType>
    {
        public EventTypeListGrid(IGridBuilder<EventType> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<EventType> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name, "eventTypeGrid")
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .WithId("eventtypelist")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.Status);
            return this;
        }
    }

    public class TaskTypeListGrid : Grid<TaskType>, IEntityListGrid<TaskType>
    {
        public TaskTypeListGrid(IGridBuilder<TaskType> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<TaskType> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .WithId("tasktypelist")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.Status);
            return this;
        }
    }

    public class PhotoCategoryListGrid : Grid<PhotoCategory>, IEntityListGrid<PhotoCategory>
    {
        public PhotoCategoryListGrid(IGridBuilder<PhotoCategory> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<PhotoCategory> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .WithId("photocategorylist")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.Status);
            return this;
        }
    }

    public class DocumentCategoryListGrid : Grid<DocumentCategory>, IEntityListGrid<DocumentCategory>
    {
        public DocumentCategoryListGrid(IGridBuilder<DocumentCategory> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<DocumentCategory> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .WithId("documentcategorylist")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.Status);
            return this;
        }
    }

}
