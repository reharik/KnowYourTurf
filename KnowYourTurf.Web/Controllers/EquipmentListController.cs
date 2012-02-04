using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class EquipmentListController:KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Equipment> _equipmentListGrid;

        public EquipmentListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Equipment> equipmentListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _equipmentListGrid = equipmentListGrid;
        }

        public ActionResult EquipmentList()
        {
            var url = UrlContext.GetUrlForAction<EquipmentListController>(x => x.Equipments(null));
            ListViewModel model = new ListViewModel()
            {
                AddUpdateUrl = UrlContext.GetUrlForAction<EquipmentController>(x => x.AddUpdate(null)),
                DeleteMultipleUrl= UrlContext.GetUrlForAction<EquipmentController>(x => x.DeleteMultiple(null)),
                GridDefinition = _equipmentListGrid.GetGridDefinition(url),
                Title = WebLocalizationKeys.EQUIPMENT.ToString()
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