using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Web.Models
{
    public class FieldDashboardViewModel : ListViewModel
    {
        public Field Item { get; set; }
        public GridDefinition CompletedListDefinition { get; set; }

        public GridDefinition DocumentListDefinition { get; set; }

        public GridDefinition PhotoListDefinition { get; set; }

        public string AddUpdatePhotoUrl { get; set; }

        public string AddUpdateDocumentUrl { get; set; }

        public string DeleteMultiplePhotosUrl { get; set; }

        public string DeleteMultipleDocumentsUrl { get; set; }

    }
}