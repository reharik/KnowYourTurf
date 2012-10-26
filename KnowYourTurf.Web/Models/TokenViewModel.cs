using System.Collections.Generic;
using CC.Core.CoreViewModelAndDTOs;

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