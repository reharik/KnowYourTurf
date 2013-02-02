using CC.Core.DomainTools;
using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class VendorListGrid : Grid<FieldVendor>, IEntityListGrid<FieldVendor>
    {

        public VendorListGrid(IGridBuilder<FieldVendor> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<FieldVendor> BuildGrid()
        {

            GridBuilder.LinkColumnFor(x => x.Client)
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
            GridBuilder.SetSearchField(x => x.Client);
            GridBuilder.SetDefaultSortColumn(x => x.Client);
            return this;
        }
    }
}