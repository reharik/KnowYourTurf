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
    public interface IListTypeListGrid<LISTTYPE> where LISTTYPE:ListType
    {
        void AddColumnModifications(Action<IGridColumn, LISTTYPE> modifications);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<LISTTYPE> items, string gridName = "gridContainer");
    }

    public interface IEventTypeListGrid
    {
        void AddColumnModifications(Action<IGridColumn, EventType> modification);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<EventType> items, string gridName = "gridContainer");
    }

    public class EventTypeListGrid : Grid<EventType>, IListTypeListGrid<EventType>, IEventTypeListGrid
    {
        public EventTypeListGrid(IGridBuilder<EventType> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
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

    public interface ITaskTypeListGrid
    {
        void AddColumnModifications(Action<IGridColumn, TaskType> modification);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<TaskType> items, string gridName = "gridContainer");
    }

    public class TaskTypeListGrid : Grid<TaskType>, IListTypeListGrid<TaskType>, ITaskTypeListGrid
    {
        public TaskTypeListGrid(IGridBuilder<TaskType> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
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

    public interface IPhotoCategoryListGrid
    {
        void AddColumnModifications(Action<IGridColumn, PhotoCategory> modification);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<PhotoCategory> items, string gridName = "gridContainer");
    }

    public class PhotoCategoryListGrid : Grid<PhotoCategory>, IListTypeListGrid<PhotoCategory>, IPhotoCategoryListGrid
    {
        public PhotoCategoryListGrid(IGridBuilder<PhotoCategory> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
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

    public interface IDocumentCategoryListGrid
    {
        void AddColumnModifications(Action<IGridColumn, DocumentCategory> modification);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<DocumentCategory> items, string gridName = "gridContainer");
    }

    public class DocumentCategoryListGrid : Grid<DocumentCategory>, IListTypeListGrid<DocumentCategory>, IDocumentCategoryListGrid
    {
        public DocumentCategoryListGrid(IGridBuilder<DocumentCategory> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
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
