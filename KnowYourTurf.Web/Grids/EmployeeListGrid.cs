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
    public interface IEmployeeListGrid
    {
        void AddColumnModifications(Action<IGridColumn, Employee> modifications);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<Employee> items, string gridName = "gridContainer");
    }

    public class EmployeeListGrid : Grid<Employee>, IEmployeeListGrid
    {

        public EmployeeListGrid(IGridBuilder<Employee> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
        {
        }

        protected override Grid<Employee> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
               .ForAction<EmployeeController>(x => x.Delete(null))
               .ToPerformAction(ColumnAction.Delete)
                .ImageName("delete.png")
               .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<EmployeeController>(x => x.AddEdit(null))
                .ToPerformAction(ColumnAction.Edit)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.FullName)
                .ForAction<EmployeeDashboardController>(x => x.ViewEmployee(null))
                .ToPerformAction(ColumnAction.Redirect)
                .IsSortable(false)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.EmployeeId);
            GridBuilder.DisplayFor(x => x.PhoneMobile);
            GridBuilder.DisplayFor(x => x.Email).FormatValue(GridColumnFormatter.EMail);
            return this;
        }
    }

}