using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class PhotoController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISelectListItemService _selectListItemService;
        private readonly IUploadedFileHandlerService _uploadedFileHandlerService;
        private readonly ISessionContext _sessionContext;

        public PhotoController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISelectListItemService selectListItemService,
            IUploadedFileHandlerService uploadedFileHandlerService,
            ISessionContext sessionContext)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _selectListItemService = selectListItemService;
            _uploadedFileHandlerService = uploadedFileHandlerService;
            _sessionContext = sessionContext;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var photo = input.EntityId > 0 ? _repository.Find<Photo>(input.EntityId) : new Photo();
            var categoryItems = _selectListItemService.CreateList<PhotoCategory>(x=>x.Name,x=>x.EntityId,true);
            var model = new PhotoViewModel
            {
                Photo = photo,
                PhotoCategoryList = categoryItems,
                Title = WebLocalizationKeys.PHOTO_INFORMATION.ToString()
            };
            return View(model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var photo = _repository.Find<Photo>(input.EntityId);
            var model = new PhotoViewModel
                            {
                                Photo = photo,
                                AddEditUrl = UrlContext.GetUrlForAction<PhotoController>(x => x.AddUpdate(null)) + "/" + photo.EntityId,
                                Title = WebLocalizationKeys.PHOTO_INFORMATION.ToString()
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

        public ActionResult Save(PhotoViewModel input)
        {
            var coId = _sessionContext.GetCompanyId();
            var photo = input.Photo.EntityId > 0 ? _repository.Find<Photo>(input.Photo.EntityId) : new Photo();
            var newDoc = mapToDomain(input, photo);
            var serverDirectory = "/CustomerPhotos/" + coId;
            photo.FileUrl = _uploadedFileHandlerService.GetUploadedFileUrl(serverDirectory, photo.Name); 
            
            var crudManager = _saveEntityService.ProcessSave(newDoc);
            if (input.From == "Field")
            {
                var field = _repository.Find<Field>(input.ParentId);
                field.AddPhoto(photo);
                crudManager = _saveEntityService.ProcessSave(field, crudManager);
            } 
            crudManager = _uploadedFileHandlerService.SaveUploadedFile(serverDirectory, newDoc.Name, crudManager);
            
            var notification = crudManager.Finish();
            return Json(notification, "text/plain");
        }

        private Photo mapToDomain(PhotoViewModel input, Photo photo)
        {
            var photoModel = input.Photo;
            photo.FileType = photoModel.FileType;
            
            photo.Name = photoModel.Name;
            photo.Description = photoModel.Description;
            if (photo.PhotoCategory == null || photo.PhotoCategory.EntityId != input.PhotoCategory)
            {
                photo.PhotoCategory = _repository.Find<PhotoCategory>(input.Photo.PhotoCategory.EntityId);
            }
            return photo;
        }
    }


    public class PhotoViewModel:ViewModel
    {
        public Photo Photo { get; set; }

        public IEnumerable<SelectListItem> PhotoCategoryList { get; set; }

        public long PhotoCategory { get; set; }
    }
}