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
            GridBuilder.LinkColumnFor(x => x.Name, "KYT")
                .ForAction<EquipmentController>(x => x.AddUpdate(null))
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.TotalHours);
            return this;
        }
    }
}