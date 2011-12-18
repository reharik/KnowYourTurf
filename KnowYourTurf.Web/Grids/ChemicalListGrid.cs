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
    public class ChemicalListGrid : Grid<Chemical>, IEntityListGrid<Chemical>
    {
        public ChemicalListGrid(IGridBuilder<Chemical> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<Chemical> BuildGrid()
        {
           
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<ChemicalController>(x => x.AddUpdate(null))
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.ActiveIngredient);
            GridBuilder.DisplayFor(x => x.ActiveIngredientPercent);
            GridBuilder.DisplayFor(x => x.EPAEstNumber);
            GridBuilder.DisplayFor(x => x.EPARegNumber);
            return this;
        }
    }

    public class POChemicalListGrid : Grid<Chemical>, IEntityListGrid<Chemical>
    {
        public POChemicalListGrid(IGridBuilder<Chemical> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<Chemical> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<ChemicalController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.Manufacturer);
            GridBuilder.DisplayFor(x => x.ActiveIngredient);
            GridBuilder.DisplayFor(x => x.ActiveIngredientPercent);
            GridBuilder.DisplayFor(x => x.EPAEstNumber);
            GridBuilder.DisplayFor(x => x.EPARegNumber);
            return this;
        }
    }
}