using System;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Services
{
    public class OverseedBagsNeededCalculator : ICalculatorHandler
    {
        private readonly IRepository _repository;
        private readonly IUnitSizeTimesQuantyCalculator _unitSizeTimesQuantyCalculator;
        private readonly ISelectListItemService _selectListItemService;

        public OverseedBagsNeededCalculator(IRepository repository, IUnitSizeTimesQuantyCalculator unitSizeTimesQuantyCalculator, ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _unitSizeTimesQuantyCalculator = unitSizeTimesQuantyCalculator;
            _selectListItemService = selectListItemService;
        }

        public CalculatorViewModel GetViewModel()
        {
            var products = _repository.Query<InventoryProduct>(x => x.Product.InstantiatingType == "Material");
            var productItems = _selectListItemService.CreateListWithConcatinatedText(products, x => x.Product.Name, x => x.UnitType, " --> ", x => x.EntityId, true);
            var fieldItems = _selectListItemService.CreateList<Field>(x => x.Name, x => x.EntityId, true,true);
            return new OverseedBagsNeededCalcViewModel
            {
                FieldList = fieldItems,
                ProductList = productItems,
                CalculatorDisplayName = WebLocalizationKeys.OVERSEED_BAGS_NEEEDED_DISPLAY.ToString(),
                CalculatorName = WebLocalizationKeys.OVERSEED_BAGS_NEEEDED.ToString(),
                CalculateUrl = UrlContext.GetUrlForAction<CalculatorController>(x => x.Calculate(null)),
                SaveJSSuccssCallback = "kyt.calculator.controller.overseedBagsNeededSuccess"
            };
        }

        public Continuation Calculate(SuperInputCalcViewModel input)
        {
            var continuation = new Continuation();
            var model = new OverseedBagsNeededCalcViewModel();
            var field = _repository.Find<Field>(Int64.Parse(input.Field));
            var inventoryProduct = _repository.Find<InventoryProduct>(Int64.Parse(input.Product));
            decimal bagSizeInPounds = _unitSizeTimesQuantyCalculator.CalculateLbsPerUnit(inventoryProduct);
            double? bagsNeeded = ((input.SeedRate / (input.OverSeedPercent * .01)) * (Convert.ToDouble(field.Size / 1000))) / Convert.ToDouble(bagSizeInPounds);

            model.BagsNeeded = Convert.ToDouble(Math.Round(Convert.ToDecimal(bagsNeeded), 2));
            model.BagSize = inventoryProduct.SizeOfUnit + " " + inventoryProduct.UnitType;
            model.FieldArea = field.Size.ToString();
            continuation.Target = model;
            return continuation;
        }
    }
}