using CC.Core.DomainTools;
using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
  public class EmailJobListGrid : Grid<EmailJob>, IEntityListGrid<EmailJob>
    {
        public EmailJobListGrid(IGridBuilder<EmailJob> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<EmailJob> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<EmailJobController>(x => x.AddUpdate(null))
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.Subject);
            GridBuilder.DisplayFor(x => x.Frequency);
            return this;
        }
    }
}