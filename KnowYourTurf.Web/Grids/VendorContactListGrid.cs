using CC.Core.DomainTools;
using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class VendorContactListGrid : Grid<VendorContact>, IEntityListGrid<VendorContact>
    {

     public VendorContactListGrid(IGridBuilder<VendorContact> gridBuilder)
            : base(gridBuilder)
        {
        }

     protected override Grid<VendorContact> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.FullName)
                .ForAction<VendorContactController>(x => x.AddUpdate(null))
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .IsSortable(false)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.Email).FormatValue(GridColumnFormatter.EMail);
            GridBuilder.DisplayFor(x => x.Phone);
            GridBuilder.DisplayFor(x => x.Address1).DisplayHeader(WebLocalizationKeys.ADDRESS);
            GridBuilder.DisplayFor(x => x.City);
            GridBuilder.DisplayFor(x => x.State);
            GridBuilder.DisplayFor(x => x.Fax);
            GridBuilder.SetSearchField(x => x.LastName);
            GridBuilder.SetDefaultSortColumn(x => x.LastName);
            return this;
        }
    }
}
