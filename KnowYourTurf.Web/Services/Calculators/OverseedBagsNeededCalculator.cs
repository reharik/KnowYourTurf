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
            var fieldItems = _selectListItemService.CreateFieldsSelectListItems();
            return new OverseedBagsNeededCalcViewModel
            {
                _FieldEntityIdList = fieldItems,
                _ProductEntityIdList = productItems,
                _calculatorDisplayName = WebLocalizationKeys.OVERSEED_BAGS_NEEEDED_DISPLAY.ToString(),
                _calculatorName = WebLocalizationKeys.OVERSEED_BAGS_NEEEDED.ToString(),
                _calculateUrl = UrlContext.GetUrlForAction<CalculatorController>(x => x.Calculate(null)),
            };
        }

        public Continuation Calculate(SuperInputCalcViewModel input)
        {
            var continuation = new Continuation();
            var model = new OverseedBagsNeededCalcViewModel();
            var field = _repository.Find<Field>(input.FieldEntityId);
            var inventoryProduct = _repository.Find<InventoryProduct>(input.ProductEntityId);
            decimal bagSizeInPounds = _unitSizeTimesQuantyCalculator.CalculateLbsPerUnit(inventoryProduct);
            double? bagsNeeded = ((input.SeedRate / (input.OverSeedPercent * .01)) * (Convert.ToDouble(field.Size / 1000))) / Convert.ToDouble(bagSizeInPounds);

            model.BagsNeeded = Convert.ToDouble(Math.Round(Convert.ToDecimal(bagsNeeded), 2));
            model.BagSize = inventoryProduct.SizeOfUnit + " " + inventoryProduct.UnitType;
            model.FieldArea = field.Size.ToString();
            model._calculatorName = WebLocalizationKeys.OVERSEED_BAGS_NEEEDED.ToString();
            continuation.Target = model;
            return continuation;
        }

        public CalculatorViewModel EmptyViewModel()
        {
            return new OverseedBagsNeededCalcViewModel();
        }
    }
}