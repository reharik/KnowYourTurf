using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html;
using KnowYourTurf.Core.Domain;
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

        public ActionResult Display_Template(ViewModel input)
        {
            return View("Display",new ListTypeListViewModel());
        }

        public ActionResult Display(ViewModel input)
        {
            var model = new ListTypeListViewModel()
                                              {
                                                  _eventTypeGridUrl = UrlContext.GetUrlForAction<ListTypeListController>(x => x.EventTypeGrid(null)),
                                                  _taskTypeGridUrl = UrlContext.GetUrlForAction<ListTypeListController>(x => x.TaskTypeGrid(null)),
                                                  _documentCategoryGridUrl = UrlContext.GetUrlForAction<ListTypeListController>(x => x.DocumentCategoryGrid(null)),
                                                  _photoCategoryGridUrl = UrlContext.GetUrlForAction<ListTypeListController>(x => x.PhotoCategoryGrid(null)),

                                                  _deleteMultipleTaskTypesUrl = UrlContext.GetUrlForAction<TaskTypeController>(x => x.DeleteMultiple(null)),
                                                  _deleteMultipleEventTypesUrl = UrlContext.GetUrlForAction<EventTypeController>(x => x.DeleteMultiple(null)),
                                                  _deleteMultiplePhotoCatUrl = UrlContext.GetUrlForAction<PhotoCategoryController>(x => x.DeleteMultiple(null)),
                                                  _deleteMultipleDocCatUrl = UrlContext.GetUrlForAction<DocumentCategoryController>(x => x.DeleteMultiple(null)),
                                              };
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public ActionResult EventTypeGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EventTypeController>(x => x.ListTypes(null));
            ListViewModel model = new ListViewModel()
            {
                gridDef = _eventTypeListGrid.GetGridDefinition(url),
                ParentId = input.ParentId
            };
            model.headerButtons.Add("delete");
            model.headerButtons.Add("new");
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TaskTypeGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<TaskTypeController>(x => x.ListTypes(null));
            ListViewModel model = new ListViewModel()
            {
                gridDef = _taskTypeListGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId
            };
            model.headerButtons.Add("delete");
            model.headerButtons.Add("new");
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DocumentCategoryGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<DocumentCategoryController>(x => x.ListTypes(null));
            ListViewModel model = new ListViewModel()
            {
                gridDef = _documentCategoryListGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId
            };
            model.headerButtons.Add("delete");
            model.headerButtons.Add("new");
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PhotoCategoryGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<PhotoCategoryController>(x => x.ListTypes(null));
            ListViewModel model = new ListViewModel()
            {
                gridDef = _photoCategoryListGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId
            };
            model.headerButtons.Add("delete");
            model.headerButtons.Add("new");
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    
    
    
    }
}