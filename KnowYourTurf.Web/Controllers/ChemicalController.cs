using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class ChemicalController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        public ChemicalController(IRepository repository, ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var chemical = input.EntityId > 0 ? _repository.Find<Chemical>(input.EntityId) : new Chemical();
            var model = new ChemicalViewModel
            {
                Chemical = chemical,
                Title = WebLocalizationKeys.CHEMICAL_INFORMATION.ToString()
            };
            return PartialView("ChemicalAddUpdate", model);
        }

        public ActionResult Display(ViewModel input)
        {
            var chemical = _repository.Find<Chemical>(input.EntityId);
            var model = new ChemicalViewModel
            {
                Chemical = chemical,
                AddUpdateUrl = UrlContext.GetUrlForAction<ChemicalController>(x => x.AddUpdate(null)) + "/" + chemical.EntityId,
                Title = WebLocalizationKeys.CHEMICAL_INFORMATION.ToString()
            };
            return PartialView("ChemicalView", model);
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
            return Json(notification, JsonRequestBehavior.AllowGet);
        }
        private bool checkDependencies(Chemical item, Notification notification)
        {
            var dependantItems = _repository.Query<Vendor>(x => x.Products.Any(i=>i == item));
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
            Chemical chemical = input.Chemical.EntityId>0 ? _repository.Find<Chemical>(input.Chemical.EntityId) : new Chemical();
            mapItem(chemical, input);
            var crudManager = _saveEntityService.ProcessSave(chemical);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private void mapItem(Chemical chemical, ChemicalViewModel input)
        {
            chemical.ActiveIngredient = input.Chemical.ActiveIngredient;
            chemical.ActiveIngredientPercent = input.Chemical.ActiveIngredientPercent;
            chemical.Description = input.Chemical.Description;
            chemical.EPAEstNumber = input.Chemical.EPAEstNumber;
            chemical.EPARegNumber = input.Chemical.EPARegNumber;
            chemical.Manufacturer = input.Chemical.Manufacturer;
            chemical.Name = input.Chemical.Name;
            chemical.Notes = input.Chemical.Notes;
        }
    }
}