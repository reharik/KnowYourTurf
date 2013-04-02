using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CC.Core;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.CustomAttributes;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Services;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

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
            model.Var = input.Var;
            return new CustomJsonResult(model);
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var methodInfo = typeof(Repository).GetMethod("Find");
            var type = typeof(Photo).Assembly.GetType("KnowYourTurf.Core.Domain." + input.Var);
            var genericMethod = methodInfo.MakeGenericMethod(new[] { type });
            dynamic entity = genericMethod.Invoke(_repository, new[] { (object)input.ParentId });
            var photoUrls = new List<string>();
            input.EntityIds.ForEachItem(x =>
            {
                var photo = ((IEnumerable<Photo>)entity.Photos).FirstOrDefault(y => y.EntityId == x);
                photoUrls.Add(photo.FileUrl);
                entity.RemovePhoto(photo);
            });
            var notification = _saveEntityService.ProcessSave(entity).Finish();
            if(notification.Success)
            {
                photoUrls.ForEachItem(_fileHandlerService.DeleteFile);
            }
            return new CustomJsonResult(notification);
        }

        public ActionResult Save(PhotoViewModel input)
        {
            var methodInfo = typeof (Repository).GetMethod("Find");
            var type = typeof (Photo).Assembly.GetType("KnowYourTurf.Core.Domain." + input.Var);
            var genericMethod = methodInfo.MakeGenericMethod(new[] {type});
            dynamic entity = genericMethod.Invoke(_repository, new[] {(object) input.ParentId});

            var photo = ((IEnumerable<Photo>)entity.Photos).FirstOrDefault(x => x.EntityId == input.EntityId) ?? new Photo();
            photo = mapToDomain(input, photo);
            photo.FileUrl = _fileHandlerService.SaveAndReturnUrlForFile("CustomerPhotos", entity.ClientId)??photo.FileUrl;
            entity.AddPhoto(photo);
            var crudManager = _saveEntityService.ProcessSave(entity);
            var notification = crudManager.Finish();
            notification.Payload = new PhotoDto {FileUrl = photo.FileUrl, ImageId = photo.EntityId};
            return new CustomJsonResult(notification);
        }

        private Photo mapToDomain(PhotoViewModel input, Photo photo)
        {
            photo.Name = input.Name;
            photo.Description = input.Description;
            if (input.PhotoCategoryEntityId==0)
            {
                photo.PhotoCategory = null;
            }else if (photo.PhotoCategory == null || photo.PhotoCategory.EntityId != input.PhotoCategoryEntityId)
            {
                photo.PhotoCategory = _repository.Find<PhotoCategory>(input.PhotoCategoryEntityId);
            }
            return photo;
        }
    }


    public class PhotoViewModel:ViewModel
    {
        public IEnumerable<SelectListItem> _PhotoCategoryEntityIdList { get; set; }
        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        [TextArea]
        public virtual string Description { get; set; }
        public virtual int PhotoCategoryEntityId { get; set; }
        [ValidateFileNotEmpty]
        public virtual string FileUrl { get; set; }

        public string _saveUrl { get; set; }
    }
}