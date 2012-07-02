using System.Collections.Generic;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Web.Models
{
    public class FieldDashboardViewModel : ListViewModel
    {
        public FieldDashboardViewModel()
        {
            PhotoHeaderButtons = new List<string>();
            DocumentHeaderButtons = new List<string>();
        }
        public Field Item { get; set; }
        public string CompletedGridUrl { get; set; }
        public string DocumentGridUrl { get; set; }
        public string PhotoGridUrl { get; set; }
        public string PendingGridUrl { get; set; }
        public string AddUpdatePhotoUrl { get; set; }
        public string AddUpdateDocumentUrl { get; set; }
        public string DeleteMultiplePhotosUrl { get; set; }
        public string DeleteMultipleDocumentsUrl { get; set; }
        public List<string> PhotoHeaderButtons { get; set; }
        public List<string> DocumentHeaderButtons { get; set; }

    }
}