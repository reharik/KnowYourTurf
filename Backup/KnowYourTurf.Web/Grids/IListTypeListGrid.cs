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

    public class EquipmentTaskTypeListGrid : Grid<EquipmentTaskType>, IEntityListGrid<EquipmentTaskType>
    {
        public EquipmentTaskTypeListGrid(IGridBuilder<EquipmentTaskType> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<EquipmentTaskType> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .WithId("equipmenttasktypelist")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.Status);
            return this;
        }
    }

    public class EquipmentTypeListGrid : Grid<EquipmentType>, IEntityListGrid<EquipmentType>
    {
        public EquipmentTypeListGrid(IGridBuilder<EquipmentType> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<EquipmentType> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .WithId("equipmentTypelist")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.Status);
            return this;
        }
    }

    public class PartListGrid : Grid<Part>, IEntityListGrid<Part>
    {
        public PartListGrid(IGridBuilder<Part> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<Part> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .WithId("partlist")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.Status);
            return this;
        }
    }

}
