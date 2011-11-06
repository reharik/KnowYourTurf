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
        public string From { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
    }

    public class ListViewModel : ViewModel
    {
        public GridDefinition ListDefinition { get; set; }
        public bool NotPopup { get; set; }
    }
}