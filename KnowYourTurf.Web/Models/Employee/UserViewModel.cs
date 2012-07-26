using System.Collections.Generic;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Models
{
    public class UserViewModel:ViewModel
    {
        public bool DeleteImage { get; set; }
        public User Employee { get; set; }
        public IEnumerable<TokenInputDto> AvailableItems { get; set; }
        public IEnumerable<TokenInputDto> SelectedItems { get; set; }
        public string RolesInput { get; set; }
    }
}