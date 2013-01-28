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
using KnowYourTurf.Web.Models.Material;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class MaterialController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        public MaterialController(IRepository repository,ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("MaterialAddUpdate", new MaterialViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var material = input.EntityId > 0 ? _repository.Find<Material>(input.EntityId) : new Material();
            var model = Mapper.Map<Material, MaterialViewModel>(material);
            model._Title = WebLocalizationKeys.MATERIAL_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<MaterialController>(x => x.Save(null));
            return new CustomJsonResult(model);
        }

        public ActionResult Display_Template(ViewModel input)
        {
            return View("MaterialView", new MaterialViewModel());
        }

        public ActionResult Display(ViewModel input)
        {
            var material =  _repository.Find<Material>(input.EntityId);
            var model = Mapper.Map<Material, MaterialViewModel>(material);
            model._Title = WebLocalizationKeys.MATERIAL_INFORMATION.ToString();
            return new CustomJsonResult(model);
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
            return new CustomJsonResult(notification);
        }
        private bool checkDependencies(Material item, Notification notification)
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

        public ActionResult Save(MaterialViewModel input)
        {
            var material = input.EntityId > 0 ? _repository.Find<Material>(input.EntityId) : new Material();
            mapItem(material, input);
            var crudManager = _saveEntityService.ProcessSave(material);
            var notification = crudManager.Finish();
            return new CustomJsonResult(notification);
        }

        private void mapItem(Material material, MaterialViewModel input)
        {
            material.Description = input.Description;
            material.Name = input.Name;
            material.Notes = input.Notes;
        }
    }
}