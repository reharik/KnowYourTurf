using CC.Core.DomainTools;
using CC.Core.Html.Grid;
using KnowYourTurf.Core.Config;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class AdminListGrid : Grid<User>, IEntityListGrid<User>
    {

        public AdminListGrid(IGridBuilder<User> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<User> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.FullName)
                .ForAction<AdminDashboardController>(x => x.ViewAdmin(null))
                .ToPerformAction(ColumnAction.Redirect)
                .IsSortable(false)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.PhoneMobile);
            GridBuilder.DisplayFor(x => x.Email);
            GridBuilder.SetSearchField(x=>x.LastName);
            GridBuilder.SetDefaultSortColumn(x=>x.LastName);
            return this;
        }

    }

}