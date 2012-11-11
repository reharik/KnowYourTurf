using CC.Core.CoreViewModelAndDTOs;

namespace KnowYourTurf.Web.Models
{
    public class ListTypeListViewModel : ListViewModel
    {
        public string _taskTypeGridUrl { get; set; }
        public string _eventTypeGridUrl { get; set; }
        public string _photoCategoryGridUrl { get; set; }
        public string _documentCategoryGridUrl { get; set; }
        public string _deleteMultipleTaskTypesUrl { get; set; }
        public string _deleteMultipleEventTypesUrl { get; set; }
        public string _deleteMultiplePhotoCatUrl { get; set; }
        public string _deleteMultipleDocCatUrl { get; set; }
    }
}