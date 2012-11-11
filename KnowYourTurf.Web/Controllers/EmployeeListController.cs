using System.Linq;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class EmployeeListController : AdminControllerBase
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
       private readonly IEntityListGrid<User> _employeeListGrid;

        public EmployeeListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<User> employeeListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _employeeListGrid = employeeListGrid;
        }

        public ActionResult ItemList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EmployeeListController>(x => x.Employees(null));
            var model = new ListViewModel()
            {
                gridDef = _employeeListGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.EMPLOYEES.ToString()
            };
            model.headerButtons.Add("new");
            model.headerButtons.Add("delete");
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Employees(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<User>(input.filters, x=>x.UserRoles.Any(i=>i.Name==UserType.Employee.ToString()) && x.UserLoginInfo.Status==Status.Active.ToString());
            var gridItemsViewModel = _employeeListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}