using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
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

        public ActionResult ItemList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EquipmentListController>(x => x.Equipments(null));
            ListViewModel model = new ListViewModel()
            {
                deleteMultipleUrl= UrlContext.GetUrlForAction<EquipmentController>(x => x.DeleteMultiple(null)),
                gridDef = _equipmentListGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.EQUIPMENT.ToString()
            };
            model.headerButtons.Add("new");
            model.headerButtons.Add("delete");
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Equipments(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Equipment>(input.filters);
            var gridItemsViewModel = _equipmentListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}