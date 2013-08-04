using System.Collections.Generic;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.CustomAttributes;
using Castle.Components.Validator;

namespace KnowYourTurf.Web.Models
{
    public class FieldViewModel : ViewModel
    {
        public FieldViewModel()
        {
            _PhotoHeaderButtons = new List<string>();
            _DocumentHeaderButtons = new List<string>();
        }
        [ValidateNonEmpty]
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        [ValidateNonEmpty]
        [TextArea]
        public string Description { get; set; }
        [ValidateNonEmpty, ValidateIntegerAttribute]
        public int Size { get; set; }
        public string FieldColor { get; set; }
        public string FileUrl { get; set; }
        public bool DeleteImage { get; set; }
        [ValidateNonEmpty]
        public int GrassTypeEntityId { get; set; }

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
        public IEnumerable<SelectListItem> _GrassTypeEntityIdList { get; set; }

        public string _saveUrl { get; set; }

    }
}