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
  public class EmailJobListGrid : Grid<EmailJob>, IEntityListGrid<EmailJob>
    {
        public EmailJobListGrid(IGridBuilder<EmailJob> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<EmailJob> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<EmailJobController>(x => x.EmailJob(null))
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.Subject);
            GridBuilder.DisplayFor(x => x.Frequency);
            return this;
        }
    }
}