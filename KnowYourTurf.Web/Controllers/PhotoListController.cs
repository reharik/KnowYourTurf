using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class PhotoListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
       private readonly IEntityListGrid<Photo> _photoListGrid;

        public PhotoListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Photo> photoListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _photoListGrid = photoListGrid;
        }

        public ActionResult ItemList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<PhotoListController>(x => x.Photos(null));
            ListViewModel model = new ListViewModel()
            {
                deleteMultipleUrl = UrlContext.GetUrlForAction<PhotoController>(x => x.DeleteMultiple(null)),
                gridDef = _photoListGrid.GetGridDefinition(url),
                _Title = WebLocalizationKeys.PHOTOS.ToString()
            };
            model.headerButtons.Add("new");
            model.headerButtons.Add("delete");
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult Photos(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Photo>(input.filters);
            var gridItemsViewModel = _photoListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}