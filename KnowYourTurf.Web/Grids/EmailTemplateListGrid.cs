using CC.Core.DomainTools;
using CC.Core.Html.Grid;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{

    public class EmailTemplateListGrid : Grid<EmailTemplate>, IEntityListGrid<EmailTemplate>
    {
        public EmailTemplateListGrid(IGridBuilder<EmailTemplate> gridBuilder)
            : base(gridBuilder)
        {
        }

        protected override Grid<EmailTemplate> BuildGrid()
        {
            
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<EmailTemplateController>(x => x.AddUpdate(null))
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.DateCreated);
            return this;
        }
    }
}