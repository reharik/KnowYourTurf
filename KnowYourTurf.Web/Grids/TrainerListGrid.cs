using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enumerations;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Areas.Schedule.Grids
{
    public class TrainerListGrid : Grid<User>, IEntityListGrid<User>
    {

        public TrainerListGrid(IGridBuilder<User> gridBuilder,
                                      ISessionContext sessionContext,
                                      IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<User> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.FirstName)
                .ForAction<TrainerController>(x => x.AddUpdate(null))
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM)
                .DefaultSortColumn();
            GridBuilder.DisplayFor(x => x.Email);
            GridBuilder.DisplayFor(x => x.PhoneMobile);
            return this;
        }
    }
}