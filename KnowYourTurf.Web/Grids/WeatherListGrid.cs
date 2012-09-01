using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class WeatherListGrid : Grid<Weather>, IEntityListGrid<Weather>
    {
        public WeatherListGrid(IGridBuilder<Weather> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<Weather> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Date)
                .ToPerformAction(ColumnAction.DisplayItem)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.DewPoint);
            GridBuilder.DisplayFor(x => x.HighTemperature);
            GridBuilder.DisplayFor(x => x.LowTemperature);
            GridBuilder.DisplayFor(x => x.RainPrecipitation);
            GridBuilder.DisplayFor(x => x.WindSpeed);
            GridBuilder.DisplayFor(x => x.Humidity);
            return this;
        }
    }

}