using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Models
{
    public class EmailJobViewModel:ViewModel
    {
        public EmailJob EmailJob { get; set; }
        public SelectBoxPickerDto UserSelectBoxPickerDto { get; set; }
        public IEnumerable<SelectListItem> EmailTemplateList { get; set; }
    }
}