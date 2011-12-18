using System.Collections.Generic;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Web.Models
{
    public class EmployeeDashboardViewModel : ListViewModel
    {
        public User User { get; set; }
        public bool DeleteImage { get; set; }
        public string RolesInput { get; set; }
        public IEnumerable<TokenInputDto> AvailableItems { get; set; }
        public IEnumerable<TokenInputDto> SelectedItems { get; set; }
        public GridDefinition CompletedListDefinition { get; set; }
        public string EmployeeListUrl { get; set; }
    }
}