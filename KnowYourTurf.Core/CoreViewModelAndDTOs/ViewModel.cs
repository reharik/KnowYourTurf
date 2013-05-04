using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Core
{
    public class ViewModel
    {
        public int EntityId { get; set; }
        public int ParentId { get; set; }
        public int RootId { get; set; }
        public string Title { get; set; }
        public string AddUpdateUrl { get; set; }
    }

    public class ListViewModel :ViewModel
    {
        public string DeleteMultipleUrl { get; set; }
        public GridDefinition GridDefinition { get; set; }
        public string DisplayUrl { get; set; }
    }
}