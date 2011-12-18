using System.Web.Mvc;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class ListTypeListController : AdminControllerBase
    {
        private readonly IEntityListGrid<EventType> _eventTypeListGrid;
        private readonly IEntityListGrid<TaskType> _taskTypeListGrid;
        private readonly IEntityListGrid<DocumentCategory> _photoCategoryListGrid;
        private readonly IEntityListGrid<PhotoCategory> _documentCategoryListGrid;

        public ListTypeListController(
            IEntityListGrid<EventType> eventTypeListGrid,
            IEntityListGrid<TaskType> taskTypeListGrid,
            IEntityListGrid<DocumentCategory> photoCategoryListGrid,
            IEntityListGrid<PhotoCategory> documentCategoryListGrid)
        {
            _eventTypeListGrid = eventTypeListGrid;
            _taskTypeListGrid = taskTypeListGrid;
            _photoCategoryListGrid = photoCategoryListGrid;
            _documentCategoryListGrid = documentCategoryListGrid;
        }

        public ActionResult ListType()
        {
            var gridUrlET = UrlContext.GetUrlForAction<EventTypeController>(x => x.ListTypes(null));
            var gridUrlTT = UrlContext.GetUrlForAction<TaskTypeController>(x => x.ListTypes(null));
            var gridUrlDC = UrlContext.GetUrlForAction<DocumentCategoryController>(x => x.ListTypes(null));
            var gridUrlPC = UrlContext.GetUrlForAction<PhotoCategoryController>(x => x.ListTypes(null));
            ListTypeListViewModel model = new ListTypeListViewModel()
                                              {
                                                  AddUpdateUrlET = UrlContext.GetUrlForAction<EventTypeController>(x => x.AddUpdate(null)),
                                                  AddUpdateUrlTT = UrlContext.GetUrlForAction<TaskTypeController>(x => x.AddUpdate(null)),
                                                  AddUpdateUrlDC = UrlContext.GetUrlForAction<DocumentCategoryController>(x => x.AddUpdate(null)),
                                                  AddUpdateUrlPC = UrlContext.GetUrlForAction<PhotoCategoryController>(x => x.AddUpdate(null)),
                                                  GridDefinition = _eventTypeListGrid.GetGridDefinition(gridUrlET),
                                                  ListDefinitionTT = _taskTypeListGrid.GetGridDefinition(gridUrlTT),
                                                  ListDefinitionDC = _documentCategoryListGrid.GetGridDefinition(gridUrlDC),
                                                  ListDefinitionPC = _photoCategoryListGrid.GetGridDefinition(gridUrlPC),
                                                  PopupTitleET = WebLocalizationKeys.EVENT_INFORMATION.ToString(),
                                                  PopupTitleTT = WebLocalizationKeys.TASK_INFORMATION.ToString(),
                                                  PopupTitlePC = WebLocalizationKeys.PHOTO_CATEGORY_INFORMATION.ToString(),
                                                  PopupTitleDC = WebLocalizationKeys.DOCUMENT_CATEGORY_INFORMATION.ToString()
                                              };
            return View(model);
        }
    }
}