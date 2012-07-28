using System;
using System.Linq;
using System.Web.Mvc;
using FluentNHibernate.Utils;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
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

        public virtual ListTypeViewModel GetAddUpdate(ViewModel input)
        {
            var listType = input.EntityId > 0
                               ? _repository.Find<LISTTYPE>(input.EntityId)
                               : Activator.CreateInstance<LISTTYPE>();
            var model = new ListTypeViewModel
                            {
                                Item = listType,
                            };
            return model;
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
                                Item = listType,
                            };
            return PartialView(model);
        }


        public virtual ActionResult SaveListType(ListTypeViewModel input)
        {
            var listType = input.Item.EntityId > 0
                               ? _repository.Find<LISTTYPE>(input.Item.EntityId)
                               : Activator.CreateInstance<LISTTYPE>();
            listType.Description = input.Item.Description;
            listType.Name = input.Item.Name;
            listType.Status = input.Item.Status;
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

        public ActionResult AddUpdate(ViewModel input)
        {
            var listType = input.EntityId > 0
                               ? _repository.Find<EventType>(input.EntityId)
                               : Activator.CreateInstance<EventType>();
            var model = new EventTypeViewModel
                            {
                                Item = listType,
                                _Title = WebLocalizationKeys.EVENT_TYPE_INFORMATION.ToString()

                            };
            return PartialView(model);
        }

        public override ActionResult Display(ViewModel input)
        {
            var listType = _repository.Find<EventType>(input.EntityId);
            var model = new ListTypeViewModel
                            {
                                Item = listType,
                                AddUpdateUrl =
                                    UrlContext.GetUrlForAction<EventTypeController>(x => x.AddUpdate(null)) + "/" +
                                    input.EntityId,
                                _Title = WebLocalizationKeys.EVENT_TYPE_INFORMATION.ToString()
                            };
            return PartialView(model);
        }
        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = new Notification { Success = true };
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<EventType>(x);
                if (checkDependencies(item, notification))
                {
                    _repository.HardDelete(item);
                }
            });
            _repository.Commit();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }
        private bool checkDependencies(EventType item, Notification notification)
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
            var listType = input.Item.EntityId > 0
                               ? _repository.Find<EventType>(input.Item.EntityId)
                               : Activator.CreateInstance<EventType>();
            listType.Description = input.Item.Description;
            listType.Name = input.Item.Name;
            listType.Status = input.Item.Status;
            listType.EventColor = input.Item.EventColor;
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

        public virtual ActionResult AddUpdate(ViewModel input)
        {
            var model = GetAddUpdate(input);
            model._Title = WebLocalizationKeys.TASK_TYPE_INFORMATION.ToString();
            return PartialView(model);
        }
        public override ActionResult Display(ViewModel input)
        {
            var listType = _repository.Find<TaskType>(input.EntityId);
            var model = new ListTypeViewModel
                            {
                                Item = listType,
                                AddUpdateUrl =
                                    UrlContext.GetUrlForAction<TaskTypeController>(x => x.AddUpdate(null)) + "/" +
                                    input.EntityId,
                                _Title = WebLocalizationKeys.TASK_TYPE_INFORMATION.ToString()
                            };
            return PartialView(model);
        }
        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = new Notification { Success = true };
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<TaskType>(x);
                if (checkDependencies(item, notification))
                {
                    _repository.HardDelete(item);
                }
            });
            _repository.Commit();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }
        private bool checkDependencies(TaskType item, Notification notification)
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

        public virtual ActionResult AddUpdate(ViewModel input)
        {
            var model = GetAddUpdate(input);
            model._Title = WebLocalizationKeys.DOCUMENT_CATEGORY_INFORMATION.ToString();
            return PartialView(model);
        }

        public override ActionResult Display(ViewModel input)
        {
            var listType = _repository.Find<DocumentCategory>(input.EntityId);
            var model = new ListTypeViewModel
                            {
                                Item = listType,
                                AddUpdateUrl =
                                    UrlContext.GetUrlForAction<DocumentCategoryController>(x => x.AddUpdate(null)) + "/" +
                                    input.EntityId,
                                _Title = WebLocalizationKeys.DOCUMENT_CATEGORY_INFORMATION.ToString()
                            };
            return PartialView(model);
        }
        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = new Notification { Success = true };
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<DocumentCategory>(x);
                if (checkDependencies(item, notification))
                {
                    _repository.HardDelete(item);
                }
            });
            _repository.Commit();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }
        private bool checkDependencies(DocumentCategory item, Notification notification)
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

        public virtual ActionResult AddUpdate(ViewModel input)
        {
            var model = GetAddUpdate(input);
            model._Title = WebLocalizationKeys.PHOTO_CATEGORY_INFORMATION.ToString();
            return PartialView(model);
        }

        public override ActionResult Display(ViewModel input)
        {
            var listType = _repository.Find<PhotoCategory>(input.EntityId);
            var model = new ListTypeViewModel
                            {
                                Item = listType,
                                AddUpdateUrl =
                                    UrlContext.GetUrlForAction<PhotoCategoryController>(x => x.AddUpdate(null)) + "/" +
                                    input.EntityId,
                                _Title = WebLocalizationKeys.PHOTO_CATEGORY_INFORMATION.ToString()
                            };
            return PartialView(model);
        }
        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = new Notification { Success = true };
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<PhotoCategory>(x);
                if (checkDependencies(item, notification))
                {
                    _repository.HardDelete(item);
                }
            });
            _repository.Commit();
            return Json(notification, JsonRequestBehavior.AllowGet);


        }

        private bool checkDependencies(PhotoCategory item, Notification notification)
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
}