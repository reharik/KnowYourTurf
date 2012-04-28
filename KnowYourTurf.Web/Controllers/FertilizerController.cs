using System;
using System.Linq;
using System.Web.Mvc;
using FluentNHibernate.Utils;
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
                Item = fertilizer,
                Title = WebLocalizationKeys.FERTILIZER_INFORMATION.ToString()
            };
            return PartialView("FertilizerAddUpdate", model);
        }

        public ActionResult Display(ViewModel input)
        {
            var fertilizer = _repository.Find<Fertilizer>(input.EntityId);
            var model = new FertilizerViewModel
            {
                Item = fertilizer,
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
            var notification = new Notification { Success = true };
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<Fertilizer>(x);
                if (checkDependencies(item, notification))
                {
                    _repository.HardDelete(item);
                }
            });
            _repository.Commit();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }
        private bool checkDependencies(Fertilizer item, Notification notification)
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

        public ActionResult Save(FertilizerViewModel input)
        {
            Fertilizer fertilizer = input.Item.EntityId > 0 ? _repository.Find<Fertilizer>(input.Item.EntityId) : new Fertilizer();
            mapItem(fertilizer, input);
            var crudManager = _saveEntityService.ProcessSave(fertilizer);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private void mapItem(Fertilizer fertilizer, FertilizerViewModel input)
        {
            fertilizer.Description = input.Item.Description;
            fertilizer.K = input.Item.K;
            fertilizer.N = input.Item.N;
            fertilizer.Name = input.Item.Name;
            fertilizer.Notes = input.Item.Notes;
            fertilizer.P = input.Item.P;
        }
    }
}