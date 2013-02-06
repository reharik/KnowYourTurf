using CC.Core.CoreViewModelAndDTOs;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;

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