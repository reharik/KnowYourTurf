using System;
using System.Collections.Generic;
using System.Linq;
using HtmlTags;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public interface IWeatherListGrid
    {
        void AddColumnModifications(Action<IGridColumn, Weather> modifications);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<Weather> items, string gridName = "gridContainer");
    }

    public class WeatherListGrid : Grid<Weather>, IWeatherListGrid
    {
        public WeatherListGrid(IGridBuilder<Weather> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
        {
        }

        protected override Grid<Weather> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.Date)
                .ForAction<WeatherController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.DewPoint);
            GridBuilder.DisplayFor(x => x.HighTemperature);
            GridBuilder.DisplayFor(x => x.LowTemperature);
            GridBuilder.DisplayFor(x => x.RainPrecipitation);
            GridBuilder.DisplayFor(x => x.WindSpeed);
            return this;
        }
    }

}