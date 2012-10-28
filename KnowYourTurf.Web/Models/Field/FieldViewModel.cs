using System.Collections.Generic;
using CC.Core.CoreViewModelAndDTOs;

namespace KnowYourTurf.Web.Models
{
    public class FieldViewModel : ViewModel
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

        public string _completedGridUrl { get; set; }
        public string _documentGridUrl { get; set; }
        public string _photoGridUrl { get; set; }
        public string _pendingGridUrl { get; set; }
        public string _AddUpdatePhotoUrl { get; set; }
        public string _AddUpdateDocumentUrl { get; set; }
        public string _DeleteMultiplePhotosUrl { get; set; }
        public string _DeleteMultipleDocumentsUrl { get; set; }
        public List<string> _PhotoHeaderButtons { get; set; }
        public List<string> _DocumentHeaderButtons { get; set; }
        public IEnumerable<PhotoDto> _Photos { get; set; }

        public string _saveUrl { get; set; }

    }
}