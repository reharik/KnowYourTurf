using System;
using System.Collections.Generic;
using System.Linq;
using HtmlTags;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class FieldListGrid : Grid<Field>, IEntityListGrid<Field>
    {

        public FieldListGrid(IGridBuilder<Field> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<Field> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<FieldDashboardController>(x => x.ViewField(null))
                .ToPerformAction(ColumnAction.Redirect)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.Description);
            GridBuilder.DisplayFor(x => x.Size);
            GridBuilder.DisplayFor(x => x.Abbreviation);
            return this;
        }
    }
}