using System.Collections.Generic;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Models
{
    public class FieldViewModel : ListViewModel
    {
        public FieldViewModel()
        {
            _PhotoHeaderButtons = new List<string>();
            _DocumentHeaderButtons = new List<string>();
        }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public string FieldColor { get; set; }
        public string FileUrl { get; set; }
        public bool DeleteImage { get; set; }
        public IEnumerable<Photo> Photos { get; set; }

        public string _CompletedGridUrl { get; set; }
        public string _DocumentGridUrl { get; set; }
        public string _PhotoGridUrl { get; set; }
        public string _PendingGridUrl { get; set; }
        public string _AddUpdatePhotoUrl { get; set; }
        public string _AddUpdateDocumentUrl { get; set; }
        public string _DeleteMultiplePhotosUrl { get; set; }
        public string _DeleteMultipleDocumentsUrl { get; set; }
        public List<string> _PhotoHeaderButtons { get; set; }
        public List<string> _DocumentHeaderButtons { get; set; }

        public string _saveUrl { get; set; }
    }
}