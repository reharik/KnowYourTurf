namespace KnowYourTurf.Core.Html.Grid
{
    public class PageSortFilter
    {

        public PageSortFilter()
        {
        }

        public PageSortFilter(int page, int take, string sortColumn, bool sortAscending)
        {
            Page = page;
            Take = take;
            SortColumn = sortColumn;
            SortAscending = sortAscending;
        }

        public int Page { get; set; }
        public int Skip { get { return (Page - 1) * Take; } }
        public int Take { get; set; }
        public string SortColumn { get; set; }
        public bool SortAscending { get; set; }
    }
}