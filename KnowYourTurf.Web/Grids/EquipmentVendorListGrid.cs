using CC.Core.DomainTools;
using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Domain.Persistence;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class EquipmentVendorListGrid : Grid<EquipmentVendor>, IEntityListGrid<EquipmentVendor>
    {

        public EquipmentVendorListGrid(IGridBuilder<EquipmentVendor> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<EquipmentVendor> BuildGrid()
        {

            GridBuilder.LinkColumnFor(x => x.Company)
                .ForAction<VendorController>(x => x.AddUpdate(null))
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.Address1).DisplayHeader(WebLocalizationKeys.ADDRESS);
            GridBuilder.DisplayFor(x => x.City);
            GridBuilder.DisplayFor(x => x.State);
            GridBuilder.DisplayFor(x => x.Phone);
            GridBuilder.DisplayFor(x => x.Fax);
            GridBuilder.DisplayFor(x => x.Website);
            GridBuilder.ImageButtonColumn()
                .ForAction<VendorContactListController>(x => x.ItemList(null))
                .ToPerformAction(ColumnAction.Redirect)
                .ImageName("AddContact.png");
            return this;
        }
    }
}