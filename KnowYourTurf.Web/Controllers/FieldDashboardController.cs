using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class FieldDashboardController : KYTController
    {
        private readonly IRepository _repository;
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Task> _pendingTaskGrid;
        private readonly IEntityListGrid<Task> _completedTaskGrid;
        private readonly IEntityListGrid<Photo> _photoListGrid;
        private readonly IEntityListGrid<Document> _documentListGrid;

        public FieldDashboardController(IRepository repository,
                                        IDynamicExpressionQuery dynamicExpressionQuery,
                                        IEntityListGrid<Photo> photoListGrid,
                                        IEntityListGrid<Document> documentListGrid)
        {
            _repository = repository;
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _pendingTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("PendingTasks");
            _completedTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("CompletedTasks");
            _photoListGrid = photoListGrid;
            _documentListGrid = documentListGrid;
        }

        public ActionResult ViewField(ViewModel input)
        {
            var field = _repository.Find<Field>(input.EntityId);
            var url = UrlContext.GetUrlForAction<FieldDashboardController>(x => x.PendingTasks(null)) + "?ParentId=" +
                      input.EntityId;
            var completeUrl = UrlContext.GetUrlForAction<FieldDashboardController>(x => x.CompletedTasks(null)) +
                              "?ParentId=" + input.EntityId + "&gridName=CompletedTaskGrid";
            var photoUrl = UrlContext.GetUrlForAction<FieldDashboardController>(x => x.Photos(null)) + "?ParentId=" +
                           input.EntityId + "&gridName=CompletedTaskGrid";
            var docuemntUrl = UrlContext.GetUrlForAction<FieldDashboardController>(x => x.Documents(null)) +
                              "?ParentId=" + input.EntityId + "&gridName=CompletedTaskGrid";
            var model = new FieldDashboardViewModel
                            {
                                EntityId = input.EntityId,
                                Field = field,
                                AddEditUrl =
                                    UrlContext.GetUrlForAction<TaskController>(x => x.AddEdit(null)) + "?ParentId=" +
                                    input.EntityId + "&From=Field",
                                AddEditPhotoUrl =
                                    UrlContext.GetUrlForAction<PhotoController>(x => x.AddUpdate(null)) + "?ParentId=" +
                                    input.EntityId + "&From=Field",
                                AddEditDocumentUrl =
                                    UrlContext.GetUrlForAction<DocumentController>(x => x.AddUpdate(null)) + "?ParentId=" +
                                    input.EntityId + "&From=Field",
                                ListDefinition =
                                    _pendingTaskGrid.GetGridDefinition(url),
                                CompletedListDefinition =
                                    _completedTaskGrid.GetGridDefinition(completeUrl),
                                DocumentListDefinition =
                                    _documentListGrid.GetGridDefinition(docuemntUrl),
                                PhotoListDefinition =
                                    _photoListGrid.GetGridDefinition(photoUrl),
                            };
            return View("FieldDashboard", model);
        }

        public JsonResult CompletedTasks(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters,
                                                                   x => x.Field.EntityId == input.ParentId && x.Complete);
            var gridItemsViewModel = _completedTaskGrid.GetGridItemsViewModel(input.PageSortFilter, items,
                                                                              "completeTaskGrid");
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PendingTasks(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters,
                                                                   x =>
                                                                   x.Field.EntityId == input.ParentId && !x.Complete);
            var gridItemsViewModel = _pendingTaskGrid.GetGridItemsViewModel(input.PageSortFilter, items,
                                                                            "pendingTaskGrid");
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Photos(GridItemsRequestModel input)
        {
            var field = _repository.Find<Field>(input.ParentId);
            Expression<Func<Photo, bool>> photoWhereClause =
                _dynamicExpressionQuery.PrepareExpression<Photo>(input.filters);
            IEnumerable<Photo> items;
            if(photoWhereClause==null)
           {
               items = field.Photos;
           }
           else
           {
               items = field.Photos.Where(photoWhereClause.Compile());
           }
            var gridItemsViewModel = _photoListGrid.GetGridItemsViewModel(input.PageSortFilter, items.AsQueryable(),
                                                                          "photoGrid");
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Documents(GridItemsRequestModel input)
        {
            var field = _repository.Find<Field>(input.ParentId);
            var documentWhereClause = _dynamicExpressionQuery.PrepareExpression<Document>(input.filters);
            IEnumerable<Document> items;
            if (documentWhereClause == null)
            {
                items = field.Documents;
            }else
            {
                items = field.Documents.Where(documentWhereClause.Compile());
            }
            var gridItemsViewModel = _documentListGrid.GetGridItemsViewModel(input.PageSortFilter, items.AsQueryable(),
                                                                          "documentGrid");
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}