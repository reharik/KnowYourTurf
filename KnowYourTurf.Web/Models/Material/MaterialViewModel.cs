using CC.Core.CoreViewModelAndDTOs;
using CC.Core.CustomAttributes;
using Castle.Components.Validator;
using KnowYourTurf.Core;

namespace KnowYourTurf.Web.Models.Material
{
    public class MaterialViewModel:ViewModel
    {
        [ValidateNonEmpty]
        public string Name { get; set; }
        [TextArea]
        public string Description { get; set; }
        [TextArea]
        public string Notes { get; set; }

        public string _saveUrl { get; set; }
    }
}