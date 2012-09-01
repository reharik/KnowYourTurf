using System.Collections.Generic;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;

namespace KnowYourTurf.Web.Models
{
    public class TokenViewModel:ViewModel
    {
        public string Name { get; set; }
       public IEnumerable<TokenInputDto> SelectedItems { get; set; }
        public IEnumerable<TokenInputDto> AvailableItems { get; set; }

        public string TooltipAjaxUrl { get; set; }

        public string NamePlural { get; set; }
    }
}