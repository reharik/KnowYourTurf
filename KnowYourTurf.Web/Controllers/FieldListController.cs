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
            var url = UrlContext.GetUrlForAction<FieldListController>(x => x.Fields(null))+"?ParentId="+input.ParentId;
            ListViewModel model = new ListViewModel()
            {
                AddUpdateUrl =  UrlContext.GetUrlForAction<FieldController>(x => x.AddUpdate(null)),
                gridDef = _fieldListGrid.GetGridDefinition(url),
                Title = WebLocalizationKeys.FIELDS.ToString(),
                ParentId = input.ParentId
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Fields(GridItemsRequestModel input)
        {
            var category = _repository.Find<Category>(input.ParentId);
            var items = _dynamicExpressionQuery.PerformQuery(category.Fields, input.filters);
            var gridItemsViewModel = _fieldListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}