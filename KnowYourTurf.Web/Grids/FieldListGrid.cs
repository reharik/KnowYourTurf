using System;
using System.Collections.Generic;
using System.Linq;
using HtmlTags;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public interface IFieldListGrid
    {
        void AddColumnModifications(Action<IGridColumn, Field> modifications);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<Field> items, string gridName = "gridContainer");
    }

    public class FieldListGrid : Grid<Field>, IFieldListGrid
    {

        public FieldListGrid(IGridBuilder<Field> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
        {
        }

        protected override Grid<Field> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
                .ForAction<FieldController>(x => x.Delete(null))
                .ToPerformAction(ColumnAction.Delete)
               .ImageName("delete.png")
                .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<FieldController>(x => x.AddEdit(null))
                .ToPerformAction(ColumnAction.Edit)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<FieldDashboardController>(x => x.ViewField(null))
                .ToPerformAction(ColumnAction.Redirect)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.Description);
            GridBuilder.DisplayFor(x => x.Size);
            GridBuilder.DisplayFor(x => x.Description);
            return this;
        }
    }
}