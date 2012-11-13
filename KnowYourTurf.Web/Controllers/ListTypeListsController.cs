using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
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
        private readonly IEntityListGrid<EquipmentTaskType> _equipmentTaskTypeListGrid;
        private readonly IEntityListGrid<EquipmentTask> _equipmentTypeListGrid;
        private readonly IEntityListGrid<Part> _partListGrid;

        public ListTypeListController(
            IEntityListGrid<EventType> eventTypeListGrid,
            IEntityListGrid<TaskType> taskTypeListGrid,
            IEntityListGrid<PhotoCategory> photoCategoryListGrid,
            IEntityListGrid<DocumentCategory> documentCategoryListGrid,
            IEntityListGrid<EquipmentTaskType> equipmentTaskTypeListGrid,
            IEntityListGrid<EquipmentTask> equipmentTypeListGrid,
            IEntityListGrid<Part> partListGrid)
        {
            _eventTypeListGrid = eventTypeListGrid;
            _taskTypeListGrid = taskTypeListGrid;
            _photoCategoryListGrid = photoCategoryListGrid;
            _documentCategoryListGrid = documentCategoryListGrid;
            _equipmentTaskTypeListGrid = equipmentTaskTypeListGrid;
            _equipmentTypeListGrid = equipmentTypeListGrid;
            _partListGrid = partListGrid;
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
                                                  _equipmentTaskTypeGridUrl = UrlContext.GetUrlForAction<ListTypeListController>(x => x.EquipmentTaskTypeGrid(null)),
                                                  _equipmentTypeGridUrl = UrlContext.GetUrlForAction<ListTypeListController>(x => x.EquipmentTypeGrid(null)),
                                                  _partsGridUrl = UrlContext.GetUrlForAction<ListTypeListController>(x => x.PartGrid(null)),

                                                  _deleteMultipleTaskTypesUrl = UrlContext.GetUrlForAction<TaskTypeController>(x => x.DeleteMultiple(null)),
                                                  _deleteMultipleEventTypesUrl = UrlContext.GetUrlForAction<EventTypeController>(x => x.DeleteMultiple(null)),
                                                  _deleteMultiplePhotoCatUrl = UrlContext.GetUrlForAction<PhotoCategoryController>(x => x.DeleteMultiple(null)),
                                                  _deleteMultipleDocCatUrl = UrlContext.GetUrlForAction<DocumentCategoryController>(x => x.DeleteMultiple(null)),
                                                  _deleteMultipleEquipTaskTypeUrl = UrlContext.GetUrlForAction<EquipmentTaskTypeController>(x => x.DeleteMultiple(null)),
                                                  _deleteMultipleEquipTypeUrl = UrlContext.GetUrlForAction<EquipmentTypeController>(x => x.DeleteMultiple(null)),
                                                  _deleteMultiplePartsUrl = UrlContext.GetUrlForAction<PartController>(x => x.DeleteMultiple(null)),
                                              };
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public ActionResult EquipmentTaskTypeGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EquipmentTaskTypeController>(x => x.ListTypes(null));
            ListViewModel model = new ListViewModel()
            {
                gridDef = _equipmentTaskTypeListGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId
            };
            model.headerButtons.Add("delete");
            model.headerButtons.Add("new");
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EquipmentTypeGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EquipmentTypeController>(x => x.ListTypes(null));
            ListViewModel model = new ListViewModel()
            {
                gridDef = _equipmentTypeListGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId
            };
            model.headerButtons.Add("delete");
            model.headerButtons.Add("new");
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PartGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<PartController>(x => x.ListTypes(null));
            ListViewModel model = new ListViewModel()
            {
                gridDef = _partListGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId
            };
            model.headerButtons.Add("delete");
            model.headerButtons.Add("new");
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EventTypeGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EventTypeController>(x => x.ListTypes(null));
            ListViewModel model = new ListViewModel()
            {
                gridDef = _eventTypeListGrid.GetGridDefinition(url, input.User),
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