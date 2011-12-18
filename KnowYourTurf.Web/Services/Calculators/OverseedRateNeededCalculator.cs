using System;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Services
{
    public class OverseedRateNeededCalculator : ICalculatorHandler
    {
        private readonly IRepository _repository;
        private readonly IUnitSizeTimesQuantyCalculator _unitSizeTimesQuantyCalculator;
        private readonly ISelectListItemService _selectListItemService;

        public OverseedRateNeededCalculator(IRepository repository, IUnitSizeTimesQuantyCalculator unitSizeTimesQuantyCalculator, ISelectListItemService selectListItemService)
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
            return new OverseedRateNeededCalcViewModel
            {
                FieldList = fieldItems,
                ProductList = productItems,
                CalculatorDisplayName = WebLocalizationKeys.OVERSEED_RATE_NEEEDED_DISPLAY.ToString(),
                CalculatorName = WebLocalizationKeys.OVERSEED_RATE_NEEEDED.ToString(),
                CalculateUrl = UrlContext.GetUrlForAction<CalculatorController>(x => x.Calculate(null)),
//                SaveJSSuccssCallback = "kyt.calculator.controller.overseedRateNeededSuccess"
            };
        }

        public Continuation Calculate(SuperInputCalcViewModel input)
        {
            var continuation = new Continuation();
            var model = new OverseedRateNeededCalcViewModel();
            var field = _repository.Find<Field>(Int64.Parse(input.Field));
            var inventoryProduct = _repository.Find<InventoryProduct>(Int64.Parse(input.Product));
            decimal bagSizeInPounds = _unitSizeTimesQuantyCalculator.CalculateLbsPerUnit(inventoryProduct);
            double? bagsNeeded = ((input.BagsUsed * Convert.ToDouble(bagSizeInPounds) * 1000)/field.Size)*(input.OverSeedPercent * .01);

            model.SeedRate = Convert.ToDouble(Math.Round(Convert.ToDecimal(bagsNeeded), 2));
            model.BagSize = inventoryProduct.SizeOfUnit + " " + inventoryProduct.UnitType;
            model.FieldArea = field.Size.ToString();
            continuation.Target = model;
            return continuation;
        }
    }
}