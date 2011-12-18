using System;
using System.Collections;
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
    public class CalculatorListGrid : Grid<Calculator>, IEntityListGrid<Calculator>
    {
        public CalculatorListGrid(IGridBuilder<Calculator> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<Calculator> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
                .ForAction<CalculatorController>(x => x.Calculator(null))
                .ToPerformAction(ColumnAction.Edit)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.Name);
            GridBuilder.DisplayFor(x => x.DateCreated);
            return this;
        }
    }
}