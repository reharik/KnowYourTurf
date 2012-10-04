using System;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
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
            var fieldItems = _selectListItemService.CreateFieldsSelectListItems();
            return new OverseedRateNeededCalcViewModel
            {
                _FieldEntityIdList = fieldItems,
                _ProductEntityIdList = productItems,
                _calculatorDisplayName = WebLocalizationKeys.OVERSEED_RATE_NEEEDED_DISPLAY.ToString(),
                _calculatorName = WebLocalizationKeys.OVERSEED_RATE_NEEEDED.ToString(),
                _calculateUrl = UrlContext.GetUrlForAction<CalculatorController>(x => x.Calculate(null)),
            };
        }

        public Continuation Calculate(SuperInputCalcViewModel input)
        {
            var continuation = new Continuation();
            var model = new OverseedRateNeededCalcViewModel();
            var field = _repository.Find<Field>(input.FieldEntityId);
            var inventoryProduct = _repository.Find<InventoryProduct>(input.ProductEntityId);
            decimal bagSizeInPounds = _unitSizeTimesQuantyCalculator.CalculateLbsPerUnit(inventoryProduct);
            double? bagsNeeded = ((input.BagsUsed * Convert.ToDouble(bagSizeInPounds) * 1000)/field.Size)*(input.OverSeedPercent * .01);

            model.SeedRate = Convert.ToDouble(Math.Round(Convert.ToDecimal(bagsNeeded), 2));
            model.BagSize = inventoryProduct.SizeOfUnit + " " + inventoryProduct.UnitType;
            model.FieldArea = field.Size.ToString();
            model._calculatorName = WebLocalizationKeys.OVERSEED_RATE_NEEEDED.ToString();
            continuation.Target = model;
            return continuation;
        }

        public CalculatorViewModel EmptyViewModel()
        {
            return new OverseedRateNeededCalcViewModel();
        }
    }
}