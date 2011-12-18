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

    public class EmailTemplateListGrid : Grid<EmailTemplate>, IEntityListGrid<EmailTemplate>
    {
        public EmailTemplateListGrid(IGridBuilder<EmailTemplate> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
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