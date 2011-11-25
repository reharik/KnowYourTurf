using System;
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
    public interface IEntityListGrid<ENTITY> where ENTITY : DomainEntity
    {
        void AddColumnModifications(Action<IGridColumn, ENTITY> modification);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<ENTITY> items, string gridName = null);
    }
    public class AdminListGrid : Grid<Administrator>, IEntityListGrid<Administrator>
    {

        public AdminListGrid(IGridBuilder<Administrator> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<Administrator> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
              .ForAction<AdminController>(x => x.Delete(null))
              .ToPerformAction(ColumnAction.Delete)
               .ImageName("delete.png")
              .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<AdminController>(x => x.Admin(null))
                .ToPerformAction(ColumnAction.Edit)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.FullName)
                .ForAction<AdminDashboardController>(x => x.ViewAdmin(null))
                .ToPerformAction(ColumnAction.Redirect)
                .IsSortable(false)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.AdminId);
            GridBuilder.DisplayFor(x => x.PhoneMobile);
            GridBuilder.DisplayFor(x => x.Email);
            return this;
        }
    }

}