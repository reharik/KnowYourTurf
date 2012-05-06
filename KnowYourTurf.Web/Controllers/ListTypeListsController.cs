using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class ListTypeListController : AdminControllerBase
    {
        private readonly IEntityListGrid<EventType> _eventTypeListGrid;
        private readonly IEntityListGrid<TaskType> _taskTypeListGrid;
        private readonly IEntityListGrid<PhotoCategory> _photoCategoryListGrid;
        private readonly IEntityListGrid<DocumentCategory> _documentCategoryListGrid;

        public ListTypeListController(
            IEntityListGrid<EventType> eventTypeListGrid,
            IEntityListGrid<TaskType> taskTypeListGrid,
            IEntityListGrid<PhotoCategory> photoCategoryListGrid,
            IEntityListGrid<DocumentCategory> documentCategoryListGrid)
        {
            _eventTypeListGrid = eventTypeListGrid;
            _taskTypeListGrid = taskTypeListGrid;
            _photoCategoryListGrid = photoCategoryListGrid;
            _documentCategoryListGrid = documentCategoryListGrid;
        }

        public ActionResult ItemList(ViewModel input)
        {
            var gridUrlET = UrlContext.GetUrlForAction<EventTypeController>(x => x.ListTypes(null));
            var gridUrlTT = UrlContext.GetUrlForAction<TaskTypeController>(x => x.ListTypes(null));
            var gridUrlDC = UrlContext.GetUrlForAction<DocumentCategoryController>(x => x.ListTypes(null));
            var gridUrlPC = UrlContext.GetUrlForAction<PhotoCategoryController>(x => x.ListTypes(null));
            var dmET = UrlContext.GetUrlForAction<EventTypeController>(x => x.DeleteMultiple(null));
            var dmTT = UrlContext.GetUrlForAction<TaskTypeController>(x => x.DeleteMultiple(null));
            var dmDC = UrlContext.GetUrlForAction<DocumentCategoryController>(x => x.DeleteMultiple(null));
            var dmPC = UrlContext.GetUrlForAction<PhotoCategoryController>(x => x.DeleteMultiple(null));
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
                                                  PopupTitleDC = WebLocalizationKeys.DOCUMENT_CATEGORY_INFORMATION.ToString(),
                                                  DeleteMultipleET = dmET,
                                                  DeleteMultipleTT = dmTT,
                                                  DeleteMultipleDC = dmDC,
                                                  DeleteMultiplePC = dmPC,
                                              };
            return View(model);
        }
    }
}