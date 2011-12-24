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

        public ActionResult PhotoList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<PhotoListController>(x => x.Photos(null));
            ListViewModel model = new ListViewModel()
            {
                AddUpdateUrl = UrlContext.GetUrlForAction<PhotoController>(x => x.AddUpdate(null)),
                GridDefinition = _photoListGrid.GetGridDefinition(url),
                Title = WebLocalizationKeys.PHOTOS.ToString()
            };
            return View(model);
        }
        
        public JsonResult Photos(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Photo>();
            var gridItemsViewModel = _photoListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}