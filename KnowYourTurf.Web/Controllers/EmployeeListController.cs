using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class EmployeeListController : AdminControllerBase
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEmployeeListGrid _employeeListGrid;

        public EmployeeListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEmployeeListGrid employeeListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _employeeListGrid = employeeListGrid;
        }

        public ActionResult EmployeeList()
        {
            var url = UrlContext.GetUrlForAction<EmployeeListController>(x => x.Employees(null));
            var model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<EmployeeController>(x => x.AddEdit(null)),
                ListDefinition = _employeeListGrid.GetGridDefinition(url, WebLocalizationKeys.EMPLOYEES),
                CrudTitle = WebLocalizationKeys.EMPLOYEE_INFORMATION.ToString()
            };
            return View(model);
        }

        public JsonResult Employees(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Employee>(input.filters);
            var gridItemsViewModel = _employeeListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}