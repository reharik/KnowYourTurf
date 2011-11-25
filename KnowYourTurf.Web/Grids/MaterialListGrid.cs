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
    public class MaterialListGrid : Grid<Material>, IEntityListGrid<Material>
    {
        public MaterialListGrid(IGridBuilder<Material> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<Material> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
               .ForAction<MaterialController>(x => x.Delete(null))
               .ToPerformAction(ColumnAction.Delete)
               .ImageName("delete.png")
               .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<MaterialController>(x => x.AddEdit(null))
                .ToPerformAction(ColumnAction.Edit)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<MaterialController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            return this;
        }
    }

    public class POMaterialListGrid : Grid<Material>, IEntityListGrid<Material>
    {
        public POMaterialListGrid(IGridBuilder<Material> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<Material> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<MaterialController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            return this;
        }
    }

}