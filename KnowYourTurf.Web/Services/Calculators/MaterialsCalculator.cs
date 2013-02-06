using System;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Controllers;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Services
{
    public class MaterialsCalculator : ICalculatorHandler
    {
        private readonly IRepository _repository;
        private readonly ISelectListItemService _selectListItemService;

        public MaterialsCalculator(IRepository repository, ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _selectListItemService = selectListItemService;
        }

        public CalculatorViewModel GetViewModel()
        {
            var fieldItems = ((KYTSelectListItemService)_selectListItemService).CreateFieldsSelectListItems();
            return new MaterialsCalcViewModel
            {
                _FieldEntityIdList = fieldItems,
                _calculatorDisplayName = WebLocalizationKeys.MATERIALS.ToString(),
                _calculatorName = WebLocalizationKeys.MATERIALS.ToString(),
                _calculateUrl = UrlContext.GetUrlForAction<CalculatorController>(x => x.Calculate(null)),
            };
        }

        public Continuation Calculate(SuperInputCalcViewModel input)
        {
            var continuation = new Continuation();
            var model = new MaterialsCalcViewModel();
            var field = _repository.Find<Field>(input.FieldEntityId);
            double material = ((field.Size * (input.Depth / 12)) / 27) + (input.Drainageline * (input.DitchlineWidth / input.DitchDepth) / 27) - (3.14 * (input.PipeRadius / 12)*2 * input.Drainageline / 27);
            model.TotalMaterials = Convert.ToDouble(Math.Round(Convert.ToDecimal(material), 2));
            model.FieldArea = field.Size;
            model._calculatorName = WebLocalizationKeys.MATERIALS.ToString();
            continuation.Target = model;
            //(
            //    (
            //        3.14
            //        *(
            //            (tine diameter/2)/12
            //        )^2
            //        *(tine depth/12)
            //    )
            //    *holes per sq. ft
            //)
            //*area
            return continuation;
        }

        public CalculatorViewModel EmptyViewModel()
        {
            return new MaterialsCalcViewModel();
        }
    }
}