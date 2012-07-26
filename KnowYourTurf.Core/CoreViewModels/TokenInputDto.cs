using System.Collections.Generic;

namespace KnowYourTurf.Core.CoreViewModels
{
    public class TokenInputDto
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class TokenInputViewModel
    {
        public IEnumerable<TokenInputDto> availableItems { get; set; }
        public IEnumerable<TokenInputDto> selectedItems { get; set; }
    }
}