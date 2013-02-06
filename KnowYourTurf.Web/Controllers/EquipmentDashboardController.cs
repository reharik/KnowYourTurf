using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Domain.Persistence;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class EquipmentDashboardController : KYTController
    {
        private readonly IRepository _repository;
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<EquipmentTask> _pendingTaskGrid;
        private readonly IEntityListGrid<EquipmentTask> _completedTaskGrid;
        private readonly IEntityListGrid<Photo> _photoListGrid;
        private readonly IEntityListGrid<Document> _documentListGrid;
        private readonly ISelectListItemService _selectListItemService;

        public EquipmentDashboardController(IRepository repository,
                                        IDynamicExpressionQuery dynamicExpressionQuery,
                                        IEntityListGrid<Photo> photoListGrid,
                                        IEntityListGrid<Document> documentListGrid,
            ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _pendingTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<EquipmentTask>>("PendingEquipmentTasks");
            _completedTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<EquipmentTask>>("CompletedEquipmentTasks");
            _photoListGrid = photoListGrid;
            _documentListGrid = documentListGrid;
            _selectListItemService = selectListItemService;
        }

        public ActionResult ViewEquipment_Template(ViewModel input)
        {
            return View("EquipmentDashboard", new EquipmentViewModel());
        }

        public ActionResult ViewEquipment(ViewModel input)
        {
            var equipment = _repository.Find<Equipment>(input.EntityId);
            var url = UrlContext.GetUrlForAction<EquipmentDashboardController>(x => x.PendingTasksGrid(null)) + "?ParentId=" + input.EntityId;
            var completeUrl = UrlContext.GetUrlForAction<EquipmentDashboardController>(x => x.CompletedTasksGrid(null)) +"?ParentId=" + input.EntityId;
            var photoUrl = UrlContext.GetUrlForAction<EquipmentDashboardController>(x => x.PhotoGrid(null)) + "?ParentId=" + input.EntityId;
            var docuemntUrl = UrlContext.GetUrlForAction<EquipmentDashboardController>(x => x.DocumentGrid(null)) + "?ParentId=" + input.EntityId;
            var equipmentTypes = _selectListItemService.CreateList<EquipmentType>(x => x.Name, x => x.EntityId, true);
            var vendors = _selectListItemService.CreateList<EquipmentVendor>(x => x.Client, x => x.EntityId, true);
            
            var model = Mapper.Map<Equipment, EquipmentViewModel>(equipment);
            model._EquipmentTypeEntityIdList = equipmentTypes;
            model._EquipmentVendorEntityIdList = vendors;
            model._pendingGridUrl = url;
            model._completedGridUrl = completeUrl;
            model._documentGridUrl = docuemntUrl;
            model._photoGridUrl = photoUrl;
            model._saveUrl = UrlContext.GetUrlForAction<EquipmentController>(x => x.Save(null));
            model._Title = WebLocalizationKeys.EQUIPMENT_INFORMATION.ToString();
            model._Photos = equipment.Photos.Select(x => new PhotoDto {FileUrl = x.FileUrl});

            return new CustomJsonResult(model);
        }

        public ActionResult CompletedTasksGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EquipmentDashboardController>(x => x.CompletedTasks(null)) + "?ParentId=" + input.ParentId;
            ListViewModel model = new ListViewModel()
            {
                gridDef = _completedTaskGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId
            };
            return new CustomJsonResult(model);
        }
        public JsonResult CompletedTasks(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<EquipmentTask>(input.filters, x => x.Equipment.EntityId == input.ParentId && x.Complete);
            var gridItemsViewModel = _completedTaskGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }

        public ActionResult PendingTasksGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EquipmentDashboardController>(x => x.PendingTasks(null)) + "?ParentId=" + input.ParentId;
            ListViewModel model = new ListViewModel()
            {
                gridDef = _pendingTaskGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId
            };
            return new CustomJsonResult(model);
        }
        public JsonResult PendingTasks(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<EquipmentTask>(input.filters,
                                                                   x =>
                                                                   x.Equipment.EntityId == input.ParentId && !x.Complete);
            var gridItemsViewModel = _pendingTaskGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }

        public ActionResult PhotoGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EquipmentDashboardController>(x => x.Photos(null)) + "?ParentId=" + input.ParentId;
            var model = new ListViewModel()
            {
                gridDef = _photoListGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId,
                deleteMultipleUrl = UrlContext.GetUrlForAction<PhotoController>(x => x.DeleteMultiple(null))
            };
            model.headerButtons.Add("new");
            model.headerButtons.Add("delete");
            return new CustomJsonResult(model);
        }
        public JsonResult Photos(GridItemsRequestModel input)
        {
            var equipment = _repository.Find<Equipment>(input.ParentId);
            Expression<Func<Photo, bool>> photoWhereClause =
                _dynamicExpressionQuery.PrepareExpression<Photo>(input.filters);
            IEnumerable<Photo> items;
            if(photoWhereClause==null)
           {
               items = equipment.Photos;
           }
           else
           {
               items = equipment.Photos.Where(photoWhereClause.Compile());
           }
            var gridItemsViewModel = _photoListGrid.GetGridItemsViewModel(input.PageSortFilter, items.AsQueryable(), input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }

        public ActionResult DocumentGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<EquipmentDashboardController>(x => x.Documents(null)) + "?ParentId=" + input.ParentId;
            var model = new ListViewModel()
            {
                gridDef = _documentListGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId,
                deleteMultipleUrl = UrlContext.GetUrlForAction<DocumentController>(x => x.DeleteMultiple(null))
            };
            model.headerButtons.Add("new");
            model.headerButtons.Add("delete");
            return new CustomJsonResult(model);
        }
        public JsonResult Documents(GridItemsRequestModel input)
        {
            var equipment = _repository.Find<Equipment>(input.ParentId);
            var documentWhereClause = _dynamicExpressionQuery.PrepareExpression<Document>(input.filters);
            IEnumerable<Document> items;
            if (documentWhereClause == null)
            {
                items = equipment.Documents;
            }else
            {
                items = equipment.Documents.Where(documentWhereClause.Compile());
            }
            var gridItemsViewModel = _documentListGrid.GetGridItemsViewModel(input.PageSortFilter, items.AsQueryable(), input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }
    }

    
}