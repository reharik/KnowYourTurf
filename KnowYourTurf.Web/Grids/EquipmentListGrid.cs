using CC.Core.DomainTools;
using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class EquipmentListGrid : Grid<Equipment>, IEntityListGrid<Equipment>
    {

        public EquipmentListGrid(IGridBuilder<Equipment> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<Equipment> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                 .ForAction<EquipmentDashboardController>(x => x.ViewEquipment(null))
                .ToPerformAction(ColumnAction.Redirect)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.EquipmentVendor.Company);
            GridBuilder.DisplayFor(x => x.WebSite);
            GridBuilder.DisplayFor(x => x.TotalHours);
            return this;
        }
    }
}