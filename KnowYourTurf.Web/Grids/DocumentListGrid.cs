using CC.Core.DomainTools;
using CC.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class DocumentListGrid : Grid<Document>, IEntityListGrid<Document>
    {
        public DocumentListGrid(IGridBuilder<Document> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<Document> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ToPerformAction(ColumnAction.AddUpdateItem).WithId("documentlist")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.DocumentCategory.Name).DisplayHeader(WebLocalizationKeys.DOCUMENT_CATEGORY);
            return this;
        }
    }
}