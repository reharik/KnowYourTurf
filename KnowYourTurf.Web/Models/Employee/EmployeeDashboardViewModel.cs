using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Web.Models
{
    public class EmployeeDashboardViewModel : ListViewModel
    {
        public User Employee { get; set; }
        public GridDefinition CompletedListDefinition { get; set; }
    }
}