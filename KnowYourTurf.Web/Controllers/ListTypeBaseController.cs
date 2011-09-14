using System;
using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Filters;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Models;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public abstract class ListTypeBaseController<LISTTYPE> : AdminControllerBase where LISTTYPE : ListType
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IContainer _container;
        private readonly IListTypeListGrid<LISTTYPE> _listTypeListGrid;

        public ListTypeBaseController(IDynamicExpressionQuery dynamicExpressionQuery,
                              IRepository repository,
                              ISaveEntityService saveEntityService,
                              IListTypeListGrid<LISTTYPE> listTypeListGrid) 
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _repository = repository;
            _saveEntityService = saveEntityService;
            _listTypeListGrid = listTypeListGrid;
        }

        public virtual ActionResult AddUpdate(ViewModel input)
        {
            var listType = input.EntityId > 0
                               ? _repository.Find<LISTTYPE>(input.EntityId)
                               : Activator.CreateInstance<LISTTYPE>();
            var model = new ListTypeViewModel
                            {
                                ListType = listType,
                            };
            return PartialView(model);
        }

        public JsonResult ListTypes(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<LISTTYPE>(input.filters);
            var gridItemsViewModel = _listTypeListGrid.GetGridItemsViewModel(input.PageSortFilter, items,typeof(LISTTYPE).Name+"Grid");
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Display(ViewModel input)
        {
            var listType = _repository.Find<LISTTYPE>(input.EntityId);
            var model = new ListTypeViewModel
                            {
                                ListType = listType,
                            };
            return PartialView(model);
        }

        public virtual ActionResult SaveListType(ListTypeViewModel input)
        {
            var listType = input.ListType.EntityId > 0
                               ? _repository.Find<LISTTYPE>(input.ListType.EntityId)
                               : Activator.CreateInstance<LISTTYPE>();
            listType.Description = input.ListType.Description;
            listType.Name = input.ListType.Name;
            listType.Status = input.ListType.Status;
            var crudManager = _saveEntityService.ProcessSave(listType);
            var notification = crudManager.Finish();
            notification.Variable = typeof(LISTTYPE).Name;
            return Json(notification, JsonRequestBehavior.AllowGet);
        }
    }

    public class EventTypeController : ListTypeBaseController<EventType>
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public EventTypeController(IDynamicExpressionQuery dynamicExpressionQuery,
                              IRepository repository,
                              ISaveEntityService saveEntityService,
                              IEventTypeListGrid listTypeListGrid)
            : base(dynamicExpressionQuery, repository, saveEntityService,(IListTypeListGrid<EventType>)listTypeListGrid)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public override ActionResult AddUpdate(ViewModel input)
        {
            var listType = input.EntityId > 0
                               ? _repository.Find<EventType>(input.EntityId)
                               : Activator.CreateInstance<EventType>();
            var model = new EventTypeViewModel
            {
                ListType = listType,
            };
            return PartialView(model);
        }

        public ActionResult SaveEventType(EventTypeViewModel input)
        {
            var listType = input.ListType.EntityId > 0
                               ? _repository.Find<EventType>(input.ListType.EntityId)
                               : Activator.CreateInstance<EventType>();
            listType.Description = input.ListType.Description;
            listType.Name = input.ListType.Name;
            listType.Status = input.ListType.Status;
            listType.EventColor= input.ListType.EventColor;
            var crudManager = _saveEntityService.ProcessSave(listType);
            var notification = crudManager.Finish();
            notification.Variable = typeof(EventType).Name;
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

    }
    public class TaskTypeController : ListTypeBaseController<TaskType>
    {
        public TaskTypeController(IDynamicExpressionQuery dynamicExpressionQuery,
                              IRepository repository,
                              ISaveEntityService saveEntityService,
                              ITaskTypeListGrid listTypeListGrid)
            : base(dynamicExpressionQuery, repository, saveEntityService, (IListTypeListGrid<TaskType>)listTypeListGrid)
        {
        }
    }
    public class DocumentCategoryController : ListTypeBaseController<DocumentCategory>
    {
        public DocumentCategoryController(IDynamicExpressionQuery dynamicExpressionQuery,
                              IRepository repository,
                              ISaveEntityService saveEntityService,
                              IDocumentCategoryListGrid listTypeListGrid)
            : base(dynamicExpressionQuery, repository, saveEntityService, (IListTypeListGrid<DocumentCategory>)listTypeListGrid)
        {
        }
    }
    public class PhotoCategoryController : ListTypeBaseController<PhotoCategory>
    {
        public PhotoCategoryController(IDynamicExpressionQuery dynamicExpressionQuery,
                              IRepository repository,
                              ISaveEntityService saveEntityService,
                              IPhotoCategoryListGrid listTypeListGrid)
            : base(dynamicExpressionQuery, repository, saveEntityService, (IListTypeListGrid<PhotoCategory>)listTypeListGrid)
        {
        }
    }
}