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
    public interface IPhotoListGrid
    {
        void AddColumnModifications(Action<IGridColumn,Photo> modification);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<Photo> items, string gridName = "gridContainer");
    }

    public class PhotoListGrid : Grid<Photo>, IPhotoListGrid
    {
        public PhotoListGrid(IGridBuilder<Photo> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
        {
        }

        protected override Grid<Photo> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
               .ForAction<PhotoController>(x => x.Delete(null),"photoGrid")
               .ToPerformAction(ColumnAction.Delete)
               .ImageName("delete.png")
               .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<PhotoController>(x => x.AddUpdate(null), "photoGrid")
                .ToPerformAction(ColumnAction.Edit)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.Name, "photoGrid")
                .ForAction<PhotoController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.PhotoCategory.Name).DisplayHeader(WebLocalizationKeys.DOCUMENT_CATEGORY);
            return this;
        }
    }
}