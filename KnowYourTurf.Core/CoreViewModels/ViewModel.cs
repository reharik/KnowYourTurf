using System.Collections.Generic;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Core
{
    public class ViewModel
    {
        public long EntityId { get; set; }
        public long ParentId { get; set; }
        public long RootId { get; set; }
        public string AddUpdateUrl { get; set; }
        public string Var { get; set; }
        public string _Title { get; set; }
        public bool Popup { get; set; }
    }

    public class ListViewModel : ViewModel
    {
        public ListViewModel()
        {
            headerButtons = new List<string>();
        }

        public string deleteMultipleUrl { get; set; }
        public GridDefinition gridDef { get; set; }
        public List<string> headerButtons { get; set; }
        public string searchField { get; set; }
//        public string addUpdate { get; set; }
    }
}