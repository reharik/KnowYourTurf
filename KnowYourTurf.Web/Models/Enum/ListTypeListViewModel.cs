using KnowYourTurf.Core;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Web.Models
{
    public class ListTypeListViewModel : ListViewModel
    {
        public string AddUpdateUrlET { get; set; }
        public string AddUpdateUrlTT { get; set; }
        public string AddUpdateUrlDC { get; set; }
        public string AddUpdateUrlPC { get; set; }
        public GridDefinition ListDefinitionTT { get; set; }
        public GridDefinition ListDefinitionDC { get; set; }
        public GridDefinition ListDefinitionPC { get; set; }

        public string PopupTitleET { get; set; }

        public string PopupTitleTT { get; set; }

        public string PopupTitlePC { get; set; }

        public string PopupTitleDC { get; set; }

        public string DeleteMultipleET { get; set; }

        public string DeleteMultipleTT { get; set; }

        public string DeleteMultipleDC { get; set; }

        public string DeleteMultiplePC { get; set; }
    }
}