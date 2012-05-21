using System.Collections.Generic;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Web.Models
{
    public class EmployeeDashboardViewModel : ViewModel
    {
        public User Item { get; set; }
        public bool DeleteImage { get; set; }
        public string RolesInput { get; set; }
        public IEnumerable<TokenInputDto> AvailableItems { get; set; }
        public IEnumerable<TokenInputDto> SelectedItems { get; set; }
        public string PendingGridUrl { get; set; }
        public string CompletedGridUrl { get; set; }
        public bool ReturnToList { get; set; }
    }
}