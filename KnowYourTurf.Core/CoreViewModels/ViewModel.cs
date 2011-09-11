using System;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Core
{
    public class ViewModel
    {
        public long EntityId { get; set; }
        public long ParentId { get; set; }
        public long RootId { get; set; }
        public string AddEditUrl { get; set; }
        private string _saveJsSuccssCallback = "kyt.popupCrud.controller.success";
        public string SaveJSSuccssCallback
        {
            get { return _saveJsSuccssCallback; }
            set { _saveJsSuccssCallback = value; }
        }
        public string From { get; set; }
        public User User { get; set; }
        public string CrudTitle { get; set; }
    }

    public class ListViewModel : ViewModel
    {
        public GridDefinition ListDefinition { get; set; }
        public bool NotPopup { get; set; }
    }
}