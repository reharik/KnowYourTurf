using CC.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class PhotoListGrid : Grid<Photo>, IEntityListGrid<Photo>
    {
        public PhotoListGrid(IGridBuilder<Photo> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<Photo> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ToPerformAction(ColumnAction.AddUpdateItem).WithId("photolist")
                .ToolTip(WebLocalizationKeys.EDIT_EVENT);
            GridBuilder.DisplayFor(x => x.PhotoCategory.Name)
                .DisplayHeader(WebLocalizationKeys.DOCUMENT_CATEGORY);
            return this;
        }
    }
}