using CC.Core.CoreViewModelAndDTOs;
using Castle.Components.Validator;
using KnowYourTurf.Core;

namespace KnowYourTurf.Web.Models.Material
{
    public class MaterialViewModel:ViewModel
    {
        [ValidateNonEmpty]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }

        public string _saveUrl { get; set; }
    }
}