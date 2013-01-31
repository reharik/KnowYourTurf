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
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class ChemicalController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        public ChemicalController(IRepository repository, ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("ChemicalAddUpdate", new ChemicalViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var chemical = input.EntityId > 0 ? _repository.Find<Chemical>(input.EntityId) : new Chemical();
            var model = Mapper.Map<Chemical, ChemicalViewModel>(chemical);
            model._Title = WebLocalizationKeys.CHEMICAL_INFORMATION.ToString();
            model._saveUrl = UrlContext.GetUrlForAction<ChemicalController>(x => x.Save(null));
            return new CustomJsonResult(model);
        }

        public ActionResult Display_Template(ViewModel input)
        {
            return View("ChemicalView", new ChemicalViewModel());
        }

        public ActionResult Display(ViewModel input)
        {
            var chemical = _repository.Find<Chemical>(input.EntityId);
            var model = Mapper.Map<Chemical, ChemicalViewModel>(chemical);
            model._Title = WebLocalizationKeys.CHEMICAL_INFORMATION.ToString();
            return new CustomJsonResult(model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var chemical = _repository.Find<Chemical>(input.EntityId);
            _repository.HardDelete(chemical);
            _repository.UnitOfWork.Commit();
            return null;
        }
        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var notification = new Notification { Success = true };
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<Chemical>(x);
                if (checkDependencies(item, notification))
                {
                    _repository.HardDelete(item);
                }
            });
            _repository.Commit();
            return new CustomJsonResult(notification);
        }
        private bool checkDependencies(Chemical item, Notification notification)
        {
            var dependantItems = _repository.Query<FieldVendor>(x => x.Products.Any(i=>i == item));
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

        public ActionResult Save(ChemicalViewModel input)
        {
            Chemical chemical = input.EntityId>0 ? _repository.Find<Chemical>(input.EntityId) : new Chemical();
            mapItem(chemical, input);
            var crudManager = _saveEntityService.ProcessSave(chemical);
            var notification = crudManager.Finish();
            return new CustomJsonResult(notification);
        }

        private void mapItem(Chemical chemical, ChemicalViewModel input)
        {
            chemical.ActiveIngredient = input.ActiveIngredient;
            chemical.ActiveIngredientPercent = input.ActiveIngredientPercent;
            chemical.Description = input.Description;
            chemical.EPAEstNumber = input.EPAEstNumber;
            chemical.EPARegNumber = input.EPARegNumber;
            chemical.Manufacturer = input.Manufacturer;
            chemical.Name = input.Name;
            chemical.Notes = input.Notes;
        }
    }
}