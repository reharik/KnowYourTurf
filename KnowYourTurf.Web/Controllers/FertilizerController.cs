using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models.Fertilizer;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class FertilizerController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Photo> _photoListGrid;
        private readonly IEntityListGrid<Document> _documentListGrid;

        public FertilizerController(IRepository repository, 
            ISaveEntityService saveEntityService,
               IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Photo> photoListGrid,
            IEntityListGrid<Document> documentListGrid)
        
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _photoListGrid = photoListGrid;
            _documentListGrid = documentListGrid;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("FertilizerAddUpdate", new FertilizerViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var fertilizer = input.EntityId > 0 ? _repository.Find<Fertilizer>(input.EntityId) : new Fertilizer();
            var photoUrl = UrlContext.GetUrlForAction<FertilizerController>(x => x.PhotoGrid(null)) + "?ParentId=" + input.EntityId;
            var docuemntUrl = UrlContext.GetUrlForAction<FertilizerController>(x => x.DocumentGrid(null)) + "?ParentId=" + input.EntityId;
            var model = Mapper.Map<Fertilizer, FertilizerViewModel>(fertilizer);
            model._Title = WebLocalizationKeys.FERTILIZER_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<FertilizerController>(x => x.Save(null));
            model._documentGridUrl = docuemntUrl;
            model._photoGridUrl = photoUrl;
            model._Photos = fertilizer.Photos.Select(x => new PhotoDto { FileUrl = x.FileUrl, ImageId = x.EntityId });
            model.Product = "Fertilizer";
            return new CustomJsonResult(model);
        }

        public ActionResult Display_Template(ViewModel input)
        {
            return View("FertilizerView", new FertilizerViewModel());
        }

        public ActionResult Display(ViewModel input)
        {
            var fertilizer = _repository.Find<Fertilizer>(input.EntityId);
            var model = Mapper.Map<Fertilizer, FertilizerViewModel>(fertilizer);
            model._Title = WebLocalizationKeys.FERTILIZER_INFORMATION.ToString();
            return new CustomJsonResult(model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var fertilizer = _repository.Find<Fertilizer>(input.EntityId);
            _repository.HardDelete(fertilizer);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = new Notification { Success = true };
            input.EntityIds.ForEachItem(x =>
            {
                var item = _repository.Find<Fertilizer>(x);
                if (checkDependencies(item, notification))
                {
                    _repository.HardDelete(item);
                }
            });
            _repository.Commit();
            return new CustomJsonResult(notification);
        }
        private bool checkDependencies(Fertilizer item, Notification notification)
        {
            var dependantItems = _repository.Query<FieldVendor>(x => x.Products.Any(i => i == item));
            if (dependantItems.Any())
            {
                if (notification.Message.IsEmpty())
                {
                    notification.Success = false;
                    notification.Message = WebLocalizationKeys.COULD_NOT_DELETE_FOR_VENDOR.ToString();
                }
                return false;
            }
            return true;
        }

        public ActionResult Save(FertilizerViewModel input)
        {
            Fertilizer fertilizer = input.EntityId > 0 ? _repository.Find<Fertilizer>(input.EntityId) : new Fertilizer();
            mapItem(fertilizer, input);
            var crudManager = _saveEntityService.ProcessSave(fertilizer);
            var notification = crudManager.Finish();
            return new CustomJsonResult(notification);
        }

        private void mapItem(Fertilizer fertilizer, FertilizerViewModel input)
        {
            fertilizer.Description = input.Description;
            fertilizer.K = input.K;
            fertilizer.N = input.N;
            fertilizer.Name = input.Name;
            fertilizer.Notes = input.Notes;
            fertilizer.P = input.P;
        }

        public ActionResult PhotoGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<FertilizerController>(x => x.Photos(null)) + "?ParentId=" + input.ParentId;
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
            var field = _repository.Find<Fertilizer>(input.ParentId);
            Expression<Func<Photo, bool>> photoWhereClause =
                _dynamicExpressionQuery.PrepareExpression<Photo>(input.filters);
            IEnumerable<Photo> items;
            if (photoWhereClause == null)
            {
                items = field.Photos;
            }
            else
            {
                items = field.Photos.Where(photoWhereClause.Compile());
            }
            var gridItemsViewModel = _photoListGrid.GetGridItemsViewModel(input.PageSortFilter, items.AsQueryable(), input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }

        public ActionResult DocumentGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<FertilizerController>(x => x.Documents(null)) + "?ParentId=" + input.ParentId;
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
            var field = _repository.Find<Fertilizer>(input.ParentId);
            var documentWhereClause = _dynamicExpressionQuery.PrepareExpression<Document>(input.filters);
            IEnumerable<Document> items;
            if (documentWhereClause == null)
            {
                items = field.Documents;
            }
            else
            {
                items = field.Documents.Where(documentWhereClause.Compile());
            }
            var gridItemsViewModel = _documentListGrid.GetGridItemsViewModel(input.PageSortFilter, items.AsQueryable(), input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }

    }
}