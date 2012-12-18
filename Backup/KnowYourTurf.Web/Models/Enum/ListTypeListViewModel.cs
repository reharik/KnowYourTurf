using CC.Core.CoreViewModelAndDTOs;

namespace KnowYourTurf.Web.Models
{
    public class ListTypeListViewModel : ListViewModel
    {
        public string _taskTypeGridUrl { get; set; }
        public string _eventTypeGridUrl { get; set; }
        public string _photoCategoryGridUrl { get; set; }
        public string _documentCategoryGridUrl { get; set; }
        public string _equipmentTaskTypeGridUrl { get; set; }
        public string _equipmentTypeGridUrl { get; set; }
        public string _partsGridUrl { get; set; }
        
        public string _deleteMultipleTaskTypesUrl { get; set; }
        public string _deleteMultipleEventTypesUrl { get; set; }
        public string _deleteMultiplePhotoCatUrl { get; set; }
        public string _deleteMultipleDocCatUrl { get; set; }
        public string _deleteMultipleEquipTaskTypeUrl { get; set; }
        public string _deleteMultipleEquipTypeUrl { get; set; }
        public string _deleteMultiplePartsUrl { get; set; }
    }
}