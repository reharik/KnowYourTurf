using CC.Core.DomainTools;
using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class ChemicalListGrid : Grid<Chemical>, IEntityListGrid<Chemical>
    {
        public ChemicalListGrid(IGridBuilder<Chemical> gridBuilder)
            : base(gridBuilder)
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
        public POChemicalListGrid(IGridBuilder<Chemical> gridBuilder)
            : base(gridBuilder)
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