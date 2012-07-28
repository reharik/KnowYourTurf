using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FubuMVC.Core;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class 
        PhotoController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISelectListItemService _selectListItemService;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly ISessionContext _sessionContext;

        public PhotoController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISelectListItemService selectListItemService,
            IFileHandlerService fileHandlerService,
            ISessionContext sessionContext)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _selectListItemService = selectListItemService;
            _fileHandlerService = fileHandlerService;
            _sessionContext = sessionContext;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var photo = input.EntityId > 0 ? _repository.Find<Photo>(input.EntityId) : new Photo();
            var categoryItems = _selectListItemService.CreateList<PhotoCategory>(x=>x.Name,x=>x.EntityId,true);
            var model = new PhotoViewModel
            {
                Item = photo,
                PhotoCategoryList = categoryItems,
                _Title = WebLocalizationKeys.PHOTO_INFORMATION.ToString(),
                Popup = input.Popup
            };
            return View(model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var photo = _repository.Find<Photo>(input.EntityId);
            var model = new PhotoViewModel
                            {
                                Item = photo,
                                AddUpdateUrl = UrlContext.GetUrlForAction<PhotoController>(x => x.AddUpdate(null)) + "/" + photo.EntityId,
                                _Title = WebLocalizationKeys.PHOTO_INFORMATION.ToString()
                            };
            return View(model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var photo = _repository.Find<Photo>(input.EntityId);
            _repository.HardDelete(photo);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<Photo>(x);
                _fileHandlerService.DeleteFile(item.FileUrl);
                _repository.HardDelete(item);
            });
            _repository.Commit();
            return Json(new Notification { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(PhotoViewModel input)
        {
            var photo = input.Item.EntityId > 0 ? _repository.Find<Photo>(input.Item.EntityId) : new Photo();
            var newDoc = mapToDomain(input, photo);
            photo.FileUrl = _fileHandlerService.SaveAndReturnUrlForFile("CustomerPhotos");
            var crudManager = _saveEntityService.ProcessSave(newDoc);
            if (input.Var == "Field")
            {
                var field = _repository.Find<Field>(input.ParentId);
                field.AddPhoto(photo);
                crudManager = _saveEntityService.ProcessSave(field, crudManager);
            } 
            var notification = crudManager.Finish();
            notification.Variable = photo.FileUrl;
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private Photo mapToDomain(PhotoViewModel input, Photo photo)
        {
            var photoModel = input.Item;
            
            photo.Name = photoModel.Name;
            photo.Description = photoModel.Description;
            if (photo.PhotoCategory == null || photo.PhotoCategory.EntityId != input.PhotoCategory)
            {
                photo.PhotoCategory = _repository.Find<PhotoCategory>(input.Item.PhotoCategory.EntityId);
            }
            return photo;
        }
    }


    public class PhotoViewModel:ViewModel
    {
        public Photo Item { get; set; }
        public IEnumerable<SelectListItem> PhotoCategoryList { get; set; }
        public long PhotoCategory { get; set; }
    }
}