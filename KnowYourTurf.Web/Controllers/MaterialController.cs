using System;
using System.Web.Mvc;
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

        public ActionResult AddEdit(ViewModel input)
        {
            var material = input.EntityId > 0 ? _repository.Find<Material>(input.EntityId) : new Material();
            var model = new MaterialViewModel
            {
                Material = material
            };
            return PartialView("MaterialAddUpdate", model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var material = _repository.Find<Material>(input.EntityId);
            var model = new MaterialViewModel
                            {
                                Material = material,
                                AddEditUrl = UrlContext.GetUrlForAction<MaterialController>(x => x.AddEdit(null)) + "/" + material.EntityId
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

        public ActionResult Save(MaterialViewModel input)
        {
            var material = input.Material.EntityId > 0 ? _repository.Find<Material>(input.Material.EntityId) : new Material();
            mapItem(material, input);
            var crudManager = _saveEntityService.ProcessSave(material);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private void mapItem(Material material, MaterialViewModel input)
        {
            material.Description = input.Material.Description;
            material.Name = input.Material.Name;
            material.Notes = input.Material.Name;
        }
    }
}