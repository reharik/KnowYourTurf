using System.Collections.Generic;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.CustomAttributes;
using CC.Core.Localization;
using Castle.Components.Validator;
using KnowYourTurf.Core.Enums;

namespace KnowYourTurf.Web.Models
{
    public class ListTypeViewModel : ViewModel
    {
        [ValidateNonEmpty]
        public string Name { get; set; }
        [TextArea]
        public string Description { get; set; }
        [ValueOf(typeof(Status))]
        public string Status { get; set; }
        public string _saveUrl { get; set; }
        public IEnumerable<SelectListItem> _StatusList { get; set; }
    }

    public class EventTypeViewModel : ListTypeViewModel
    {
        public string EventColor { get; set; }
    }
    public class EquipmentTypeViewModel : ListTypeViewModel
    {
        public string TaskColor { get; set; }
    }

    public class PartViewModel : ListTypeViewModel
    {
        public string Vendor { get; set; }
        public string FileUrl { get; set; }
    }

}