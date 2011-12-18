using KnowYourTurf.Core;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Web.Models
{
    public class ListTypeListViewModel : ListViewModel
    {
        public string AddEditUrlET { get; set; }
        public string AddEditUrlTT { get; set; }
        public string AddEditUrlDC { get; set; }
        public string AddEditUrlPC { get; set; }
        public GridDefinition ListDefinitionTT { get; set; }
        public GridDefinition ListDefinitionDC { get; set; }
        public GridDefinition ListDefinitionPC { get; set; }

        public string PopupTitleET { get; set; }

        public string PopupTitleTT { get; set; }

        public string PopupTitlePC { get; set; }

        public string PopupTitleDC { get; set; }
    }
}