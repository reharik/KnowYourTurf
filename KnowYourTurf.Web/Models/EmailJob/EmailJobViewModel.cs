using System.Collections.Generic;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.CustomAttributes;
using CC.Core.Localization;
using Castle.Components.Validator;
using KnowYourTurf.Core.Enums;

namespace KnowYourTurf.Web.Models
{
    public class EmailJobViewModel:ViewModel
    {
        public TokenInputViewModel Subscribers { get; set; }
        public IEnumerable<SelectListItem> _EmailTemplateEntityIdList { get; set; }
        public IEnumerable<SelectListItem> _FrequencyList { get; set; }
        public IEnumerable<SelectListItem> _StatusList { get; set; }

        [ValidateNonEmpty]
        public string Name { get; set; }
        [TextArea]
        public string Description { get; set; }
        public string Subject { get; set; }
        [ValidateNonEmpty]
        [ValueOf(typeof(EmailFrequency))]
        public string Frequency { get; set; }
        [ValueOf(typeof(Status))]
        public string Status { get; set; }

        [ValidateNonEmpty]
        public int EmailTemplateEntityId { get; set; }

        public string _saveUrl { get; set; }
    }
}