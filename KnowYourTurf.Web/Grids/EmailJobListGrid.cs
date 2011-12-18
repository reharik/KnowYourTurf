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
            GridBuilder.ImageButtonColumn()
               .ForAction<EmailJobController>(x => x.Delete(null))
               .ToPerformAction(ColumnAction.Delete)
                .ImageName("delete.png")
               .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<EmailJobController>(x => x.EmailJob(null))
                .ToPerformAction(ColumnAction.Edit)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.Name)
                .ForAction<EmailJobController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.Subject);
            GridBuilder.DisplayFor(x => x.Frequency);
            return this;
        }
    }
}