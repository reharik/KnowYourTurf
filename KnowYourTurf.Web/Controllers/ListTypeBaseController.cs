using System;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
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
        private readonly IEntityListGrid<LISTTYPE> _listTypeListGrid;

        public ListTypeBaseController(IDynamicExpressionQuery dynamicExpressionQuery,
                                      IRepository repository,
                                      ISaveEntityService saveEntityService,
                                      IEntityListGrid<LISTTYPE> listTypeListGrid)
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
            var gridItemsViewModel = _listTypeListGrid.GetGridItemsViewModel(input.PageSortFilter, items,
                                                                             typeof (LISTTYPE).Name + "Grid");
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult Display(ViewModel input)
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
            notification.Variable = typeof (LISTTYPE).Name;
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
                                   IEntityListGrid<EventType> listTypeListGrid)
            : base(
                dynamicExpressionQuery, repository, saveEntityService, listTypeListGrid)
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

        public override ActionResult Display(ViewModel input)
        {
            var listType = _repository.Find<EventType>(input.EntityId);
            var model = new ListTypeViewModel
                            {
                                ListType = listType,
                                AddEditUrl =
                                    UrlContext.GetUrlForAction<EventTypeController>(x => x.AddUpdate(null)) + "/" +
                                    input.EntityId
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
            listType.EventColor = input.ListType.EventColor;
            var crudManager = _saveEntityService.ProcessSave(listType);
            var notification = crudManager.Finish();
            notification.Variable = typeof (EventType).Name;
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

    }

    public class TaskTypeController : ListTypeBaseController<TaskType>
    {
        private readonly IRepository _repository;

        public TaskTypeController(IDynamicExpressionQuery dynamicExpressionQuery,
                                  IRepository repository,
                                  ISaveEntityService saveEntityService,
                                  IEntityListGrid<TaskType> listTypeListGrid)
            : base(dynamicExpressionQuery, repository, saveEntityService, listTypeListGrid
                )
        {
            _repository = repository;
        }

        public override ActionResult Display(ViewModel input)
        {
            var listType = _repository.Find<TaskType>(input.EntityId);
            var model = new ListTypeViewModel
                            {
                                ListType = listType,
                                AddEditUrl =
                                    UrlContext.GetUrlForAction<TaskTypeController>(x => x.AddUpdate(null)) + "/" +
                                    input.EntityId
                            };
            return PartialView(model);
        }


    }

    public class DocumentCategoryController : ListTypeBaseController<DocumentCategory>
    {
        private readonly IRepository _repository;

        public DocumentCategoryController(IDynamicExpressionQuery dynamicExpressionQuery,
                                          IRepository repository,
                                          ISaveEntityService saveEntityService,
                                          IEntityListGrid<DocumentCategory> listTypeListGrid)
            : base(
                dynamicExpressionQuery, repository, saveEntityService,
                listTypeListGrid)
        {
            _repository = repository;
        }

        public override ActionResult Display(ViewModel input)
        {
            var listType = _repository.Find<DocumentCategory>(input.EntityId);
            var model = new ListTypeViewModel
                            {
                                ListType = listType,
                                AddEditUrl =
                                    UrlContext.GetUrlForAction<DocumentCategoryController>(x => x.AddUpdate(null)) + "/" +
                                    input.EntityId
                            };
            return PartialView(model);
        }
    }

    public class PhotoCategoryController : ListTypeBaseController<PhotoCategory>
    {
        private readonly IRepository _repository;

        public PhotoCategoryController(IDynamicExpressionQuery dynamicExpressionQuery,
                                       IRepository repository,
                                       ISaveEntityService saveEntityService,
                                       IEntityListGrid<PhotoCategory> listTypeListGrid)
            : base(
                dynamicExpressionQuery, repository, saveEntityService,
                listTypeListGrid)
        {
            _repository = repository;
        }

        public override ActionResult Display(ViewModel input)
        {
            var listType = _repository.Find<PhotoCategory>(input.EntityId);
            var model = new ListTypeViewModel
                            {
                                ListType = listType,
                                AddEditUrl =
                                    UrlContext.GetUrlForAction<PhotoCategoryController>(x => x.AddUpdate(null)) + "/" +
                                    input.EntityId
                            };
            return PartialView(model);
        }
    }
}