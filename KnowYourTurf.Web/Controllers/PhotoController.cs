using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class PhotoController:KYTController
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

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("AddUpdate", new PhotoViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var photo = input.EntityId > 0 ? _repository.Find<Photo>(input.EntityId) : new Photo();
            var categoryItems = _selectListItemService.CreateList<PhotoCategory>(x => x.Name, x => x.EntityId, true);
            var model = Mapper.Map<Photo, PhotoViewModel>(photo);
            model._PhotoCategoryEntityIdList = categoryItems;
            model._Title = WebLocalizationKeys.PHOTO_INFORMATION.ToString();
            model.Popup = input.Popup;
            model._saveUrl = UrlContext.GetUrlForAction<PhotoController>(x => x.Save(null));
            return Json(model, JsonRequestBehavior.AllowGet);
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
            input.EntityIds.ForEachItem(x =>
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
            var field = _repository.Find<Field>(input.ParentId);
            var photo = field.Photos.FirstOrDefault(x => x.EntityId == input.EntityId) ?? new Photo();
            photo = mapToDomain(input, photo);
            photo.FileUrl = _fileHandlerService.SaveAndReturnUrlForFile("CustomerPhotos");
            field.AddPhoto(photo);
            var crudManager = _saveEntityService.ProcessSave(field);
            var notification = crudManager.Finish();
            notification.Variable = photo.FileUrl;
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private Photo mapToDomain(PhotoViewModel input, Photo photo)
        {
            photo.Name = input.Name;
            photo.Description = input.Description;
            if (photo.PhotoCategory == null || photo.PhotoCategory.EntityId != input.PhotoCategoryEntityId)
            {
                photo.PhotoCategory = _repository.Find<PhotoCategory>(input.PhotoCategoryEntityId);
            }
            return photo;
        }
    }


    public class PhotoViewModel:ViewModel
    {
        public IEnumerable<SelectListItem> _PhotoCategoryEntityIdList { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int PhotoCategoryEntityId { get; set; }
        public virtual string FileUrl { get; set; }

        public string _saveUrl { get; set; }
    }
}