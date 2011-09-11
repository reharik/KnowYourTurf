using System;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class SeedController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        public SeedController(IRepository repository, ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult AddEdit(ViewModel input)
        {
            var seed = input.EntityId > 0 ? _repository.Find<Seed>(input.EntityId) : new Seed();
            var model = new SeedViewModel
            {
                Seed = seed
            };
            return PartialView("SeedAddUpdate", model);
        }

        public ActionResult Display(ViewModel input)
        {
            var seed = _repository.Find<Seed>(input.EntityId);
            var model = new SeedViewModel
            {
                Seed = seed,
                AddEditUrl = UrlContext.GetUrlForAction<SeedController>(x => x.AddEdit(null)) + "/" + seed.EntityId
            };
            return PartialView("SeedView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var seed = _repository.Find<Seed>(input.EntityId);
            _repository.HardDelete(seed);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(SeedViewModel input)
        {
            Seed seed = input.Seed.EntityId > 0 ? _repository.Find<Seed>(input.Seed.EntityId) : new Seed();
            mapItem(seed, input);
            var crudManager = _saveEntityService.ProcessSave(seed);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private void mapItem(Seed seed, SeedViewModel input)
        {
            seed.Description = input.Seed.Description;
            seed.Name = input.Seed.Name;
            seed.Notes = input.Seed.Notes;
        }
    }
}