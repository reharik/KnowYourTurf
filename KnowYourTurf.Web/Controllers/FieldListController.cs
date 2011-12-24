using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class FieldListController : KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Field> _fieldListGrid;

        public FieldListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Field> fieldListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _fieldListGrid = fieldListGrid;
        }

        public ActionResult FieldList()
        {
            var url = UrlContext.GetUrlForAction<FieldListController>(x => x.Fields(null));
            ListViewModel model = new ListViewModel()
            {
                AddUpdateUrl =  UrlContext.GetUrlForAction<FieldController>(x => x.AddUpdate(null)),
                GridDefinition = _fieldListGrid.GetGridDefinition(url),
                Title = WebLocalizationKeys.FIELDS.ToString()
            };
            return View(model);
        }

        public JsonResult Fields(GridItemsRequestModel input)
        {
              var items = _dynamicExpressionQuery.PerformQuery<Field>(input.filters);
            var gridItemsViewModel = _fieldListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}