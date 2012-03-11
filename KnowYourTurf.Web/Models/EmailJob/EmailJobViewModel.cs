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
        public EmailJob Item { get; set; }
        public string EmployeeInput { get; set; }
        public IEnumerable<TokenInputDto> AvailableEmployees { get; set; }
        public IEnumerable<TokenInputDto> SelectedEmployees { get; set; }
        public IEnumerable<SelectListItem> EmailTemplateList { get; set; }
    }
}