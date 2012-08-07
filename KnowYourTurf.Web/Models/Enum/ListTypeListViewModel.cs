using KnowYourTurf.Core;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Web.Models
{
    public class ListTypeListViewModel : ListViewModel
    {
        public string _taskTypeGridUrl { get; set; }
        public string _eventTypeGridUrl { get; set; }
        public string _photoCategoryGridUrl { get; set; }
        public string _documentCategoryGridUrl { get; set; }
        public GridDefinition ListDefinitionTT { get; set; }
        public GridDefinition ListDefinitionDC { get; set; }
        public GridDefinition ListDefinitionPC { get; set; }

    }
}