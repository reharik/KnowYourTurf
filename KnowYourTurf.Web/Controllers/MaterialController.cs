using System;
using System.Linq;
using System.Web.Mvc;
using FluentNHibernate.Utils;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models.Material;

namespace KnowYourTurf.Web.Controllers
{
    public class MaterialController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        public MaterialController(IRepository repository,ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var material = input.EntityId > 0 ? _repository.Find<Material>(input.EntityId) : new Material();
            var model = new MaterialViewModel
            {
                Item = material,
                _Title = WebLocalizationKeys.MATERIAL_INFORMATION.ToString()
            };
            return PartialView("MaterialAddUpdate", model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var material = _repository.Find<Material>(input.EntityId);
            var model = new MaterialViewModel
                            {
                                Item = material,
                                AddUpdateUrl = UrlContext.GetUrlForAction<MaterialController>(x => x.AddUpdate(null)) + "/" + material.EntityId,
                                _Title = WebLocalizationKeys.MATERIAL_INFORMATION.ToString()
                            };
            return PartialView("MaterialView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var material = _repository.Find<Material>(input.EntityId);
            _repository.HardDelete(material);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = new Notification { Success = true };
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<Material>(x);
                if (checkDependencies(item, notification))
                {
                    _repository.HardDelete(item);
                }
            });
            _repository.Commit();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }
        private bool checkDependencies(Material item, Notification notification)
        {
            var dependantItems = _repository.Query<Vendor>(x => x.Products.Any(i => i == item));
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

        public ActionResult Save(MaterialViewModel input)
        {
            var material = input.Item.EntityId > 0 ? _repository.Find<Material>(input.Item.EntityId) : new Material();
            mapItem(material, input);
            var crudManager = _saveEntityService.ProcessSave(material);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private void mapItem(Material material, MaterialViewModel input)
        {
            material.Description = input.Item.Description;
            material.Name = input.Item.Name;
            material.Notes = input.Item.Name;
        }
    }
}