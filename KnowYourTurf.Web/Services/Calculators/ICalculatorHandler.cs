using System;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;
using KnowYourTurf.Web.Models;
using NHibernate.Linq;
using System.Linq;

namespace KnowYourTurf.Web.Services
{
    public interface ICalculatorHandler
    {
        CalculatorViewModel GetViewModel();
        Continuation Calculate(SuperInputCalcViewModel input);
        CalculatorViewModel EmptyViewModel();
    }

    public class FertilizerNeededCalculator : ICalculatorHandler
    {
        private readonly IRepository _repository;
        private readonly IUnitSizeTimesQuantyCalculator _unitSizeTimesQuantyCalculator;
        private readonly ISelectListItemService _selectListItemService;

        public FertilizerNeededCalculator(IRepository repository, IUnitSizeTimesQuantyCalculator unitSizeTimesQuantyCalculator, ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _unitSizeTimesQuantyCalculator = unitSizeTimesQuantyCalculator;
            _selectListItemService = selectListItemService;
        }

        public CalculatorViewModel EmptyViewModel()
        {
            return new FertilzierNeededCalcViewModel();
        }

        public CalculatorViewModel GetViewModel()
        {
            var products = _repository.Query<InventoryProduct>(x => x.Product.InstantiatingType == "Fertilizer");
            var productItems = _selectListItemService.CreateListWithConcatinatedText(products, x => x.Product.Name, x => x.UnitType, " --> ", x => x.EntityId, true);
            var fieldItems = _selectListItemService.CreateFieldsSelectListItems();
            return new FertilzierNeededCalcViewModel
            {
                _FieldEntityIdList = fieldItems,
                _ProductEntityIdList = productItems,
                _calculatorDisplayName = WebLocalizationKeys.FERTILIZER_NEEDED_DISPLAY.ToString(),
                _calculatorName = WebLocalizationKeys.FERTILIZER_NEEDED.ToString(),
                _calculateUrl =UrlContext.GetUrlForAction<CalculatorController>(x=>x.Calculate(null)),
            };
        }

        public Continuation Calculate(SuperInputCalcViewModel input)
        {
            var continuation = new Continuation();
            var model = new FertilzierNeededCalcViewModel();
            var field = _repository.Find<Field>(input.FieldEntityId);
            var inventoryProduct = _repository.Query<InventoryProduct>(x=>x.EntityId == input.ProductEntityId).Fetch(x=>x.Product).FirstOrDefault();
            decimal bagSizeInPounds = _unitSizeTimesQuantyCalculator.CalculateLbsPerUnit(inventoryProduct);
            double fertN = ((Fertilizer)inventoryProduct.Product).N;
            double fertP = ((Fertilizer)inventoryProduct.Product).P;
            double fertK = ((Fertilizer)inventoryProduct.Product).K;
            double? N = input.FertilizerRate / (fertN * .01) * field.Size * .001 / Convert.ToDouble(bagSizeInPounds) * Convert.ToDouble(bagSizeInPounds) * fertN / (field.Size * .001) * .01;
            double? P = input.FertilizerRate/(fertN*.01)*field.Size/1000*fertP*.44*.01/(field.Size/1000);
            double? K = input.FertilizerRate/(fertN*.01)*field.Size/1000*fertK*.83*.01/(field.Size/1000);
            double? bagsNeeded = input.FertilizerRate/(fertN*.01)*field.Size/1000/Convert.ToDouble(bagSizeInPounds);

            model.N = Convert.ToDouble(Math.Round(Convert.ToDecimal(N), 2));
            model.P = Convert.ToDouble(Math.Round(Convert.ToDecimal(P), 2));
            model.K = Convert.ToDouble(Math.Round(Convert.ToDecimal(K), 2));
            model.BagsNeeded = Convert.ToDouble(Math.Round(Convert.ToDecimal(bagsNeeded), 2));
            model.BagSize = inventoryProduct.SizeOfUnit + " " + inventoryProduct.UnitType;
            model.FieldArea = field.Size.ToString();
            model._calculatorName = WebLocalizationKeys.FERTILIZER_NEEDED.ToString();
            continuation.Target = model;

            return continuation;
        }
    }
}