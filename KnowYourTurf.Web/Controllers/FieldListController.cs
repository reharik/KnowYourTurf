using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class FieldListController : KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Field> _fieldListGrid;
        private readonly IRepository _repository;

        public FieldListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Field> fieldListGrid,
            IRepository repository)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _fieldListGrid = fieldListGrid;
            _repository = repository;
        }

        public ActionResult ItemList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<FieldListController>(x => x.Fields(null)) + "?RootId=" + input.RootId;
            ListViewModel model = new ListViewModel()
            {
                gridDef = _fieldListGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.FIELDS.ToString()
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Fields(GridItemsRequestModel input)
        {
            var category = _repository.Find<Category>(input.RootId);
            var items = _dynamicExpressionQuery.PerformQuery(category.Fields, input.filters);
            var gridItemsViewModel = _fieldListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}