using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using FluentNHibernate.Utils;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
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

        public JsonResult ListTypes(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<LISTTYPE>(input.filters);
            var gridItemsViewModel = _listTypeListGrid.GetGridItemsViewModel(input.PageSortFilter, items,input.User);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }

        protected LISTTYPE getListType(ViewModel input)
        {
            var listType = input.EntityId > 0
                               ? _repository.Find<LISTTYPE>(input.EntityId)
                               : Activator.CreateInstance<LISTTYPE>();
            return listType;
        }

        public virtual ActionResult SaveListType(ListTypeViewModel input)
        {
            var listType = mapListType(input);
            var notification = saveListType(listType);
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public LISTTYPE mapListType(ListTypeViewModel input)
        {
            var listType = input.EntityId > 0
                   ? _repository.Find<LISTTYPE>(input.EntityId)
                   : Activator.CreateInstance<LISTTYPE>();
            listType.Description = input.Description;
            listType.Name = input.Name;
            listType.Status = input.Status;
            return listType;
        }

        protected Notification saveListType(LISTTYPE listType)
        {
            var crudManager = _saveEntityService.ProcessSave(listType);
            var notification = crudManager.Finish();
            notification.Variable = typeof(LISTTYPE).Name;
            return notification;
        }

        protected Notification deleteMultiple(BulkActionViewModel input, Func<LISTTYPE, Notification, bool> checkDependencies)
        {
            var notification = new Notification {Success = true};
            input.EntityIds.Each(x =>
                                     {
                                         var item = _repository.Find<LISTTYPE>(x);
                                         if (checkDependencies(item, notification))
                                         {
                                             _repository.HardDelete(item);
                                         }
                                     });
            _repository.Commit();
            return notification;
        }

    }

    public class EventTypeController : ListTypeBaseController<EventType>
    {
        private readonly IRepository _repository;
        private readonly ISelectListItemService _selectListItemService;

        public EventTypeController(IDynamicExpressionQuery dynamicExpressionQuery,
                                   IRepository repository,
                                   ISaveEntityService saveEntityService,
                                   ISelectListItemService selectListItemService,
                                   IEntityListGrid<EventType> listTypeListGrid)
            : base(dynamicExpressionQuery, repository, saveEntityService, listTypeListGrid)
        {
            _repository = repository;
            _selectListItemService = selectListItemService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("AddUpdate",new EventTypeViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var listType = getListType(input);
            var model = Mapper.Map<EventType, EventTypeViewModel>(listType);
            model._Title = WebLocalizationKeys.EVENT_TYPE_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<EventTypeController>(x => x.SaveEventType(null));
            model._StatusList = _selectListItemService.CreateList<Status>();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = deleteMultiple(input, checkDependencies); 
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        protected virtual bool checkDependencies(EventType item, Notification notification)
        {
            var dependantItems = _repository.Query<Field>(x => x.Events.Any(y=>y.EventType== item));
            if (dependantItems.Any())
            {
                if (notification.Message.IsEmpty())
                {
                    notification.Success = false;
                    notification.Message = WebLocalizationKeys.COULD_NOT_DELETE_EVENTTYPE.ToString();
                }
                return false;
            }
            return true;
        }


        public ActionResult SaveEventType(EventTypeViewModel input)
        {
            var listType = mapListType(input);
            listType.EventColor = input.EventColor;
            var notification = saveListType(listType);
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

    }

    public class TaskTypeController : ListTypeBaseController<TaskType>
    {
        private readonly IRepository _repository;
        private readonly ISelectListItemService _selectListItemService;

        public TaskTypeController(IDynamicExpressionQuery dynamicExpressionQuery,
                                  IRepository repository,
                                  ISaveEntityService saveEntityService,
                                  ISelectListItemService selectListItemService,
                                  IEntityListGrid<TaskType> listTypeListGrid)
            : base(dynamicExpressionQuery, repository, saveEntityService, listTypeListGrid
                )
        {
            _repository = repository;
            _selectListItemService = selectListItemService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("AddUpdate", new ListTypeViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var listType = getListType(input);
            var model = Mapper.Map<TaskType, ListTypeViewModel>(listType);
            model._saveUrl = UrlContext.GetUrlForAction<TaskTypeController>(x => x.SaveTaskType(null));
            model._Title = WebLocalizationKeys.TASK_TYPE_INFORMATION.ToString();
            model._StatusList = _selectListItemService.CreateList<Status>();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = deleteMultiple(input, checkDependencies);
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        protected virtual bool checkDependencies(TaskType item, Notification notification)
        {
            var dependantItems = _repository.Query<Task>(x => x.TaskType == item);
            if (dependantItems.Any())
            {
                if (notification.Message.IsEmpty())
                {
                    notification.Success = false;
                    notification.Message = WebLocalizationKeys.COULD_NOT_DELETE_FOR_TASK.ToString();
                }
                return false;
            }
            return true;
        }

        public ActionResult SaveTaskType(ListTypeViewModel input)
        {
            var listType = mapListType(input);
            var notification = saveListType(listType);
            return Json(notification, JsonRequestBehavior.AllowGet);
        }
    }

    public class DocumentCategoryController : ListTypeBaseController<DocumentCategory>
    {
        private readonly IRepository _repository;
        private readonly ISelectListItemService _selectListItemService;

        public DocumentCategoryController(IDynamicExpressionQuery dynamicExpressionQuery,
                                          IRepository repository,
                                          ISelectListItemService selectListItemService,
                                          ISaveEntityService saveEntityService,
                                          IEntityListGrid<DocumentCategory> listTypeListGrid)
            : base(
                dynamicExpressionQuery, repository, saveEntityService,
                listTypeListGrid)
        {
            _repository = repository;
            _selectListItemService = selectListItemService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("AddUpdate", new ListTypeViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var listType = getListType(input);
            var model = Mapper.Map<DocumentCategory, ListTypeViewModel>(listType);
            model._saveUrl = UrlContext.GetUrlForAction<DocumentCategoryController>(x => x.SaveListType(null));
            model._Title = WebLocalizationKeys.DOCUMENT_CATEGORY_INFORMATION.ToString();
            model._StatusList = _selectListItemService.CreateList<Status>();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = deleteMultiple(input, checkDependencies);
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        protected virtual bool checkDependencies(DocumentCategory item, Notification notification)
        {
            var dependantItems = _repository.Query<Document>(x => x.DocumentCategory== item);
            if (dependantItems.Any())
            {
                if (notification.Message.IsEmpty())
                {
                    notification.Success = false;
                    notification.Message = WebLocalizationKeys.COULD_NOT_DELETE_DOCUMENTCATEGORY.ToString();
                }
                return false;
            }
            return true;
        }

    }

    public class PhotoCategoryController : ListTypeBaseController<PhotoCategory>
    {
        private readonly IRepository _repository;
        private readonly ISelectListItemService _selectListItemService;

        public PhotoCategoryController(IDynamicExpressionQuery dynamicExpressionQuery,
                                       IRepository repository,
                                       ISelectListItemService selectListItemService,
                                       ISaveEntityService saveEntityService,
                                       IEntityListGrid<PhotoCategory> listTypeListGrid)
            : base(
                dynamicExpressionQuery, repository, saveEntityService,
                listTypeListGrid)
        {
            _repository = repository;
            _selectListItemService = selectListItemService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("AddUpdate", new ListTypeViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var listType = getListType(input);
            var model = Mapper.Map<PhotoCategory, ListTypeViewModel>(listType);
            model._saveUrl = UrlContext.GetUrlForAction<PhotoCategoryController>(x => x.SaveListType(null));
            model._Title = WebLocalizationKeys.PHOTO_CATEGORY_INFORMATION.ToString();
            model._StatusList = _selectListItemService.CreateList<Status>();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = deleteMultiple(input, checkDependencies);
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        protected virtual  bool checkDependencies(PhotoCategory item, Notification notification)
        {
            var dependantItems = _repository.Query<Photo>(x=>x.PhotoCategory==item);
            if (dependantItems.Any())
            {
                if (notification.Message.IsEmpty())
                {
                    notification.Success = false;
                    notification.Message = WebLocalizationKeys.COULD_NOT_DELETE_PHOTOCATEGORY.ToString();
                }
                return false;
            }
            return true;
        }
    }

    public class EquipmentTaskTypeController : ListTypeBaseController<EquipmentTaskType>
    {
        private readonly IRepository _repository;
        private readonly ISelectListItemService _selectListItemService;

        public EquipmentTaskTypeController(IDynamicExpressionQuery dynamicExpressionQuery,
                                   IRepository repository,
                                   ISaveEntityService saveEntityService,
                                   ISelectListItemService selectListItemService,
                                   IEntityListGrid<EquipmentTaskType> listTypeListGrid)
            : base(dynamicExpressionQuery, repository, saveEntityService, listTypeListGrid)
        {
            _repository = repository;
            _selectListItemService = selectListItemService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("AddUpdate", new ListTypeViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var listType = getListType(input);
            var model = Mapper.Map<EquipmentTaskType, ListTypeViewModel>(listType);
            model._Title = WebLocalizationKeys.EQUIPMENT_TASK_TYPE_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<EquipmentTaskTypeController>(x => x.SaveListType(null));
            model._StatusList = _selectListItemService.CreateList<Status>();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = deleteMultiple(input, checkDependencies);
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        protected virtual bool checkDependencies(EquipmentTaskType item, Notification notification)
        {
            var dependantItems = _repository.Query<Equipment>(x => x.Tasks.Any(y => y.TaskType == item));
            if (dependantItems.Any())
            {
                if (notification.Message.IsEmpty())
                {
                    notification.Success = false;
                    notification.Message = WebLocalizationKeys.COULD_NOT_DELETE_EQUIPMENTTASKTYPE.ToString();
                }
                return false;
            }
            return true;
        }
    }

    public class EquipmentTypeController : ListTypeBaseController<EquipmentType>
    {
        private readonly IRepository _repository;
        private readonly ISelectListItemService _selectListItemService;

        public EquipmentTypeController(IDynamicExpressionQuery dynamicExpressionQuery,
                                   IRepository repository,
                                   ISaveEntityService saveEntityService,
                                   ISelectListItemService selectListItemService,
                                   IEntityListGrid<EquipmentType> listTypeListGrid)
            : base(dynamicExpressionQuery, repository, saveEntityService, listTypeListGrid)
        {
            _repository = repository;
            _selectListItemService = selectListItemService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("AddUpdate", new ListTypeViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var listType = getListType(input);
            var model = Mapper.Map<EquipmentType, ListTypeViewModel>(listType);
            model._Title = WebLocalizationKeys.EQUIPMENT_TYPE_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<EquipmentTypeController>(x => x.SaveListType(null));
            model._StatusList = _selectListItemService.CreateList<Status>();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = deleteMultiple(input, checkDependencies);
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        protected virtual bool checkDependencies(EquipmentType item, Notification notification)
        {
            var dependantItems = _repository.Query<Equipment>(x => x.Tasks.Any(y => y.TaskType == item));
            if (dependantItems.Any())
            {
                if (notification.Message.IsEmpty())
                {
                    notification.Success = false;
                    notification.Message = WebLocalizationKeys.COULD_NOT_DELETE_EQUIPMENTTYPE.ToString();
                }
                return false;
            }
            return true;
        }
    }

    public class PartController : ListTypeBaseController<Part>
    {
        private readonly IRepository _repository;
        private readonly ISelectListItemService _selectListItemService;

        public PartController(IDynamicExpressionQuery dynamicExpressionQuery,
                                   IRepository repository,
                                   ISaveEntityService saveEntityService,
                                   ISelectListItemService selectListItemService,
                                   IEntityListGrid<Part> listTypeListGrid)
            : base(dynamicExpressionQuery, repository, saveEntityService, listTypeListGrid)
        {
            _repository = repository;
            _selectListItemService = selectListItemService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("AddUpdate", new PartViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var listType = getListType(input);
            var model = Mapper.Map<Part, PartViewModel>(listType);
            model._Title = WebLocalizationKeys.PART_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<PartController>(x => x.SavePart(null));
            model._StatusList = _selectListItemService.CreateList<Status>();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = deleteMultiple(input, checkDependencies);
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        protected virtual bool checkDependencies(Part item, Notification notification)
        {
            var dependantItems = _repository.Query<EquipmentTask>(x => x.Parts.Any(y => y == item));
            if (dependantItems.Any())
            {
                if (notification.Message.IsEmpty())
                {
                    notification.Success = false;
                    notification.Message = WebLocalizationKeys.COULD_NOT_DELETE_PART.ToString();
                }
                return false;
            }
            return true;
        }


        public ActionResult SavePart(PartViewModel input)
        {
            var listType = mapListType(input);
            listType.Vendor = input.Vendor;
            listType.FileUrl = input.FileUrl;
            var notification = saveListType(listType);
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

    }
}