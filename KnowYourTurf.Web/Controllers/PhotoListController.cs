using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

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
                gridDef = _photoListGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.PHOTOS.ToString()
            };
            model.headerButtons.Add("delete");
            return new CustomJsonResult(model);
        }
        
        public JsonResult Photos(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Photo>(input.filters);
            var gridItemsViewModel = _photoListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }
    }
}