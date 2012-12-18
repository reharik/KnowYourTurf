using CC.Core.CoreViewModelAndDTOs;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;

namespace KnowYourTurf.Web.Models.Fertilizer
{
    public class FertilizerViewModel:ViewModel
    {
        [ValidateNonEmpty]
        public string Name { get; set; }
        [TextArea]
        public string Description { get; set; }
        [TextArea]
        public string Notes { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double N { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double P { get; set; }
        [ValidateNonEmpty, ValidateDouble]
        public double K { get; set; }

        public string _saveUrl { get; set; }
    }
}