using System;
using System.Collections;
using System.Collections.Generic;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Core.CoreViewModels
{
    public class GridItemsViewModel
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public IEnumerable items { get; set; }
        public IDictionary<string, string> userdata { get; set; }

        public int GetTotal(int totalRecordsAvailable)
        {
            if (records <= 0)
                return 0;
            decimal subTotal = (decimal)records / (decimal)totalRecordsAvailable;
            return subTotal < 1 ? 1 : (int)Math.Ceiling(subTotal);
        }
    }

    public class GridItemsRequestModel : ListViewModel
    {
        public int page { get; set; }
        public int rows { get; set; }
        public string sidx { get; set; }
        public string sord { get; set; }
        public bool showDeleted { get; set; }
        public bool showArchived { get; set; }
        private PageSortFilter _pageSortFilter;
        public PageSortFilter PageSortFilter
        {
            get
            {
                bool sortAscending = true;
                if (sord != null && sord.Equals("desc", StringComparison.OrdinalIgnoreCase)) sortAscending = false;
                return new PageSortFilter(page, rows, sidx, sortAscending);
            }
            set { _pageSortFilter = value; }
        }

        public string filters { get; set; }

        public string Product { get; set; }
    }

}