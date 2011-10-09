using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class EquipmentListController:KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEquipmentListGrid _equipmentListGrid;

        public EquipmentListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEquipmentListGrid equipmentListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _equipmentListGrid = equipmentListGrid;
        }

        public ActionResult EquipmentList()
        {
            var url = UrlContext.GetUrlForAction<EquipmentListController>(x => x.Equipments(null));
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<EquipmentController>(x => x.AddEdit(null)),
                ListDefinition = _equipmentListGrid.GetGridDefinition(url, WebLocalizationKeys.EQUIPMENT)
            };
            return View(model);
        }

        public JsonResult Equipments(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Equipment>(input.filters);
            var gridItemsViewModel = _equipmentListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}