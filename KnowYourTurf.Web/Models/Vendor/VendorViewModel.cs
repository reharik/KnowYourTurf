using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Models.Vendor
{
    public class VendorViewModel : ViewModel
    {
        public IEnumerable<string> VendorContactNames{ get; set; }
        public TokenInputViewModel Chemicals { get; set; }
        public TokenInputViewModel Fertilizers { get; set; }
        public TokenInputViewModel Materials { get; set; }

        [ValidateNonEmpty]
        public string Company { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        [ValueOf(typeof(State))]
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        [TextArea]
        public string Notes { get; set; }
        [ValueOf(typeof(Status))]
        public string Status { get; set; }

        public string _saveUrl { get; set; }

        public IEnumerable<SelectListItem> _StateList { get; set; }

        public IEnumerable<SelectListItem> _StatusList { get; set; }
    }
}