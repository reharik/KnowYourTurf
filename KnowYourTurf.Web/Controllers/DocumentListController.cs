using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Grids;

namespace KnowYourTurf.Web.Controllers
{
    public class DocumentListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IDocumentListGrid _documentListGrid;

        public DocumentListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IDocumentListGrid documentListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _documentListGrid = documentListGrid;
        }

        public ActionResult DocumentList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<DocumentListController>(x => x.Documents(null));
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<DocumentController>(x => x.AddUpdate(null)),
                ListDefinition = _documentListGrid.GetGridDefinition(url, WebLocalizationKeys.DOCUMENTS)
            };
            return View(model);
        }
        
        public JsonResult Documents(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Document>();
            var gridItemsViewModel = _documentListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}