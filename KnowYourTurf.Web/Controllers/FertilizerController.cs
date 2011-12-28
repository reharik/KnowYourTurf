using System;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models.Fertilizer;

namespace KnowYourTurf.Web.Controllers
{
    public class FertilizerController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        public FertilizerController(IRepository repository, ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var fertilizer = input.EntityId > 0 ? _repository.Find<Fertilizer>(input.EntityId) : new Fertilizer();
            var model = new FertilizerViewModel
            {
                Fertilizer = fertilizer,
                Title = WebLocalizationKeys.FERTILIZER_INFORMATION.ToString()
            };
            return PartialView("FertilizerAddUpdate", model);
        }

        public ActionResult Display(ViewModel input)
        {
            var fertilizer = _repository.Find<Fertilizer>(input.EntityId);
            var model = new FertilizerViewModel
            {
                Fertilizer = fertilizer,
                AddUpdateUrl = UrlContext.GetUrlForAction<FertilizerController>(x => x.AddUpdate(null)) + "/" + fertilizer.EntityId,
                Title = WebLocalizationKeys.FERTILIZER_INFORMATION.ToString()
            };
            return PartialView("FertilizerView", model);
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
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<Fertilizer>(x);
                _repository.HardDelete(item);
            });
            _repository.Commit();
            return Json(new Notification { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(FertilizerViewModel input)
        {
            Fertilizer fertilizer = input.Fertilizer.EntityId > 0 ? _repository.Find<Fertilizer>(input.Fertilizer.EntityId) : new Fertilizer();
            mapItem(fertilizer, input);
            var crudManager = _saveEntityService.ProcessSave(fertilizer);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private void mapItem(Fertilizer fertilizer, FertilizerViewModel input)
        {
            fertilizer.Description = input.Fertilizer.Description;
            fertilizer.K = input.Fertilizer.K;
            fertilizer.N = input.Fertilizer.N;
            fertilizer.Name = input.Fertilizer.Name;
            fertilizer.Notes = input.Fertilizer.Notes;
            fertilizer.P = input.Fertilizer.P;
        }
    }
}