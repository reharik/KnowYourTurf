using CC.Core.CoreViewModelAndDTOs;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;

namespace KnowYourTurf.Web.Models
{
    public class ChemicalViewModel:ViewModel
    {
        [ValidateNonEmpty]
        public string Name { get; set; }
        [TextArea]
        public string Description { get; set; }
        public string Notes { get; set; }
        public string Manufacturer { get; set; }
        public string ActiveIngredient { get; set; }
        [ValidateDecimal]
        public decimal ActiveIngredientPercent { get; set; }
        public string EPARegNumber { get; set; }
        public string EPAEstNumber { get; set; }

        public string _saveUrl { get; set; }
    }
}