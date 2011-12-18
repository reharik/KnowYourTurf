using System;
using System.Collections.Generic;
using System.Linq;
using HtmlTags;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class VendorListGrid : Grid<Vendor>, IEntityListGrid<Vendor>
    {

        public VendorListGrid(IGridBuilder<Vendor> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<Vendor> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
               .ForAction<VendorController>(x => x.Delete(null))
               .ToPerformAction(ColumnAction.Delete)
               .ImageName("delete.png")
               .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<VendorController>(x => x.AddEdit(null))
                .ToPerformAction(ColumnAction.Edit)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.Company)
                .ForAction<VendorController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.Address1).DisplayHeader(WebLocalizationKeys.ADDRESS);
            GridBuilder.DisplayFor(x => x.City);
            GridBuilder.DisplayFor(x => x.State);
            GridBuilder.DisplayFor(x => x.Phone);
            GridBuilder.DisplayFor(x => x.Fax);
            GridBuilder.DisplayFor(x => x.Website);
            GridBuilder.ImageButtonColumn()
                .ForAction<VendorContactListController>(x => x.VendorContactList(null))
                .ToPerformAction(ColumnAction.Redirect)
                .ImageName("AddContact.png");
            return this;
        }
    }
}