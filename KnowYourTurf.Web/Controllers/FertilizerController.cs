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

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("FertilizerAddUpdate", new FertilizerViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var fertilizer = input.EntityId > 0 ? _repository.Find<Fertilizer>(input.EntityId) : new Fertilizer();
            var model = Mapper.Map<Fertilizer, FertilizerViewModel>(fertilizer);
            model._Title = WebLocalizationKeys.FERTILIZER_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<FertilizerController>(x => x.Save(null));
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Display_Template(ViewModel input)
        {
            return View("FertilizerView", new FertilizerViewModel());
        }

        public ActionResult Display(ViewModel input)
        {
            var fertilizer = _repository.Find<Fertilizer>(input.EntityId);
            var model = Mapper.Map<Fertilizer, FertilizerViewModel>(fertilizer);
            model._Title = WebLocalizationKeys.FERTILIZER_INFORMATION.ToString();
            return Json(model, JsonRequestBehavior.AllowGet);
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
            Fertilizer fertilizer = input.EntityId > 0 ? _repository.Find<Fertilizer>(input.EntityId) : new Fertilizer();
            mapItem(fertilizer, input);
            var crudManager = _saveEntityService.ProcessSave(fertilizer);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private void mapItem(Fertilizer fertilizer, FertilizerViewModel input)
        {
            fertilizer.Description = input.Description;
            fertilizer.K = input.K;
            fertilizer.N = input.N;
            fertilizer.Name = input.Name;
            fertilizer.Notes = input.Notes;
            fertilizer.P = input.P;
        }
    }
}