using System;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
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
            var fieldItems = _selectListItemService.CreateList<Field>(x => x.Name, x => x.EntityId, true,true);
            return new MaterialsCalcViewModel
            {
                FieldList = fieldItems,
                CalculatorDisplayName = WebLocalizationKeys.MATERIALS.ToString(),
                CalculatorName = WebLocalizationKeys.MATERIALS.ToString(),
                CalculateUrl = UrlContext.GetUrlForAction<CalculatorController>(x => x.Calculate(null)),
//                SaveJSSuccssCallback = "kyt.calculator.controller.materialsSuccess"
            };
        }

        public Continuation Calculate(SuperInputCalcViewModel input)
        {
            var continuation = new Continuation();
            var model = new MaterialsCalcViewModel();
            var field = _repository.Find<Field>(Int64.Parse(input.Field));
            double material = ((field.Size * (input.Depth / 12)) / 27) + (input.Drainageline * (input.DitchlineWidth / input.DitchDepth) / 27) - (3.14 * (input.PipeRadius / 12)*2 * input.Drainageline / 27);
            model.TotalMaterials = Convert.ToDouble(Math.Round(Convert.ToDecimal(material), 2));
            model.FieldArea = field.Size;
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
    }
}