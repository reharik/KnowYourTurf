using System;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class DocumentListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
       private readonly IEntityListGrid<Document> _documentListGrid;

        public DocumentListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Document> documentListGrid)
        {
            if (documentListGrid == null) throw new ArgumentNullException("documentListGrid");
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _documentListGrid = documentListGrid;
        }

        public ActionResult DocumentList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<DocumentListController>(x => x.Documents(null));
            ListViewModel model = new ListViewModel()
            {
                AddUpdateUrl = UrlContext.GetUrlForAction<DocumentController>(x => x.AddUpdate(null)),
                GridDefinition = _documentListGrid.GetGridDefinition(url)
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