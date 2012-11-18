using System;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html;
using CC.Core.Services;
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

        public ActionResult ItemList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<DocumentListController>(x => x.Documents(null));
            ListViewModel model = new ListViewModel()
            {
                deleteMultipleUrl = UrlContext.GetUrlForAction<DocumentController>(x => x.DeleteMultiple(null)),
                gridDef = _documentListGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.DOCUMENTS.ToString()
            };
            model.headerButtons.Add("delete");
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult Documents(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Document>(input.filters);
            var gridItemsViewModel = _documentListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}